using DapperUtilTool.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DapperService.Dto.OtherServiceDto
{
    /// <summary>
    /// 比价系统调用CRM的服务API
    /// </summary>
    public class ComparePriceToCrmApi
    {
        /// <summary>
        /// 
        /// </summary>
        private IHttpClientFactory _apiRequest { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiRequest"></param>
        public ComparePriceToCrmApi( IHttpClientFactory apiRequest)
        {
            _apiRequest = apiRequest;
        }
        /// <summary>
        /// 获取CRM采购用户下拉的用户数组
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ComparePriceToCrmApiResultDto<List<CrmPurchaseOutPutDto>>> GetPurchaseBelongsFromCrm(PurchaseInputDto input)
        {
                string poststr = Newtonsoft.Json.JsonConvert.SerializeObject(input);         
                string apiResult = await _apiRequest.GetMessageFromApiDress("Post", poststr, poststr, "api/PreOrderApi/User/GetCrmPurchaseUsers");
                ComparePriceToCrmApiResultDto<List<CrmPurchaseOutPutDto>> output = Newtonsoft.Json.JsonConvert.DeserializeObject<ComparePriceToCrmApiResultDto<List<CrmPurchaseOutPutDto>>>(apiResult);
                return output;         
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ComparePriceToCrmApiResultDto<List<CrmPurchaseOutPutDto>> GetPurchaseBelongsFromCrm1(PurchaseInputDto input)
        {
            string poststr = Newtonsoft.Json.JsonConvert.SerializeObject(input);
            string apiResult =  _apiRequest.GetMessageFromApiDress("Post", poststr, poststr, "api/PreOrderApi/User/GetCrmPurchaseUsers").Result;
            ComparePriceToCrmApiResultDto<List<CrmPurchaseOutPutDto>> output = Newtonsoft.Json.JsonConvert.DeserializeObject<ComparePriceToCrmApiResultDto<List<CrmPurchaseOutPutDto>>>(apiResult);
            return output;
        }
    }
}
