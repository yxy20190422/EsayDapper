using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace DapperUtilTool.NLog
{/// <summary>
 /// 监控日志对象,用于记录每个action
 /// </summary>
    public class MonitorLog
    {
        /// <summary>
        /// action执行开始时间
        /// </summary>
        public DateTime ExecuteStartTime { get; set; }

        /// <summary>
        /// action执行结束时间
        /// </summary>
        public DateTime ExecuteEndTime { get; set; }

        /// <summary>
        /// 用户标识
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 客户端请求ip地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// Action名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 客户端请求http地址
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// http请求类型
        /// </summary>
        public string RequestType { get; set; }

        /// <summary>
        /// URL参数
        /// </summary>
        public string QueryString { get; set; }

        /// <summary>
        /// Form表单数据
        /// </summary>
        public string FormBody { get; set; }

        /// <summary>
        /// action结果执行开始时间
        /// </summary>
        public DateTime ResultStartTime { get; set; }

        /// <summary>
        /// action结果执行结束时间
        /// </summary>
        public DateTime ResultEndTime { get; set; }

        /// <summary>
        /// action结果类型
        /// </summary>
        public string ActionType { get; set; }

        /// <summary>
        /// action结果是否成功
        /// </summary>
        public bool IsSucceed { get; set; }

        /// <summary>
        /// 响应字符串
        /// </summary>
        public string ResponseData { get; set; }

        /// <summary>
        /// 日志级别 1页面 2数据 3其它
        /// </summary>
        public int LogLevel { get; set; }

        /// <summary>
        /// 获取form参数
        /// </summary>
        /// <param name="collections"></param>
        /// <returns></returns>
        public string GetCollections(IFormCollection collections)
        {
            string parameters = string.Empty;
            if (collections != null)
            {
                foreach (string key in collections.Keys)
                {
                    parameters += string.Format("{0}={1}&", key, collections[key]);
                }
            }
            return parameters.TrimEnd('&');
        }

        /// <summary>
        /// 获取日志监控信息
        /// </summary>
        /// <returns></returns>
        public string GetLogInfo()
        {
            string actionView = "Action执行时间监控";
            string name = "Action";
            if (LogLevel == 1)
            {
                actionView = "View视图生成时间监控";
                name = "View";
            }

            string msg = @"{0}
            ControllerName：{1}Controller,
            {12}Name：{2}
            开始时间：{3}
            结束时间：{4}
            总 耗 时：{5}秒
            操作用户：{13}
            请求IP：{6}
            请求地址：{7}
            请求参数：{8}
            请求类型：{9}
            处理结果：{10}
            响应数据：{11}";
            return string.Format(msg,
                actionView,
                ControllerName,
                ActionName,
                ExecuteStartTime,
                ResultEndTime,
                (ResultEndTime - ExecuteStartTime).TotalSeconds,
                IpAddress,
                RequestUrl,
                RequestType.ToLower() == "get" ? QueryString : FormBody,
                RequestType,
                IsSucceed ? "成功" : "失败",
                ResponseData,
                name,
                UserId);
        }
    }
}
