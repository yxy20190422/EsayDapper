using System;
using System.Collections.Generic;
using System.Text;

namespace DapperService.Dto
{
    /// <summary>
    /// 导出物料上传的模型
    /// </summary>
    public class Ppc_StockInfoExportDto
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
        ///  库存量
        /// </summary>
        public int? InvQty
        {
            get;
            set;
        }

        /// <summary>
        ///  最小包装量
        /// </summary>
        public int? SPQ
        {
            get;
            set;
        }

        /// <summary>
        ///  最小起订量
        /// </summary>
        public int? MOQ
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
        ///  批次
        /// </summary>
        public string BatchNo
        {
            get;
            set;
        }

        /// <summary>
        ///  封装
        /// </summary>
        public string Encapsulation
        {
            get;
            set;
        }

        /// <summary>
        ///  备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

        /// <summary>
        ///  深圳货期（格式：X-X 天）
        /// </summary>
        public string SZDelivery
        {
            get;
            set;
        }

        /// <summary>
        ///  香港货期
        /// </summary>
        public string HKDelivery
        {
            get;
            set;
        }
    }
}
