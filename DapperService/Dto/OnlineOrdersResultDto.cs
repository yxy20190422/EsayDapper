using System;
using System.Collections.Generic;
using System.Text;

namespace DapperService.Dto
{
    /// <summary>
    /// 结果集Dto
    /// </summary>
    [Serializable]
    public class OnlineOrdersResultDto
    {
        /// <summary>
        /// 状态码：0.失败、1:成功、2:异常
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }
    /// <summary>
    /// 结果集Dto
    /// </summary>
    [Serializable]
    public class OnlineOrdersResultDto<T> : OnlineOrdersResultDto
    {
        /// <summary>
        /// 扩展字段
        /// </summary>
        public T Ext1 { get; set; }
    }
}
