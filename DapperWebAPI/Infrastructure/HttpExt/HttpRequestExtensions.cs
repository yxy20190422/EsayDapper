using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperWebAPI.Infrastructure.HttpExt
{
    /// <summary>
    /// HTTP请求扩展
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// 获取输出内容主体字符串
        /// </summary>
        /// <param name="request"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetRawBodyString(this HttpRequest request, Encoding encoding = null)
        {
            if (encoding is null)
                encoding = Encoding.UTF8;

            using (var reader = new StreamReader(request.Body, encoding))
            {
                request.Body.Seek(0, SeekOrigin.Begin);
                return reader.ReadToEnd();
            }
        }
        /// <summary>
        /// 异步获取输出内容主体字符串
        /// </summary>
        /// <param name="request"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<string> GetRawBodyStringAsync(this HttpRequest request, Encoding encoding = null)
        {
            if (encoding is null)
                encoding = Encoding.UTF8;
            using (var reader = new StreamReader(request.Body, encoding))
            {
                request.Body.Seek(0, SeekOrigin.Begin);
                return await reader.ReadToEndAsync();
            }
        }
        /// <summary>
        /// 获取输出内容主体字节
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static byte[] GetRawBodyBytes(this HttpRequest request)
        {
            using (var ms = new MemoryStream(2048))
            {
                request.Body.CopyTo(ms);
                return ms.ToArray();
            }
        }
        /// <summary>
        /// 异步获取输出内容主体字节
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<byte[]> GetRawBodyBytesAsync(this HttpRequest request)
        {
            using (var ms = new MemoryStream(2048))
            {
                await request.Body.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
    }
}
