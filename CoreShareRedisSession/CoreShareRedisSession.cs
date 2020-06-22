using System;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreShareRedisSession
{
    /// <summary>
    /// 基于同一域名下各子系统Session共享的封装
    /// </summary>
    public static class CoreShareRedisSession
    {
        //1.设置自定义Core数据保护
        //2.将Session信息持久化到Redis缓存
        //3.设置当前程序的Cookie域名为统一域名

        public static void ShareRedisSession(this IServiceCollection services,IConfiguration configuration)
        {
            IConfigurationSection sessionCacheSections = configuration.GetSection("SessoinConfig");
            SessoinConfig sessionCacheConfig = sessionCacheSections.Get<SessoinConfig>();
            if (sessionCacheConfig == null)
            {
                throw new Exception("缺少Session共享配置");
            }
            if (sessionCacheConfig.RedisCacheConfig == null)
            {
                throw new Exception("缺少Session共享持久化配置");
            }
            services.AddSingleton<IXmlRepository, ShareSessionXmlRepository>();
            services.AddDataProtection()
            .SetApplicationName(sessionCacheConfig.SessionCookieName)
            .AddKeyManagementOptions(o =>
            {
                o.XmlRepository = new ShareSessionXmlRepository();
            });
            services.AddStackExchangeRedisCache(o =>
            {
                o.Configuration = sessionCacheConfig.RedisCacheConfig.ConnectionString;
                o.InstanceName = sessionCacheConfig.RedisCacheConfig.InstanceName;
            });
            services.AddSession(options =>
            {
                //默认Session过期20分钟
                options.IdleTimeout = TimeSpan.FromMinutes(sessionCacheConfig.SessionTimeOut == 0 ? 20 : sessionCacheConfig.SessionTimeOut);
                options.Cookie.HttpOnly = true;
                options.Cookie.Domain = sessionCacheConfig.SessionCookieDomain;
                options.Cookie.Name = sessionCacheConfig.SessionCookieName + ".Session";
            });
        }

    }
}
