using DapperService.Dto;
using DapperService.Dto.OtherServiceDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DapperService.IService
{
    /// <summary>
    /// 平台服务
    /// </summary>
    public interface IPlatformServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        OnlineOrdersResultDto<List<Ppc_PlatformConfigOutPutDto>> GetPlatform();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<ComparePriceToCrmApiResultDto<List<CrmPurchaseOutPutDto>>> GetPurchaseBelongsFromCrm(PurchaseInputDto input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ComparePriceToCrmApiResultDto<List<CrmPurchaseOutPutDto>> GetPurchaseBelongsFromCrm1(PurchaseInputDto input);

        
    }
}
