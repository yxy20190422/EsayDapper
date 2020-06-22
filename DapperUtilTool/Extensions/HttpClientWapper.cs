using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DapperUtilTool.Extensions
{
    /// <summary>
    /// webapi通讯接口封装
    /// </summary>
    public static class HttpClientWapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientFactory"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="Head"></param>
        /// <param name="PostStr"></param>
        /// <param name="Uri"></param>
        /// <returns></returns>
        public static async Task<string> GetMessageFromApiDress(this IHttpClientFactory clientFactory,string HttpMethod, string Head, string PostStr, string Uri)
        {
            var client = clientFactory.CreateClient("Controller");
            if (!string.IsNullOrWhiteSpace(Head))
            {
                var timeStamp = Common.GetTimeStamp();
                var secret = ConstantConfig.Secret;
                var body = Head;
                var str = secret + body + timeStamp;
                var token = Common.MD5Encrypt(str);
                client.DefaultRequestHeaders.Add("key", ConstantConfig.Key);
                client.DefaultRequestHeaders.Add("time", timeStamp.ToString());
                client.DefaultRequestHeaders.Add("token", token);
            }
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("ContentType", "application/json");
            var stringContent= new  StringContent("");
            if (!string.IsNullOrWhiteSpace(PostStr))
            {
                 stringContent = new StringContent(PostStr);
            }
            stringContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            var response =  HttpMethod.ToLower() == "post" ? await client.PostAsync(Uri, stringContent) : await client.GetAsync(Uri);
            if (response.IsSuccessStatusCode)
            {
                var Message = await response.Content.ReadAsStringAsync();
                return Message;
            }
            else
            {
                return "接口通讯失败！";
            }
        }
    }
}
