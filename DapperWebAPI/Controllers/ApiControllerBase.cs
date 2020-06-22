using DapperUtilTool.CoreModels;
using DapperUtilTool.Extensions;
using DapperUtilTool.NLog;
using DapperWebAPI.Infrastructure.HttpExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DapperWebAPI.Controllers
{
    /// <summary>
    /// API控制器基类
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ApiControllerBase : Controller
    {
        /// <summary>
        /// action执行开始
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (string.IsNullOrWhiteSpace(SessionDataResult.LoginUseID))
            {
                InitSession(filterContext);
            }
            //如果有AllowAnonymousAttribute标识就跳过
            if (IsAllowAnonymous(filterContext))
            {
                ValidateViewModels(filterContext);
                return;
            }
            //是否验证Token成功
            if (IsValidateTokenSucc(filterContext))
            {
                ValidateViewModels(filterContext);
            }
        }

        #region 初始化Session值
        private void InitSession(ActionExecutingContext filterContext)
        {
            SessionResult test = new SessionResult()
            {
                LoginUseID = "17b2269a-0bc6-4a20-95ed-25b3386250b1",
                LoginUseName = "采购员01"
            };
            filterContext.HttpContext.Session.Set<SessionResult>(ConstantConfig._SessionKey, test);            
        }
        #endregion

        /// <summary>
        /// 验证ViewModels
        /// </summary>
        /// <param name="filterContext"></param>
        private void ValidateViewModels(ActionExecutingContext filterContext)
        {
            if (!filterContext.ModelState.IsValid)
            {
                var result = new CoreResult();
                foreach (var item in filterContext.ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        result.Message += error.ErrorMessage + "|";
                    }
                }
                if (!string.IsNullOrWhiteSpace(result.Message))
                {
                    result.Message = result.Message.Remove(result.Message.Length - 1, 1);
                }
                filterContext.Result = new JsonResult(result);
            }
        }

        /// <summary>
        /// 是否验证Token成功
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private bool IsValidateTokenSucc(ActionExecutingContext filterContext)
        {
            var isSucc = false;

            StringValues vs;
            string key = string.Empty;
            if (filterContext.HttpContext.Request.Headers.TryGetValue("key", out vs))
            {
                key = Common.AdjustNullValue(vs);
            }
            string time = string.Empty;
            if (filterContext.HttpContext.Request.Headers.TryGetValue("time", out vs))
            {
                time = Common.AdjustNullValue(vs.ToString());
            }
            string token = string.Empty;
            if (filterContext.HttpContext.Request.Headers.TryGetValue("token", out vs))
            {
                token = Common.AdjustNullValue(vs.ToString());
            }
            var msg = "key：" + key + "；time：" + time + "；token：" + token;
            LogHelper.Nlogger.Info(msg);

            if (string.IsNullOrEmpty(key))
            {
                filterContext = InitFilterContext(filterContext, "key validation failed!");
                return isSucc;
            }
            if (string.IsNullOrEmpty(time))
            {
                filterContext = InitFilterContext(filterContext, "time validation failed!");
                return isSucc;
            }
            double reqtimestramp = 0;
            if (!double.TryParse(time, out reqtimestramp))
            {
                filterContext = InitFilterContext(filterContext, "incorrect time format!");
                return isSucc;
            }
            if (string.IsNullOrEmpty(token))
            {
                filterContext = InitFilterContext(filterContext, "token validation failed!");
                return isSucc;
            }

            reqtimestramp += 5 * 60;
            var systemTimeStramp = Common.GetTimeStamp();
            if (reqtimestramp < systemTimeStramp)
            {
                filterContext = InitFilterContext(filterContext, "request exceeds available time!");
                return isSucc;
            }
            var bodyJson = InitBodyString(filterContext.HttpContext.Request);
            var authStr = ConstantConfig.Secret + bodyJson + time;
            var checkToken = Common.MD5Encrypt(authStr);
            if (!checkToken.Equals(token))
            {
                filterContext = InitFilterContext(filterContext, "token comparison failed!");
                return isSucc;
            }

            isSucc = true;
            return isSucc;
        }

        /// <summary>
        /// 初始化主体字符串
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string InitBodyString(HttpRequest request)
        {
            try
            {
                var bodyJson = request.GetRawBodyString();
                return bodyJson;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 是否允许匿名访问
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private bool IsAllowAnonymous(ActionExecutingContext filterContext)
        {
            var count = filterContext.ActionDescriptor.EndpointMetadata.Count(m => m.GetType().Name == "AllowAnonymousAttribute");
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 初始化过滤器上下文
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private ActionExecutingContext InitFilterContext(ActionExecutingContext filterContext, string msg)
        {
            var result = new CoreResult() { StatusCode = 0, Message = msg };
            filterContext.Result = new JsonResult(result)
            {
                ContentType = "application/json",
                StatusCode = 401,
            };
            filterContext.HttpContext.Response.StatusCode = 401;
            return filterContext;
        }

        /// <summary>
        /// action执行结束
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}
