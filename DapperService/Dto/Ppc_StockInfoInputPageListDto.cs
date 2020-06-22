using System;
using System.Collections.Generic;
using System.Text;

namespace DapperService.Dto
{

    /// <summary>
    /// 
    /// </summary>
    public partial class Ppc_StockInfoInputPageListDto : PageEntity
    {
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
        ///  最小最低毛利
        /// </summary>
        public decimal? MinProfitBegin
        {
            get;
            set;
        }
        /// <summary>
        ///  最大最低毛利
        /// </summary>
        public decimal? MinProfitEnd
        {
            get;
            set;
        }

        /// <summary>
        ///  最小毛利率
        /// </summary>
        public decimal? ProfitRateBegin
        {
            get;
            set;
        }

        /// <summary>
        ///  最大毛利率
        /// </summary>
        public decimal? ProfitRateEnd
        {
            get;
            set;
        }
        /// <summary>
        /// 选项卡的付差价物料 1：代表选项卡选中负差价物料 0:未选中
        /// </summary>
        public int? HeadNegativeStockType
        {
            get;
            set;
        }
        /// <summary>
        /// 选项卡的有价格平台数量 1：代表选项卡选中有价格平台数量 0:未选中
        /// </summary>
        public int? HeadPricePlatCount
        {
            get;
            set;
        }

        /// <summary>
        ///  负价差物料 1:单独展示 2 不单独展示
        /// </summary>
        public int? NegativeStockType
        {
            get;
            set;
        }

        /// <summary>
        ///  有价平台数量 1:1个  2:2个或2个以上
        /// </summary>
        public int? PricePlatCount
        {
            get;
            set;
        }


    }

}
