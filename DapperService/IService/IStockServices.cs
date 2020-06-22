using DapperService.Dto;
using DapperService.Export;
using DapperUtilTool.CoreModels;
using Kogel.Dapper.Extension.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperService.IService
{
    /// <summary>
    /// 物料上传服务
    /// </summary>
    public interface IStockServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        OnlineOrdersResultDto UploadStock(UploadStockInputDto inputDto);
        /// <summary>
        /// 平台价格比对分页功能
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OnlineOrdersResultDto<PageInfo<Ppc_UploadInfoOutPutPageListDto>> GetPageUploadinfo(Ppc_UploadInfoInputPageListDto input);
        /// <summary>
        /// 根据比较单号获取上传明细信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OnlineOrdersResultDto<PageInfo<Ppc_StockInfoOutputPageListDto>> GetPageStock(Ppc_StockInfoInputPageListDto input);
        /// <summary>
        ///统一配置价格策略
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OnlineOrdersResultDto AddPriceConfig(Ppc_PriceConfigInputDto input);

        /// <summary>
        /// 导出上传库存用的Excel
        /// </summary>
        /// <param name="UploadNo">上传单号</param>
        /// <returns></returns>
        List<StockUploadModel> ExportUploadOutData(string  UploadNo);
    }
}
