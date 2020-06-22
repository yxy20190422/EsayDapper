using System;
using System.Collections.Generic;
using System.Text;
using Kogel.Dapper.Extension.Attributes;

namespace DapperModelCore.DBModel
{

    /// <summary>
    /// 
    /// </summary>
    public partial class Ppc_PriceConfig
    {
        private int? _iD;
        private string _uploadNo;
        private int? _adjustType;
        private decimal? _maxProfitRate;
        private decimal? _minProfitRate;
        private decimal? _priceRatio;
        private decimal? _changeProfitRate;
        private string _comparePlatform;
        private int? _isUnpacking;
        private int? _isPriceLadder;
        private int? _isSupplyLadder;
        private decimal? _RecommendAddPRate;
        private int? _isDelete;
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
        /// @"上传编号"
        /// </summary>
        [StringLength(20)]
        [Display(Name = @"上传编号")]
        public string UploadNo
        {
            get
            {
                return _uploadNo;
            }
            set
            {
                _uploadNo = value;
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 20)
                {
                    _uploadNo = value.Substring(0, 20);
                }
            }
        }

        /// <summary>
        /// 调整方式 1：自动调价 2：手动调价
        /// </summary>
        [Display(Name = @"调整方式 1：自动调价 2：手动调价")]
        public int? AdjustType
        {
            get
            {
                return _adjustType;
            }
            set
            {
                _adjustType = value;
            }
        }

        /// <summary>
        /// 最高毛利率
        /// </summary>
        [Display(Name = @"最高毛利率")]
        public decimal? MaxProfitRate
        {
            get
            {
                return _maxProfitRate;
            }
            set
            {
                _maxProfitRate = value;
            }
        }

        /// <summary>
        /// 最低毛利率
        /// </summary>
        [Display(Name = @"最低毛利率")]
        public decimal? MinProfitRate
        {
            get
            {
                return _minProfitRate;
            }
            set
            {
                _minProfitRate = value;
            }
        }

        /// <summary>
        /// 跟价系数
        /// </summary>
        [Display(Name = @"跟价系数")]
        public decimal? PriceRatio
        {
            get
            {
                return _priceRatio;
            }
            set
            {
                _priceRatio = value;
            }
        }

        /// <summary>
        /// 手动调价的毛利率
        /// </summary>
        [Display(Name = @"手动调价的毛利率")]
        public decimal? ChangeProfitRate
        {
            get
            {
                return _changeProfitRate;
            }
            set
            {
                _changeProfitRate = value;
            }
        }

        /// <summary>
        /// @"对标的平台（逗号隔开）"
        /// </summary>
        [StringLength(50)]
        [Display(Name = @"对标的平台（逗号隔开）")]
        public string ComparePlatform
        {
            get
            {
                return _comparePlatform;
            }
            set
            {
                _comparePlatform = value;
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 50)
                {
                    _comparePlatform = value.Substring(0, 50);
                }
            }
        }

        /// <summary>
        /// 是否可拆包 1：可拆包 2：不可拆包
        /// </summary>
        [Display(Name = @"是否可拆包 1：可拆包 2：不可拆包")]
        public int? IsUnpacking
        {
            get
            {
                return _isUnpacking;
            }
            set
            {
                _isUnpacking = value;
            }
        }

        /// <summary>
        /// 是否使用平台价格梯度 0：不使用 1：使用（配合 IsUpacking = 2 使用）
        /// </summary>
        [Display(Name = @"是否使用平台价格梯度 0：不使用 1：使用（配合 IsUpacking = 2 使用）")]
        public int? IsPriceLadder
        {
            get
            {
                return _isPriceLadder;
            }
            set
            {
                _isPriceLadder = value;
            }
        }

        /// <summary>
        /// 是否使用供应商的梯度0：不用 1：用（配合 IsUpacking = 2 使用）
        /// </summary>
        [Display(Name = @"是否使用供应商的梯度0：不用 1：用（配合 IsUpacking = 2 使用）")]
        public int? IsSupplyLadder
        {
            get
            {
                return _isSupplyLadder;
            }
            set
            {
                _isSupplyLadder = value;
            }
        }

        /// <summary>
        /// 推荐递增毛利率
        /// </summary>
        [Display(Name = @"推荐递增毛利率")]
        public decimal? RecommendAddPRate
        {
            get
            {
                return _RecommendAddPRate;
            }
            set
            {
                _RecommendAddPRate = value;
            }
        }

        /// <summary>
        /// 0:已作废 1 有效
        /// </summary>
        [Display(Name = @"0:已作废 1 有效")]
        public int? IsDelete
        {
            get
            {
                return _isDelete;
            }
            set
            {
                _isDelete = value;
            }
        }

    }

}
