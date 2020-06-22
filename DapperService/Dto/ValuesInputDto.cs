using System;
using System.Collections.Generic;
using System.Text;

namespace DapperService.Dto
{
    /// <summary>
    /// 输入值参数
    /// </summary>
    public class ValuesInputDto
    {
        /// <summary>
        /// 输入ID
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 键值
        /// </summary>
        public string Keys { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Vals { get; set; }
    }
}
