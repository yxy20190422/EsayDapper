using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUtilTool.NLog
{
    public class LogHelper
    {
        /// <summary>
        /// Exceptionless 日志
        /// </summary>
        public static Exceptionsless Exceptionless
        {
            get
            {
                return new Exceptionsless();
            }
        }

        /// <summary>
        /// Nlog日志
        /// </summary>
        public static Nlogger Nlogger
        {
            get
            {
                return new Nlogger();
            }
        }

        /// <summary>
        /// Exceptionless结合nlog日志
        /// </summary>
        public static ExceptionlessNlogger ExceptionlessNlog
        {
            get
            {
                return new ExceptionlessNlogger();
            }
        }
    }
}
