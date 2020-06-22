using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUtilTool.NLog
{
    /// <summary>
    /// nlog结合Exceptionless
    /// </summary>
    public class ExceptionlessNlogger
    {
        private Exceptionsless exceptionsless { get; set; }

        private Nlogger nLogger { get; set; }


        /// <summary>
        /// 构造方法 Exceptionless自托管
        /// </summary>
        /// <param name="apikey"></param>
        /// <param name="serverUrl"></param>
        public ExceptionlessNlogger()
        {
            LogInstance();
        }

        private void LogInstance()
        {
            exceptionsless = new Exceptionsless();
            nLogger = new Nlogger();
        }

        public void Debug(Type source, string msg)
        {
            exceptionsless.SubmitLogDebug(source, msg);
            nLogger.Debug(msg);
        }

        public void Info(Type source, string msg)
        {
            exceptionsless.SubmitLogInfo(source, msg);
            nLogger.Info(msg);
        }

        public void Error(Type source, string msg)
        {
            exceptionsless.SubmitLogError(source, msg);
            nLogger.Error(msg);
        }

        public void Error(Exception ex)
        {
            exceptionsless.SubmitException(ex);
            nLogger.Error(ex, ex.Message);
        }

        public void Fatal(Type source, string msg)
        {
            exceptionsless.SubmitLogFatal(source, msg);
            nLogger.Fatal(msg);
        }
    }
}
