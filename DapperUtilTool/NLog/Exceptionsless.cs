using DapperUtilTool.NLog.Connection;
using Exceptionless;
using System;
using System.Collections.Generic;
using System.Text;
using Exceptionless.Logging;

namespace DapperUtilTool.NLog
{
    public class Exceptionsless : IDisposable
    {
        private ExceptionlessClient client = null;

        #region 创建ExceptionlessClient
        /// <summary>
        /// 构造方法 Exceptionless自托管
        /// </summary>
        public Exceptionsless()
        {
            if (LogServiceCollectionExtensions.Connection == null)
            {
                throw new ArgumentNullException("请注入Exceptionsless连接对象！");
            }


            string apikey = LogServiceCollectionExtensions.Connection.exceptionslessOptions.Apikey;// LogConfig.GetExceptionslessConfig().Apikey;
            string serverUrl = LogServiceCollectionExtensions.Connection.exceptionslessOptions.ServerUrl;// LogConfig.GetExceptionslessConfig().ServerUrl;

            if (string.IsNullOrEmpty(apikey) || string.IsNullOrEmpty(serverUrl))
            {
                throw new Exception("Exceptionless的apikey或serverUrl不能为空!");
            }
            client = GetExceptionlessClient(apikey, serverUrl);
        }

        public ExceptionlessClient GetExceptionlessClient(string apikey, string serverUrl)
        {
            SetIncludePrivateInformation();
            client = new ExceptionlessClient(apikey);
            client.Configuration.ServerUrl = serverUrl;
            client.Startup();
            return client;
        }
        #endregion

        #region 提交消息

        public void SubmitLogTrace(Type source, string msg)
        {
            client.SubmitLog(source.FullName, msg, LogLevel.Trace);
        }

        public void SubmitLogTrace(string msg)
        {
            client.SubmitLog(msg, LogLevel.Trace);
        }

        public void SubmitLogDebug(string msg)
        {
            client.SubmitLog(msg, LogLevel.Debug);
        }

        public void SubmitLogDebug(Type source, string msg)
        {
            client.SubmitLog(source.FullName, msg, LogLevel.Debug);
        }


        public void SubmitLogInfo(string msg)
        {
            client.SubmitLog(msg, LogLevel.Info);
        }

        public void SubmitLogInfo(Type source, string msg)
        {
            client.SubmitLog(source.FullName, msg, LogLevel.Info);
        }


        public void SubmitLogError(Type source, string msg)
        {
            client.SubmitLog(source.FullName, msg, LogLevel.Error);
        }


        public void SubmitLogError(string msg)
        {
            client.SubmitLog(msg, LogLevel.Error);
        }


        /// <summary>
        /// 提交消息
        /// </summary>
        /// <param name="msg"></param>
        public void SubmitLogFatal(string msg)
        {
            client.SubmitLog(msg, LogLevel.Fatal);
        }


        /// <summary>
        /// 提交消息
        /// </summary>
        /// <param name="type">日志源</param>
        /// <param name="msg"></param>
        public void SubmitLogFatal(Type source, string msg)
        {
            client.SubmitLog(source.FullName, msg, LogLevel.Fatal);
        }

        /// <summary>
        /// 提交异常
        /// </summary>
        /// <param name="exception"></param>
        public void SubmitException(Exception exception)
        {
            client.SubmitException(exception);
        }
        #endregion

        #region EventBuilder类型消息，用于扩展日志信息
        public void EventLog(EventBuilder builder)
        {
            builder.Client = client;
            builder.Submit();
        }

        public void EventLog(Exception exception)
        {
            client.CreateException(exception).Submit();
        }

        public void EventLogDebug(Type source, string msg)
        {
            client.CreateLog(source.FullName, msg, LogLevel.Debug).Submit();
        }

        public void EventLogDebug(string msg)
        {
            client.CreateLog(msg, LogLevel.Debug).Submit();
        }

        public void EventLogInfo(Type source, string msg)
        {
            client.CreateLog(source.FullName, msg, LogLevel.Info).Submit();
        }

        public void EventLogInfo(string msg)
        {
            client.CreateLog(msg, LogLevel.Info).Submit();
        }


