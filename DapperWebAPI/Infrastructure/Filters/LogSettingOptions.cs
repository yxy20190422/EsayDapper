using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperWebAPI.Infrastructure.Filters
{ /// <summary>
  /// 日志设置选项
  /// </summary>
    public class LogSettingOptions
    {
        /// <summary>
        /// 日志设置选项
        /// </summary>
        public LogSettingOptions() { }
        /// <summary>
        /// 无例外对象
        /// </summary>
        public Exceptionless Exceptionless { get; set; }
        /// <summary>
        /// 回应输出
        /// </summary>
        public ResponseOutput ResponseOutput { get; set; }
    }
    /// <summary>
    /// 无例外对象
    /// </summary>
    public class Exceptionless
    {
        /// <summary>
        /// API键
        /// </summary>
        public string ApiKey { get; set; }
        /// <summary>
        /// 服务URL
        /// </summary>
        public string ServerUrl { get; set; }
    }
    /// <summary>
    /// 回应输出
    /// </summary>
    public class ResponseOutput
    {
        /// <summary>
        /// 是否友好的
        /// </summary>
        public bool IsFriendly { get; set; }
        /// <summary>
        /// 友好消息
        /// </summary>
        public string FriendlyMsg { get; set; }
    }
}
