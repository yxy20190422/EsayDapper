using DapperUtilTool.Common;
using DapperUtilTool.CoreModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperService.Dto
{

    /// <summary>
    /// 
    /// </summary>
    public partial class Ppc_UploadInfoOutPutPageListDto
    {

        /// <summary>
        ///  
        /// </summary>
        public int? ID
        {
            get;
            set;
        }

        /// <summary>
        ///  编号
        /// </summary>
        public string UploadNO
        {
            get;
            set;
        }

        /// <summary>
        ///  标题
        /// </summary>
        public string Des
        {
            get;
            set;
        }

        /// <summary>
        ///  状态：0：无任务 1：对比中
        /// </summary>
        public int? Status
        {
            get;
            set;
        }
        /// <summary>
        /// 状态说明
        /// </summary>
        [Ignore]
        public string StatusStr
        {
            get
            {
                if (Status.GetValueOrDefault(0) > 0)
                {
                    return EnumCenter.GetDescription<EnumCenter.Ppc_UploadInfo_Status>(Status);
                }
                return string.Empty;
            }
        }

        /// <summary>
        ///  创建日期
        /// </summary>
        public DateTime? CreateTime
        {
            get;
            set;
        }

        /// <summary>
        ///  创建人名称
        /// </summary>
        public string CreateName
        {
            get;
            set;
        }

    }
}
