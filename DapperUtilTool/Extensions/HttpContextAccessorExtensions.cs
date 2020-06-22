using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUtilTool.Extensions
{
    /// <summary>
    /// HTTP上下文存储器扩展
    /// </summary>
    public static class HttpContextAccessorExtensions
    {
        /// <summary>
        /// 上下文存储器
        /// </summary>
        private static IHttpContextAccessor _contextAccessor;
        public static IApplicationBuilder UseContextAccessor(this IApplicationBuilder builder)
        {
            _contextAccessor = builder.ApplicationServices.GetRequiredService<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
            return builder;
        }
        /// <summary>
        /// 当前上下文
        /// </summary>
        public static Microsoft.AspNetCore.Http.HttpContext Current
        {
            get
            {
                if (_contextAccessor != null)
                {
                    return _contextAccessor.HttpContext;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
