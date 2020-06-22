using System;
using System.Collections.Generic;
using System.Text;

namespace DapperService.Dto
{
    /// <summary>
    /// 统一价格定价输入参数设置
    /// </summary>
    public class Ppc_PriceConfigInputDto
    {
        /// <summary>
        /// 上传编号必须传值
        /// </summary>
        public string UploadNo { get; set; }
        /// <summary>
        /// 调整范围:1:勾选物料 2:代表筛选的结果
        /// </summary>
        public int? AdjustArange { get; set; }
        /// <summary>
        /// 调整方式 1：自动调价 2：手动调价
        /// </summary>
        public int? AdjustType { get; set; }
        /// <summary>
        /// 调整范围为1时,此字段须有值。库存分页PID的集合,调整范围为2时,此字段可能没有值主要看页面是否勾选详情页面PID。
        /// </summary>
        public long?[] StockIDArray { get; set; }

        /// <summary>
        ///  调整范围为2筛选的结果时,此栏位才有值
        /// </summary>
        public string Model
        {
            get;
            set;
        }

        /// <summary>
        ///   调整范围为2筛选的结果时,此栏位才有值
        /// </summary>
        public string Brand
        {
            get;
            set;
        }

        /// <summary>
        ///   调整范围为2筛选的结果时,此栏位才有值
        /// </summary>
        public decimal? MinProfitBegin
        {
            get;
            set;
        }
        /// <summary>
        ///   调整范围为2筛选的结果时,此栏位才有值
        /// </summary>
        public decimal? MinProfitEnd
        {
            get;
            set;
        }

        /// <summary>
        ///   调整范围为2筛选的结果时,此栏位才有值
        /// </summary>
        public decimal? ProfitRateBegin
        {
            get;
            set;
        }

        /// <summary>
        ///   调整范围为2筛选的结果时,此栏位才有值
        /// </summary>
        public decimal? ProfitRateEnd
        {
            get;
            set;
        }
        /// <summary>
        /// 调整范围为2筛选的结果时,此栏位才有值 选项卡的付差价物料 1：代表选项卡选中负差价物料 0:未选中
        /// </summary>
        public int? HeadNegativeStockType
        {
            get;
            set;
        }
        /// <summary>
        ///调整范围为2筛选的结果时,此栏位才有值 选项卡的有价格平台数量 1：代表选项卡选中有价格平台数量 0:未选中
        /// </summary>
        public int? HeadPricePlatCount
        {
            get;
            set;
        }

        /// <summary>
        /// 调整范围为2筛选的结果时,此栏位才有值 负价差物料 1:单独展示 2 不单独展示
        /// </summary>
        public int? NegativeStockType
        {
            get;
            set;
        }

        /// <summary>
        /// 调整范围为2筛选的结果时,此栏位才有值有价平台数量 1:1个  2:2个或2个以上
        /// </summary>
        public int? PricePlatCount
        {
            get;
            set;
        }
        /// <summary>
        ///调整方式AdjustType为1：自动调价 最高毛利率才有可能需要传值
        /// </summary>
        public decimal? MaxProfitRate { get; set; }
        /// <summary>
        ///调整方式AdjustType为1：自动调价 最低毛利率才有可能需要传值 
        /// </summary>
        public decimal? MinProfitRate { get; set; }
        /// <summary>
        /// 调整方式AdjustType为1：自动调价 跟价系数才有可能需要传值 
        /// </summary>
        public decimal? PriceRatio { get; set; }
        /// <summary>
        /// 调整方式AdjustType为1：自动调价 勾选的对标平台
        /// </summary>
        public int?[] PlatformIDArray { get; set; }
        /// <summary>
        ///调整方式AdjustType为1：自动调价 拆包服务 1：可拆包 2：不可拆包
        /// </summary>
        public int? IsUnpacking { get; set; }
        /// <summary>
        /// 调整方式AdjustType为1：自动调价 是否使用价格梯度0：使用 1：不使用（配合 IsUpacking = 2 使用）
        /// </summary>
        public int? IsPriceLadder { get; set; }
        /// <summary>
        /// 供应商价格阶梯（显示条件：当不可拆包时。才显示这个供应商阶梯复选框）
        /// </summary>
        public int? IsSupplyLadder { get; set; }

        /// <summary>
        /// 当拆包服务为可拆包,并且使用价格梯度IsPriceLadder为1不使用（未勾选）时。这个价格梯度才有值
        /// </summary>
        public List<CustomerPlatformList> CustermerCongigList { get; set; }
        /// <summary>
        /// 当拆包服务为不可拆包,并且使用供应商预设阶梯为1不使用（未勾选）时。这个价格梯度才有值
        /// </summary>
        public List<CustomerSupplierList> CustomerSupplierList { get; set; }
        /// <summary>
        /// 当拆包服务为不可拆包,并且使用供应商预设阶梯为0使用（勾选）时。这个推荐毛利率才出现
        /// </summary>
        public decimal? AddSupllierRate { get; set; }

        /// <summary>
        /// 调整方式AdjustType为2：手动调价时当前毛利率才有值
        /// </summary>
        public decimal? CurrentProfitRate { get; set; }
    }
    /// <summary>
    /// 自定义平台阶梯价格
    /// </summary>
    public class CustomerPlatformList
    {
        /// <summary>
        ///阶梯数量
        /// </summary>
        public int? Qty { get; set; }
        /// <summary>
        ///毛利率
        /// </summary>
        public decimal? ProfitRate { get; set; }
    }
    /// <summary>
    /// 自定义价格
    /// </summary>
    public class CustomerSupplierList
    {
        /// <summary>
        ///SPQ的倍数
        /// </summary>
        public int? Qty { get; set; }
        /// <summary>
        ///毛利率
        /// </summary>
        public decimal? ProfitRate { get; set; }
    }
}
