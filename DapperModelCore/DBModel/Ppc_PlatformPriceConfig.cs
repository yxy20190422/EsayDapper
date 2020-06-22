using System;
using System.Collections.Generic;
using System.Text;
using Kogel.Dapper.Extension.Attributes;

namespace DapperModelCore.DBModel
{

    /// <summary>
    /// 
    /// </summary>
    public partial class Ppc_PlatformPriceConfig
    {
        private int? _iD;
        private int? _priceConfigID;
        private int? _maxVal;
        private decimal? _profitRate;


        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        [Identity]
        public int? ID
        {
            get
            {
                return _iD;
            }
            set
            {
                _iD = value;
            }
        }

        /// <summary>
        /// ppc_Priceconfig表ID
        /// </summary>
        [Display(Name = @"ppc_Priceconfig表ID")]
        public int? PriceConfigID
        {
            get
            {
                return _priceConfigID;
            }
            set
            {
                _priceConfigID = value;
            }
        }

        /// <summary>
        /// 阶梯数量
        /// </summary>
        [Display(Name = @"阶梯数量")]
        public int? MaxVal
        {
            get
            {
                return _maxVal;
            }
            set
            {
                _maxVal = value;
            }
        }

        /// <summary>
        /// 阶梯毛利率
        /// </summary>
        [Display(Name = @"阶梯毛利率")]
        public decimal? ProfitRate
        {
            get
            {
                return _profitRate;
            }
            set
            {
                _profitRate = value;
            }
        }

    }

}
