using System;
using System.Collections.Generic;
using System.Text;
using Kogel.Dapper.Extension.Attributes;

namespace DapperModelCore.DBModel
{

    /// <summary>
    /// 
    /// </summary>
    public partial class Ppc_PlatformPrice
    {
        private long? _iD;
        private long? _stockID;
        private int? _platformID;
        private int? _maxVal;
        private DateTime? _createTime;
        private decimal? _cNYPrice;
        private decimal? _uSDPrice;
        private string _platformType;


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
        /// ppc_stockinfo表ID
        /// </summary>
        [Display(Name = @"ppc_stockinfo表ID")]
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
        /// ppc_platformconfig 表ID
        /// </summary>
        [Display(Name = @"ppc_platformconfig 表ID")]
        public int? PlatformID
        {
            get
            {
                return _platformID;
            }
            set
            {
                _platformID = value;
            }
        }

        /// <summary>
        /// 阶梯数量最大值
        /// </summary>
        [Display(Name = @"阶梯数量最大值")]
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
        /// 创建时间
        /// </summary>
        [Display(Name = @"创建时间")]
        public DateTime? CreateTime
        {
            get
            {
                return _createTime;
            }
            set
            {
                _createTime = value;
            }
        }

        /// <summary>
        /// 人民币价
        /// </summary>
        [Display(Name = @"人民币价")]
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
        /// 美金价
        /// </summary>
        [Display(Name = @"美金价")]
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
        /// @"平台类型：LC(力创)，YH（云汉）"
        /// </summary>
        [StringLength(11)]
        [Display(Name = @"平台类型：LC(力创)，YH（云汉）")]
        public string PlatformType
        {
            get
            {
                return _platformType;
            }
            set
            {
                _platformType = value;
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 11)
                {
                    _platformType = value.Substring(0, 11);
                }
            }
        }

    }

}
