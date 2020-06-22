using System;
using System.Collections.Generic;
using System.Text;
using Kogel.Dapper.Extension.Attributes;

namespace DapperModelCore.DBModel
{

    /// <summary>
    /// 
    /// </summary>
    public partial class Ppc_PlatformDiffprice
    {
        private long? _iD;
        private long? _stockID;
        private string _name;
        private decimal? _minPlatformPrice;
        private decimal? _priceDiff;
        private string _currency;
        private int? _platformid;


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
        /// PPC_StockInfo 表ID
        /// </summary>
        [Display(Name = @"PPC_StockInfo 表ID")]
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
        /// @"名称"
        /// </summary>
        [StringLength(255)]
        [Display(Name = @"名称")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 255)
                {
                    _name = value.Substring(0, 255);
                }
            }
        }

        /// <summary>
        /// 最小平台价格
        /// </summary>
        [Display(Name = @"最小平台价格")]
        public decimal? MinPlatformPrice
        {
            get
            {
                return _minPlatformPrice;
            }
            set
            {
                _minPlatformPrice = value;
            }
        }

        /// <summary>
        /// 价差（MinPlatformPrice-物料最低售价）
        /// </summary>
        [Display(Name = @"价差（MinPlatformPrice-物料最低售价）")]
        public decimal? PriceDiff
        {
            get
            {
                return _priceDiff;
            }
            set
            {
                _priceDiff = value;
            }
        }

        /// <summary>
        /// @"币别"
        /// </summary>
        [StringLength(10)]
        [Display(Name = @"币别")]
        public string Currency
        {
            get
            {
                return _currency;
            }
            set
            {
                _currency = value;
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 10)
                {
                    _currency = value.Substring(0, 10);
                }
            }
        }

        /// <summary>
        /// PPC_platformconfig表ID
        /// </summary>
        [Display(Name = @"PPC_platformconfig表ID")]
        public int? Platformid
        {
            get
            {
                return _platformid;
            }
            set
            {
                _platformid = value;
            }
        }

    }

}
