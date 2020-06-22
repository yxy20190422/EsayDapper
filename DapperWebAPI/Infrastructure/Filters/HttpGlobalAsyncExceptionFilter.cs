using DapperUtilTool.CoreModels;
using DapperUtilTool.Extensions;
using DapperUtilTool.NLog;
using DapperWebAPI.Infrastructure.HttpExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DapperWebAPI.Infrastructure.Filters
{
    /// <summary>
    /// 全局异常日志类
    /// </summary>
    public class HttpGlobalAsyncExceptionFilter : ActionFilterAttribute, IAsyncExceptionFilter
    {
        /// <summary>
        /// 日志选项
        /// </summary>
        private LogSettingOptions _options;

        /// <summary>
        /// HTTP全局异常过滤监控构造函数
        /// </summary>
        public HttpGlobalAsyncExceptionFilter(IOptions<LogSettingOptions> options)
        {
            _options = options.Value;
        }

        #region Action时间监控
        /// <summary>
        /// action执行开始
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //将日志监控对象存入当前Controller的ViewData中
            filterContext.HttpContext.Items[ConstantConfig._NlogKey] = InitMonitorLog(filterContext);
        }

        /// <summary>
        /// 初始化监控日志
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private MonitorLog InitMonitorLog(ActionExecutingContext filterContext)
        {
            var monitorLog = new MonitorLog();
            monitorLog.ExecuteStartTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff", DateTimeFormatInfo.InvariantInfo));
            monitorLog.UserId = filterContext.HttpContext.User.Identity.Name;
            monitorLog.IpAddress = filterContext.HttpContext.Connection.RemoteIpAddress.ToString();
            monitorLog.ControllerName = filterContext.RouteData.Values["controller"] as string;
            monitorLog.ActionName = filterContext.RouteData.Values["action"] as string; var request = filterContext.HttpContext.Request;
            monitorLog.RequestUrl = $"{request.Host}{request.Path}";
            monitorLog.RequestType = request.Method;

            if (request.Method.ToLower() == "get")
            {
                if (request.QueryString.HasValue)
                    monitorLog.QueryString = request.QueryString.ToString();
            }
            else
            {
                try
                {
                    if (request.ContentLength > 0)
                    {
                        var body = request.GetRawBodyString();
                        monitorLog.FormBody = body;
                    }
                    else if (request.QueryString.HasValue)
                        monitorLog.FormBody = request.QueryString.Value;
                    else
                    {
                        if (request.Form.Count > 0)
                            monitorLog.FormBody = monitorLog.GetCollections((FormCollection)request.Form);
                    }
                }
                catch (Exception) { }
            }
            return monitorLog;
        }

        /// <summary>
        /// action执行结束
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var monitorLog = filterContext.HttpContext.Items[ConstantConfig._NlogKey] as MonitorLog;
            monitorLog.ExecuteEndTime = DateTime.Now;
        }
        #endregion

        #region Result时间监控
        /// <summary>
        /// action 结果执行开始
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var monitorLog = filterContext.HttpContext.Items[ConstantConfig._NlogKey] as MonitorLog;
            if (monitorLog != null)
                monitorLog.ResultStartTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff", DateTimeFormatInfo.InvariantInfo));
        }

        /// <summary>
        ///  action 结果执行结束
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            try
            {
                var monitorLog = filterContext.HttpContext.Items[ConstantConfig._NlogKey] as MonitorLog;
                if (monitorLog != null)
                {
                    monitorLog.ResultEndTime = DateTime.Now;

                    var actionType = filterContext.Result.GetType().Name;
                    monitorLog.ActionType = actionType;
                    monitorLog.IsSucceed = true;                            //默认成功

                    if (actionType.ToLower() == "jsonresult")
                    {
                        monitorLog.LogLevel = 2;
                        monitorLog.ResponseData = JsonConvert.SerializeObject((filterContext.Result as JsonResult).Value);
                    }
                    else if (actionType.ToLower().Contains("objectresult"))
                    {
                        monitorLog.LogLevel = 2;
                        monitorLog.ResponseData = JsonConvert.SerializeObject((filterContext.Result as ObjectResult).Value);
                    }
                    else if (actionType.ToLower() == "viewresult")
                        monitorLog.LogLevel = 1;
                    else
                        monitorLog.LogLevel = 3;
                    LogHelper.Nlogger.Trace(monitorLog.GetLogInfo());
                    //执行结束后清除ViewData
                    filterContext.HttpContext.Items.Remove(ConstantConfig._NlogKey);
                }
            }
            catch { }
        }
        #endregion

        #region 全局异常记录
        /// <summary>
        /// 异常记录
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        public Task OnExceptionAsync(ExceptionContext filterContext)
        {
            try
            {
                var msg = "message:" + filterContext.Exception.Message + "。stacktrace:" + filterContext.Exception.StackTrace;
                LogHelper.Nlogger.Error(msg);
                var exceptionModel = new CoreResult<string>();
                exceptionModel.StatusCode = 0;
                exceptionModel.Message = "接口异常！";
                exceptionModel.Ext1 = msg;
                filterContext.Result = new OkObjectResult(exceptionModel);
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                //不再throw，在此捕获获取
                filterContext.ExceptionHandled = true;
            }
            catch { }
            return Task.CompletedTask;
        }
        #endregion
    }
}
