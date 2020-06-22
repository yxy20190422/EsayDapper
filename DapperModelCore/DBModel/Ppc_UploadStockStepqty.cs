using System;
using System.Collections.Generic;
using System.Text;
using Kogel.Dapper.Extension.Attributes;

namespace DapperModelCore.DBModel
{

    /// <summary>
    /// 
    /// </summary>
    public partial class Ppc_UploadStockStepqty
    {
        private long? _iD;
        private int? _maxVal;
        private DateTime? _updateTime;
        private string _uploadNo;
        private string _stockinfoGuid;


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
        /// @"物料上传编号"
        /// </summary>
        [StringLength(20)]
        [Display(Name = @"物料上传编号")]
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
        /// @""
        /// </summary>
        [StringLength(50)]
        [Display(Name = @"")]
        public string StockinfoGuid
        {
            get
            {
                return _stockinfoGuid;
            }
            set
            {
                _stockinfoGuid = value;
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 50)
                {
                    _stockinfoGuid = value.Substring(0, 50);
                }
            }
        }

    }

}
