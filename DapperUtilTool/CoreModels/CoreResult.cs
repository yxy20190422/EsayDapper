using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUtilTool.CoreModels
{
    /// <summary>
    /// 核心结果集
    /// </summary>
    [Serializable]
    public class CoreResult
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
    /// 核心结果集
    /// </summary>
    [Serializable]
    public class CoreResult<T> : CoreResult
    {
        /// <summary>
        /// 扩展字段
        /// </summary>
        public T Ext1 { get; set; }
    }
    /// <summary>
    /// 核心结果集
    /// </summary>
    [Serializable]
    public class CoreResult<T1, T2> : CoreResult<T1>
    {
        /// <summary>
        /// 扩展字段
        /// </summary>
        public T2 Ext2 { get; set; }
    }
}
