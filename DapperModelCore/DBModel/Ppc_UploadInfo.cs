using System;
using System.Collections.Generic;
using System.Text;
using Kogel.Dapper.Extension.Attributes;

namespace DapperModelCore.DBModel
{

    /// <summary>
    /// 
    /// </summary>
    public partial class Ppc_UploadInfo
    {
        private int? _iD;
        private string _uploadNO;
        private string _des;
        private int? _status;
        private DateTime? _createTime;
        private string _createID;
        private string _createName;
        private DateTime? _updateTime;


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
        /// @"编号"
        /// </summary>
        [StringLength(20)]
        [Display(Name = @"编号")]
        public string UploadNO
        {
            get
            {
                return _uploadNO;
            }
            set
            {
                _uploadNO = value;
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 20)
                {
                    _uploadNO = value.Substring(0, 20);
                }
            }
        }

        /// <summary>
        /// @"上传描述"
        /// </summary>
        [StringLength(200)]
        [Display(Name = @"上传描述")]
        public string Des
        {
            get
            {
                return _des;
            }
            set
            {
                _des = value;
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 200)
                {
                    _des = value.Substring(0, 200);
                }
            }
        }

        /// <summary>
        /// 状态：0：无任务 1：待对比 2：对比中
        /// </summary>
        [Display(Name = @"状态：0：无任务 1：待对比 2：对比中")]
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
        /// @"创建ID"
        /// </summary>
        [StringLength(20)]
        [Display(Name = @"创建ID")]
        public string CreateID
        {
            get
            {
                return _createID;
            }
            set
            {
                _createID = value;
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 20)
                {
                    _createID = value.Substring(0, 20);
                }
            }
        }

        /// <summary>
        /// @"创建人名称"
        /// </summary>
        [StringLength(20)]
        [Display(Name = @"创建人名称")]
        public string CreateName
        {
            get
            {
                return _createName;
            }
            set
            {
                _createName = value;
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 20)
                {
                    _createName = value.Substring(0, 20);
                }
            }
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Display(Name = @"更新时间")]
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