        public void EventLogError(Type source, string msg)
        {
            client.CreateLog(source.FullName, msg, LogLevel.Error).Submit();
        }

        public void EventLogError(string msg)
        {
            client.CreateLog(msg, LogLevel.Error).Submit();
        }

        public void EventLogFatal(string msg)
        {
            client.CreateLog(msg, LogLevel.Fatal).Submit();
        }

        public void EventLogFatal(Type source, string msg)
        {
            client.CreateLog(source.FullName, msg, LogLevel.Fatal).Submit();
        }


        public void EventLogDebug(string msg, string tag)
        {
            client.CreateLog(msg, LogLevel.Debug).AddTags(tag).Submit();
        }

        public void EventLogInfo(string msg, string tag)
        {
            client.CreateLog(msg, LogLevel.Info).AddTags(tag).Submit();
        }


        public void EventLogError(string msg, string tag)
        {
            client.CreateLog(msg, LogLevel.Error).AddTags(tag).Submit();
        }

        public void EventLogFatal(string msg, string tag)
        {
            client.CreateLog(msg, LogLevel.Fatal).AddTags(tag).Submit();
        }
        #endregion

        #region Configuration配置
        /// <summary>
        /// 进程立即终止时使用了Exceptionless，应该手动强制处理队列，提交日志
        /// </summary>
        public void ProcessQueue()
        {
            ExceptionlessClient.Default.ProcessQueue();
        }

        /// <summary>
        /// 会将诊断消息写入日志文件,帮助诊断ExceptionlessClient的任何问题，调式用
        /// </summary>
        /// <param name="path">C:\\exceptionless.log</param>
        public void EnableClientLog(string path)
        {
            client.Configuration.UseFileLogger(path);
        }

        /// <summary>
        /// 设置全局Client包含的原数据，非当前实例的client,默认都为true
        /// </summary>
        private void SetIncludePrivateInformation()
        {
            // Include the username if available (E.G., Environment.UserName or IIdentity.Name)
            ExceptionlessClient.Default.Configuration.IncludeUserName = true;
            // Include the MachineName in MachineInfo.
            ExceptionlessClient.Default.Configuration.IncludeMachineName = true;
            // Include Ip Addresses in MachineInfo and RequestInfo.
            ExceptionlessClient.Default.Configuration.IncludeIpAddress = true;
            // Include Cookies, please note that DataExclusions are applied to all Cookie keys when enabled.
            ExceptionlessClient.Default.Configuration.IncludeCookies = true;
            // Include Form/POST Data, please note that DataExclusions are only applied to Form data keys when enabled.
            ExceptionlessClient.Default.Configuration.IncludePostData = true;
            // Include Query String information, please note that DataExclusions are applied to all Query String keys when enabled.
            ExceptionlessClient.Default.Configuration.IncludeQueryString = true;
        }

        /// <summary>
        /// 设置全局Client,在空闲时候更新configuration 设置时间，默认启动后5秒检查一次
        /// </summary>
        private void SetUpdateSettingsWhenIdleInterval()
        {
            ExceptionlessClient.Default.Configuration.UpdateSettingsWhenIdleInterval = new TimeSpan(0, 1, 0);
        }


        /// <summary>
        /// 用于离线存储
        /// 事件在提交时需要序列化到磁盘，因此不建议用于高吞吐量日志记录
        /// 暂不提供调用
        /// </summary>
        /// <param name="path"></param>
        private void SetUseFolderStorage()
        {
            //使用文件存储
            ExceptionlessClient.Default.Configuration.UseFolderStorage("store");
        }

        /// <summary>
        /// 使用NLog或Log4net做为目标时，如果每分钟记录数千条消息，则应使用内存中事件存储，速度更快。
        /// </summary>
        private void SetUseInMemoryStorage()
        {
            ExceptionlessClient.Default.Configuration.UseInMemoryStorage();
        }


        /// <summary>
        /// 禁用Exceptionless
        /// 暂不提供调用
        /// </summary>
        private void SetDisablingExceptionless()
        {
            ExceptionlessClient.Default.Configuration.Enabled = false;
        }
        #endregion

        #region  资源释放
        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            ((IDisposable)client).Dispose();
        }
        #endregion
    }
}
