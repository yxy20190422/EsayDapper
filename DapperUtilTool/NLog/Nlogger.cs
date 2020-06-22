using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUtilTool.NLog
{
    /// <summary>
    /// NLog日志
    /// </summary>
    public class Nlogger
    {
        /// <summary>
        /// NLog日志
        /// </summary>
        private ILogger logger { get; set; }
        /// <summary>
        /// NLog日志构造函数
        /// </summary>
        public Nlogger()
        {
            try
            {
                logger = NLogBuilder.ConfigureNLog("Nlog.config").GetCurrentClassLogger();
            }
            catch (Exception e)
            {
                throw new Exception("未找到nlog.config文件或配置错误：" + e.Message);
            }
        }
        /// <summary>
        /// 追踪
        /// </summary>
        /// <param name="msg">消息</param>
        public void Trace(string msg)
        {
            logger.Trace(msg);
        }
        /// <summary>
        /// 追踪
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="args">参数集</param>
        public void Trace(string msg, params object[] args)
        {
            logger.Trace(msg, args);
        }

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="msg">消息</param>
        public void Debug(string msg)
        {
            logger.Debug(msg);
        }
        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="args">参数集</param>
        public void Debug(string msg, params object[] args)
        {
            logger.Debug(msg, args);
        }
        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常消息</param>
        /// <param name="args">参数集</param>
        public void Debug(string msg, Exception ex, params object[] args)
        {
            logger.Debug(msg, ex, args);
        }

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="msg">消息</param>
        public void Info(string msg)
        {
            logger.Info(msg);
        }
        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="ex">异常消息</param>
        public void Info(Exception ex)
        {
            logger.Info(ex);
        }
        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="args">参数集</param>
        public void Info(string msg, params object[] args)
        {
            logger.Info(msg, args);
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="msg">消息</param>
        public void Error(string msg)
        {
            logger.Error(msg);
        }
        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="args">参数集</param>
        public void Error(string msg, params object[] args)
        {
            logger.Error(msg, args);
        }
        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="ex">异常消息</param>
        /// <param name="args">参数集</param>
        public void Error(Exception ex, string msg)
        {
            logger.Error(ex, msg);
        }

        /// <summary>
        /// 灾难
        /// </summary>
        /// <param name="msg">消息</param>
        public void Fatal(string msg)
        {
            logger.Fatal(msg);
        }
        /// <summary>
        /// 灾难
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="args">参数集</param>
        public void Fatal(string msg, params object[] args)
        {
            logger.Fatal(msg, args);
        }
        /// <summary>
        /// 灾难
        /// </summary>
        /// <param name="ex">异常消息</param>
        /// <param name="args">参数集</param>
        public void Fatal(Exception ex, string msg)
        {
            logger.Fatal(ex, msg);
        }
    }
}
