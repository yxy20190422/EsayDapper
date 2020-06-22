using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUtilTool.Common
{
    /// <summary>
    /// 扩展Session
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// Session持久化至Redis
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        public static void UseSession(this IServiceCollection services, SessoinConfig sessoinConfig)
        {
            services.AddSingleton<IDistributedCache>(
                serviceProvider =>
                    new RedisCache(new RedisCacheOptions
                    {
                        Configuration = sessoinConfig.RedisCacheConfig.ConnectionString,
                        InstanceName = sessoinConfig.RedisCacheConfig.InstanceName
                    }));

            // 添加Session并设置过期时长
            services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(sessoinConfig.SessionTimeOut); });
        }

    }
}
