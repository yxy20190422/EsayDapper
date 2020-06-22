using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using DapperUtilTool.Common;
using DapperUtilTool.CoreModels;
using DapperUtilTool.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperWebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 上传的Excel存入的位置
        /// </summary>
        private readonly IWebHostEnvironment hostingEnvironment;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public HomeController(IWebHostEnvironment env)
        {
            this.hostingEnvironment = env;
        }
        /// <summary>
        /// 测试SSO
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()      
        
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(string UserName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, UserName)
            };
            if (string.IsNullOrWhiteSpace(HttpContext.Session.Get<string>("UserName")))
            {
                HttpContext.Session.Set("UserName", UserName);
            }       
            return RedirectToAction("GetLoginUser");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult SignOut()
        {
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetLoginUser()
        {
            return View();
        }
    }
}