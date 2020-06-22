using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperModelCore.DapperCore;
using DapperModelCore.DBModel;
using DapperService.Dto;
using DapperService.IService;
using DapperUtilTool.CoreModels;
using Kogel.Dapper.Extension.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DapperWebAPI.Controllers
{
    /// <summary>
    /// 物料上传的控制器
    /// </summary>
    [AllowAnonymous]
    public class StockController : ApiControllerBase
    {
        /// <summary>
        /// 仓储接口
        /// </summary>
        private IRepository _iRepository { get; set; }
        /// <summary>
        /// 物料的服务
        /// </summary>
        private IStockServices _service { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>       
        public StockController(IStockServices services)
        {
            _service = services;
        }

        /// <summary>
        /// 上传保存物料
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost]
        public OnlineOrdersResultDto UploadStock(UploadStockInputDto inputDto)
        {
            OnlineOrdersResultDto result = new OnlineOrdersResultDto() { StatusCode = 0, Message = "保存数据失败" };
            inputDto.FileName = "d9d7556c-6283-437c-8b4e-1ac9bb921a47.xlsx";
            inputDto.Remark = "测试";
            inputDto.UpFileds = new List<UpLoadHead>() { 
                new UpLoadHead(){Key="型号",Value="物料型号" },
                 new UpLoadHead(){Key="品牌",Value="物料型号" },
                  new UpLoadHead(){Key="库存",Value="" },
                   new UpLoadHead(){Key="SPQ",Value="SPQ" },
                    new UpLoadHead(){Key="MOQ",Value="MOQ" },
                     new UpLoadHead(){Key="供应商报价",Value="供应商报价" },
                     new UpLoadHead(){Key="批次",Value="批次" },
                        new UpLoadHead(){Key="封装",Value="封装" },
                        new UpLoadHead(){Key="描述",Value="描述" },
                         new UpLoadHead(){Key="货期(SZ)",Value="货期(SZ)" },
                           new UpLoadHead(){Key="货期(HK)",Value="货期(HK)" },
                           new UpLoadHead(){Key="阶梯",Value="阶梯" },
            };
            result = _service.UploadStock(inputDto);
            return result;
        }

        /// <summary>
        /// 平台价格比对分页功能
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public OnlineOrdersResultDto<PageInfo<Ppc_UploadInfoOutPutPageListDto>> GetPageUploadinfo(Ppc_UploadInfoInputPageListDto input)
        {
            OnlineOrdersResultDto<PageInfo<Ppc_UploadInfoOutPutPageListDto>> res = new OnlineOrdersResultDto<PageInfo<Ppc_UploadInfoOutPutPageListDto>>()
            {
                StatusCode = 0,
                Message = "获取分页信息失败"
            };
            var result = _service.GetPageUploadinfo(input);
            return result;
        }

        #region 根据条件获取价格比对分页详情
        /// <summary>
        /// 根据条件获取上传单号分页详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public OnlineOrdersResultDto<PageInfo<Ppc_StockInfoOutputPageListDto>> GetPageStock(Ppc_StockInfoInputPageListDto input)
        {
            OnlineOrdersResultDto<PageInfo<Ppc_StockInfoOutputPageListDto>> res = new OnlineOrdersResultDto<PageInfo<Ppc_StockInfoOutputPageListDto>>()
            {
                StatusCode = 0,
                Message = "获取分页信息失败",
                Ext1 = new PageInfo<Ppc_StockInfoOutputPageListDto>()

            };
            res = _service.GetPageStock(input);
            return res;
        }
        #endregion


        #region 新增价格策略
        /// <summary>
        /// 新增价格策略
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public OnlineOrdersResultDto AddPriceConfig(Ppc_PriceConfigInputDto input)
        {
            OnlineOrdersResultDto res = new OnlineOrdersResultDto()
            {
                StatusCode = 0,
                Message = "统一定价配置失败"
            };
            res = _service.AddPriceConfig(input);
            return res;
        }
        #endregion





        #region ExportStock
        /// <summary>
        /// 导出Excel功能开发
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public OnlineOrdersResultDto<string> ExportStock(Ppc_PriceConfigInputDto input)
        {
            OnlineOrdersResultDto<string> res = new OnlineOrdersResultDto<string>()
            {
                StatusCode = 0,
                Message = "excel导出失败",
            };
            var updatetor = _iRepository.FindEntity<Ppc_UploadInfo>(m => m.UploadNO == input.UploadNo);
            if (updatetor == null)
            {
                res.Message = "上传记录未找到";
                return res;
            }
            if (updatetor.Status != 0)
            {
                res.Message = "物料正在比对中,请等程序运行完成后再导出上传的库存";
                return res;
            }
            res.StatusCode = 1;
            res.Message = "excel导出进行中";
            res.Ext1 = "/Export/ExportStockUpload?UploadNo=" + input.UploadNo;
            return res;
        }
        #endregion


    }
}