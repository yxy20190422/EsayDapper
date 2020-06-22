using DapperModelCore.DapperCore;
using DapperModelCore.DBModel;
using DapperService.Dto;
using DapperService.Dto.OtherServiceDto;
using DapperService.IService;
using DapperUtilTool.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;

namespace DapperService.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class PlatformServices : IPlatformServices
    {
        /// <summary>
        /// 仓储接口
        /// </summary>
        private IRepository _iRepository { get; set; }

        private IHttpClientFactory _apiRequest { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iRepository"></param>
        /// <param name="apiRequest"></param>
        public PlatformServices(IRepository iRepository, IHttpClientFactory apiRequest)
        {
            _iRepository = iRepository;
            _apiRequest = apiRequest;
        }
        /// <summary>
        /// 返回有效的第三方平台的数据
        /// </summary>
        /// <returns></returns>
        public OnlineOrdersResultDto<List<Ppc_PlatformConfigOutPutDto>> GetPlatform()
        {
            OnlineOrdersResultDto<List<Ppc_PlatformConfigOutPutDto>> res = new OnlineOrdersResultDto<List<Ppc_PlatformConfigOutPutDto>>()
            {
                StatusCode = 0,
                Message = "获取平台信息失败",
                Ext1 = new List<Ppc_PlatformConfigOutPutDto>()
            };
            Expression<Func<Ppc_PlatformConfig, bool>> prediate = m => m.IsShow == 1;
            var selector = ExpExtension.GetSelector<Ppc_PlatformConfig, Ppc_PlatformConfigOutPutDto>();
            var listplat = _iRepository.GetList<Ppc_PlatformConfig, Ppc_PlatformConfigOutPutDto>(prediate, null, null, selector);
            if (listplat == null || listplat.Count == 0 || listplat[0].ID.GetValueOrDefault(0) < 0)
            {
                return res;
            }
            res.StatusCode = 1;
            res.Ext1 = listplat;
            res.Message = "获取平台信息成功";
            return res;
        }
        /// <summary>
        /// 获取CRM采购用户下拉的用户数组
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ComparePriceToCrmApiResultDto<List<CrmPurchaseOutPutDto>> GetPurchaseBelongsFromCrm1(PurchaseInputDto input)
        {
            string poststr = Newtonsoft.Json.JsonConvert.SerializeObject(input);
            ComparePriceToCrmApiResultDto<List<CrmPurchaseOutPutDto>> output =  new ComparePriceToCrmApi(_apiRequest).GetPurchaseBelongsFromCrm(input).Result;
            return output;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ComparePriceToCrmApiResultDto<List<CrmPurchaseOutPutDto>>> GetPurchaseBelongsFromCrm(PurchaseInputDto input)
        {
            string poststr = Newtonsoft.Json.JsonConvert.SerializeObject(input);
            ComparePriceToCrmApiResultDto<List<CrmPurchaseOutPutDto>> output = await new ComparePriceToCrmApi(_apiRequest).GetPurchaseBelongsFromCrm(input);
            return output;
        }
    }
}
