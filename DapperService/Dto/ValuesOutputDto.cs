using DapperUtilTool.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperService.Dto
{
    /// <summary>
    /// 输出的实体对象
    /// </summary>
    public class ValuesOutputDto
    {
        /// <summary>
        /// 输入ID
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Ignore]
        public string Des
        {
            get
            {
                if (Id == 11) { return "Hello,World"; }
                return string.Empty;
            }
        }
    }
}
