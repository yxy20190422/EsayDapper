using System;
using System.Collections.Generic;
using System.Text;
using Kogel.Dapper.Extension.Attributes;

namespace DapperModelCore.DBModel
{

    /// <summary>
    /// 
    /// </summary>
    public partial class Ppc_PlatformConfig
    {
        private int? _iD;
        private string _platformName;
        private int? _isShow;
        private DateTime? _createTime;
        private string _platformType;


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
        /// @"平台名称(中文名)"
        /// </summary>
        [StringLength(50)]
        [Display(Name = @"平台名称(中文名)")]
        public string PlatformName
        {
            get
            {
                return _platformName;
            }
            set
            {
                _platformName = value;
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 50)
                {
                    _platformName = value.Substring(0, 50);
                }
            }
        }

        /// <summary>
        /// 是否使用该平台数据1 使用 0不使用
        /// </summary>
        [Display(Name = @"是否使用该平台数据1 使用 0不使用")]
        public int? IsShow
        {
            get
            {
                return _isShow;
            }
            set
            {
                _isShow = value;
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
