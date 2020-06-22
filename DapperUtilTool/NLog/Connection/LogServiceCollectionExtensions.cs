using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUtilTool.NLog.Connection
{
    public static class LogServiceCollectionExtensions
    {
        internal static ILogConnection Connection { get; private set; }

        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="options">连接参数</param>
        /// <returns></returns>
        public static IServiceCollection AddExceptionless(this IServiceCollection serviceCollection, ExceptionslessOptions options)
        {
            serviceCollection.AddSingleton<ILogConnection>(m => new LogConnection() { exceptionslessOptions = options });
            Connection = serviceCollection.BuildServiceProvider().GetRequiredService<ILogConnection>();
            return serviceCollection;
        }
    }
}
