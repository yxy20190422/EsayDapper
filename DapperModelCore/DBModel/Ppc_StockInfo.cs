using System;
using System.Collections.Generic;
using System.Text;
using Kogel.Dapper.Extension.Attributes;

namespace DapperModelCore.DBModel
{

    /// <summary>
    /// 
    /// </summary>
    public partial class Ppc_StockInfo
    {
        private long? _pID;
        private string _uploadNo;
        private string _model;
        private string _brand;
        private int? _invQty;
        private int? _sPQ;
        private int? _mOQ;
        private decimal? _costPrice;
        private string _batchNo;
        private string _encapsulation;
        private string _remark;
        private string _sZDelivery;
        private string _hKDelivery;
        private int? _status;
        private decimal? _minSalePrice;
        private decimal? _minProfit;
        private decimal? _profitRate;
        private int? _minProfitQty;
        private int? _negativeStockType;
        private int? _pricePlatCount;
        private DateTime? _createTime;
        private DateTime? _updateTime;
        private string _guid;


        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        [Identity]
        public long? PID
        {
            get
            {
                return _pID;
            }
            set
            {
                _pID = value;
            }
        }

        /// <summary>
        /// @"ppc_uploadinfo 表编号"
        /// </summary>
        [StringLength(20)]
        [Display(Name = @"ppc_uploadinfo 表编号")]
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
        /// @"型号"
        /// </summary>
        [StringLength(200)]
        [Display(Name = @"型号")]
        public string Model
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 200)
                {
                    _model = value.Substring(0, 200);
                }
            }
        }

        /// <summary>
        /// @"品牌"
        /// </summary>
        [StringLength(100)]
        [Display(Name = @"品牌")]
        public string Brand
        {
            get
            {
                return _brand;
            }
            set
            {
                _brand = value;
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 100)
                {
                    _brand = value.Substring(0, 100);
                }
            }
        }

        /// <summary>
        /// 库存量
        /// </summary>
        [Display(Name = @"库存量")]
        public int? InvQty
        {
            get
            {
                return _invQty;
            }
            set
            {
                _invQty = value;
            }
        }

        /// <summary>
        /// 最小包装量
        /// </summary>
        [Display(Name = @"最小包装量")]
        public int? SPQ
        {
            get
            {
                return _sPQ;
            }
            set
            {
                _sPQ = value;
            }
        }

        /// <summary>
        /// 最小起订量
        /// </summary>
        [Display(Name = @"最小起订量")]
        public int? MOQ
        {
            get
            {
                return _mOQ;
            }
            set
            {
                _mOQ = value;
            }
        }

        /// <summary>
        /// 成本价(供应商报价)
        /// </summary>
        [Display(Name = @"成本价(供应商报价)")]
        public decimal? CostPrice
        {
            get
            {
                return _costPrice;
            }
            set
            {
                _costPrice = value;
            }
        }

        /// <summary>
        /// @"批次"
        /// </summary>
        [StringLength(50)]
        [Display(Name = @"批次")]
        public string BatchNo
        {
            get
            {
                return _batchNo;
            }
            set
            {
                _batchNo = value;
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 50)
                {
                    _batchNo = value.Substring(0, 50);
                }
            }
        }

        /// <summary>
        /// @"封装"
        /// </summary>
        [StringLength(100)]
        [Display(Name = @"封装")]
        public string Encapsulation
        {
            get
            {
                return _encapsulation;
            }
            set
            {
                _encapsulation = value;
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 100)
                {
                    _encapsulation = value.Substring(0, 100);
                }
            }
        }

        /// <summary>
        /// @"备注"
        /// </summary>
        [StringLength(500)]
        [Display(Name = @"备注")]
        public string Remark
        {
            get
            {
                return _remark;
            }
            set
            {
                _remark = value;
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 500)
                {
                    _remark = value.Substring(0, 500);
                }
            }
        }

        /// <summary>
        /// @"深圳货期（格式：X-X 天）"
        /// </summary>
        [StringLength(20)]
        [Display(Name = @"深圳货期（格式：X-X 天）")]
        public string SZDelivery
        {
            get
            {
                return _sZDelivery;
            }
            set
            {
                _sZDelivery = value;
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 20)
                {
                    _sZDelivery = value.Substring(0, 20);
                }
            }
        }

        /// <summary>
        /// @"香港货期"
        /// </summary>
        [StringLength(20)]
        [Display(Name = @"香港货期")]
        public string HKDelivery
        {
            get
            {
                return _hKDelivery;
            }
            set
            {
                _hKDelivery = value;
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 20)
                {
                    _hKDelivery = value.Substring(0, 20);
                }
            }
        }

        /// <summary>
        /// 状态 0：无任务状态 1：与接口对接中 2：自查询处理中
        /// </summary>
        [Display(Name = @"状态 0：无任务状态 1：与接口对接中 2：自查询处理中")]
        public int? Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        /// <summary>
        /// 最低售价（阶梯价中的最低价格）
        /// </summary>
        [Display(Name = @"最低售价（阶梯价中的最低价格）")]
        public decimal? MinSalePrice
        {
            get
            {
                return _minSalePrice;
            }
            set
            {
                _minSalePrice = value;
            }
        }

        /// <summary>
        /// 最低毛利
        /// </summary>
        [Display(Name = @"最低毛利")]
        public decimal? MinProfit
        {
            get
            {
                return _minProfit;
            }
            set
            {
                _minProfit = value;
            }
        }

        /// <summary>
        /// 毛利率
        /// </summary>
        [Display(Name = @"毛利率")]
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

        /// <summary>
        /// 回本需售个数
        /// </summary>
        [Display(Name = @"回本需售个数")]
        public int? MinProfitQty
        {
            get
            {
                return _minProfitQty;
            }
            set
            {
                _minProfitQty = value;
            }
        }

        /// <summary>
        /// 负价差物料 1:单独展示 2 不单独展示
        /// </summary>
        [Display(Name = @"负价差物料 1:单独展示 2 不单独展示")]
        public int? NegativeStockType
        {
            get
            {
                return _negativeStockType;
            }
            set
            {
                _negativeStockType = value;
            }
        }

        /// <summary>
        /// 有价平台数量 1:1个  2:2个或2个以上
        /// </summary>
        [Display(Name = @"有价平台数量 1:1个  2:2个或2个以上")]
        public int? PricePlatCount
        {
            get
            {
                return _pricePlatCount;
            }
            set
            {
                _pricePlatCount = value;
            }
        }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = @"创建日期")]
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

        /// <summary>
        /// @""
        /// </summary>
        [StringLength(50)]
        [Display(Name = @"")]
        public string Guid
        {
            get
            {
                return _guid;
            }
            set
            {
                _guid = value;
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 50)
                {
                    _guid = value.Substring(0, 50);
                }
            }
        }

    }

}
