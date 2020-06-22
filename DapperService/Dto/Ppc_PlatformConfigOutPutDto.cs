using System;
using System.Collections.Generic;
using System.Text;

namespace DapperService.Dto
{

    /// <summary>
    /// 
    /// </summary>
    public partial class Ppc_PlatformConfigOutPutDto
    {

        /// <summary>
        ///  平台ID
        /// </summary>
        public int? ID
        {
            get;
            set;
        }

        /// <summary>
        ///  平台名称(中文名)
        /// </summary>
        public string PlatformName
        {
            get;
            set;
        }

    }

}
