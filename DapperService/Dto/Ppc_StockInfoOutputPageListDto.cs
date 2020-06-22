using DapperModelCore.DBModel;
using DapperUtilTool.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperService.Dto
{

    /// <summary>
    /// 
    /// </summary>
    public partial class Ppc_StockInfoOutputPageListDto
    {

        /// <summary>
        ///  
        /// </summary>
        public long? PID
        {
            get;
            set;
        }

        /// <summary>
        ///  ppc_uploadinfo 表编号
        /// </summary>
        public string UploadNo
        {
            get;
            set;
        }

        /// <summary>
        ///  型号
        /// </summary>
        public string Model
        {
            get;
            set;
        }

        /// <summary>
        ///  品牌
        /// </summary>
        public string Brand
        {
            get;
            set;
        }

        /// <summary>
        ///  SPQ
        /// </summary>
        public int? SPQ
        {
            get;
            set;
        }

        /// <summary>
        ///  成本价(供应商报价)
        /// </summary>
        public decimal? CostPrice
        {
            get;
            set;
        }

        /// <summary>
        ///  状态 0：无任务状态 1：与接口对接中 2：自查询处理中
        /// </summary>
        public int? Status
        {
            get;
            set;
        }

        /// <summary>
        ///  最低售价（为价格梯度的最低售价）
        /// </summary>
        public decimal? MinSalePrice
        {
            get;
            set;
        }

        /// <summary>
        ///  最低毛利
        /// </summary>
        public decimal? MinProfit
        {
            get;
            set;
        }

        /// <summary>
        ///  毛利率
        /// </summary>
        public decimal? ProfitRate
        {
            get;
            set;
        }
        /// <summary>
        /// 毛利率百分比
        /// </summary>
        public string ProfitRateStr {
            get;
            set;
        }

        /// <summary>
        ///  回本需售个数
        /// </summary>
        public int? MinProfitQty
        {
            get;
            set;
        }
        /// <summary>
        /// 批次
        /// </summary>
        public string BatchNo { get; set; }

        /// <summary>
        /// 封装
        /// </summary>
        public string Encapsulation { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public int InvQty { get; set; }
        /// <summary>
        /// 最小起订量
        /// </summary>
        public int MOQ { get; set; }
        /// <summary>
        /// 货期深圳
        /// </summary>
        public string SZDelivery { get; set; }
        /// <summary>
        /// 货期深圳
        /// </summary>
        public string HKDelivery { get; set; }
        /// <summary>
        /// 最低售价旁边的价格梯度数量2000+
        /// </summary>
        [Ignore]
        public string QtyStr { get; set; }
        /// <summary>
        /// 物料价格梯度
        /// </summary>
        [Ignore]
        public List<Ppc_StockinfoPriceGradientDto> SIDGradient { get;set;}
        /// <summary>
        /// 第三方平台价差信息表
        /// </summary>
        [Ignore]
        public List<PlatformStockList> OtherPlatformList { get; set; }

    }
    /// <summary>
    /// 第三方价差实体
    /// </summary>
    public class Ppc_StockinfoPriceGradientDto
    {
        /// <summary>
        /// 阶梯数量最大值
        /// </summary>
        public int? MaxVal
        {
            get; set;
        }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal? CNYPrice
        {
            get; set;
        }
    }


    /// <summary>
    ///平台物料价差表
    /// </summary>
    public class PlatformStockList
    {
        /// <summary>
        /// 平台ID
        /// </summary>
        public int Platformid { get; set; }
        /// <summary>
        /// 平台名称（前端表头展示Tiltle）
        /// </summary>
        public string TiltleName { get; set; }
        /// <summary>
        /// 第三方最低单价
        /// </summary>
        public decimal? MinPlatformPrice { get; set; }
        /// <summary>
        /// 第三方价格差
        /// </summary>
        public decimal? PriceDiff { get; set; }
        /// <summary>
        /// 第三方价差表
        /// </summary>
        public List<Ppc_PlatformPriceGradientDto> GradientList { get; set; }
    }

    /// <summary>
    /// 第三方价格梯度信息
    /// </summary>
    public class Ppc_PlatformPriceGradientDto
    {
        /// <summary>
        /// 阶梯数量最大值
        /// </summary>
        public int? MaxVal
        {
            get; set;
        }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal? CNYPrice
        {
            get; set;
        }
    }

}
