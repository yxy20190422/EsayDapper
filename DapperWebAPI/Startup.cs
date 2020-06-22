using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Autofac;
using DapperUtilTool.AutofacTools;
using DapperUtilTool.Common;
using DapperUtilTool.Extensions;
using DapperWebAPI.Infrastructure.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using CoreShareRedisSession;

namespace DapperWebAPI
{
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerBuilder"></param>
        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule<ConfigureAutofac>();
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option =>
            {
                //option.Filters.Add(typeof(HttpGlobalAsyncAuthorizationFilter));
                option.Filters.Add(typeof(HttpGlobalAsyncExceptionFilter));
            });
            services.AddControllers();
            services.AddHttpClient("Controller", c =>
             {
                 c.BaseAddress = new Uri("http://test.icvip.com");
             }).ConfigurePrimaryHttpMessageHandler(() =>
             {
                 return new HttpClientHandler()
                 {
                     // ����Ҫ GZip ���룬���ڴ�����
                     AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                 };
             });
            #region Session
            //��ȡkey-xxxxx.xml
            services.ShareRedisSession(Configuration);
            //���Session��Ϊȫ�ֵ���
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                //ע�ⲻ���ô�дV1����Ȼ�ϱ���Not Found /swagger/v1/swagger.json
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = $"�ȼ�ϵͳ�����ĵ����ٲ���",
                    Description = "����΢������ټ�����Ϣ",
                    Contact = new OpenApiContact() { Name = "yxy", Email = "www.cnblogs.com/kogel/p/10805696.html" }
                });
                //c.OperationFilter<AddHeaderParameter>();
                c.CustomSchemaIds(c => c.FullName);
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//��ȡӦ�ó�������Ŀ¼�����ԣ����ܹ���Ŀ¼Ӱ�죬������ô˷�����ȡ·����
                var xmlPath = Path.Combine(basePath, "DapperWebAPI.xml");
                c.IncludeXmlComments(xmlPath);
                var modelXmlFile = "DapperService.xml";
                var modelXmlPath = Path.Combine(basePath, modelXmlFile);
                c.IncludeXmlComments(modelXmlPath);
            });
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();
            //������ܵ���ע��HttpContextAccessor������
            HttpContextAccessorExtensions.UseContextAccessor(app);
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();//��Ȩ

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });
            #region Swagger
            if (!env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

                });
            }
            #endregion
        }
    }
}
