using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;

namespace DapperUtilTool.AutofacTools
{

    public class ConfigureAutofac : Autofac.Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            #region 注入数据库服务层
            InjectService(containerBuilder, "DapperModelCore", "Repository");
            #endregion
            #region 注入业务逻辑层
            InjectService(containerBuilder, "DapperService", "Services");
            #endregion
        }
        /// <summary>
        /// 注入系统服务
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="AssemblyName"></param>
        /// <param name="ServiceEndWith"></param>
        private void InjectService(ContainerBuilder containerBuilder, string AssemblyName, string ServiceEndWith)
        {
            Assembly service = Assembly.Load(AssemblyName);
            containerBuilder.RegisterAssemblyTypes(service)
.Where(t => t.Name.EndsWith(ServiceEndWith))
.AsImplementedInterfaces().PropertiesAutowired();
        }
    }
}
