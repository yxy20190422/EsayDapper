using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUtilTool.NLog
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class LogAttribute : Attribute
    {
        public LogRecordType _logRecordType { get; set; }

        public LogAttribute(LogRecordType logType)
        {
            _logRecordType = logType;
        }
    }
    /// <summary>
    /// 记录类型
    /// </summary>
    public enum LogRecordType
    {
        Exceptionsless,
        Nlog,
        ExceptionlessNlog
    }
}
