using System;
using System.Collections.Generic;
using System.Text;
using Kogel.Dapper.Extension.Attributes;

namespace DapperModelCore.DBModel
{

    /// <summary>
    /// 
    /// </summary>
    public partial class Ppc_StockinfoPrice
    {
        private long? _iD;
        private long? _stockID;
        private int? _maxVal;
        private decimal? _cNYPrice;
        private decimal? _uSDPrice;
        private DateTime? _updateTime;


        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        [Identity]
        public long? ID
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
        /// 
        /// </summary>
        [Display(Name = @"")]
        public long? StockID
        {
            get
            {
                return _stockID;
            }
            set
            {
                _stockID = value;
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
        /// 阶梯人民币价
        /// </summary>
        [Display(Name = @"阶梯人民币价")]
        public decimal? CNYPrice
        {
            get
            {
                return _cNYPrice;
            }
            set
            {
                _cNYPrice = value;
            }
        }

        /// <summary>
        /// 阶梯美金价
        /// </summary>
        [Display(Name = @"阶梯美金价")]
        public decimal? USDPrice
        {
            get
            {
                return _uSDPrice;
            }
            set
            {
                _uSDPrice = value;
            }
        }

        /// <summary>
        /// 更新日期
        /// </summary>
        [Display(Name = @"更新日期")]
        public DateTime? UpdateTime
        {
            get
            {
                return _updateTime;
            }
            set
            {
                _updateTime = value;
            }
        }

    }

}
