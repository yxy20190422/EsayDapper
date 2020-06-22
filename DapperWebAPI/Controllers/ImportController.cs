using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DapperService.Dto;
using DapperUtilTool.Common;
using DapperUtilTool.CoreModels;
using DapperUtilTool.Extensions;
using DapperUtilTool.UpLoadHead;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;

namespace DapperWebAPI.Controllers
{
    /// <summary>
    /// 上传.netcore物料Excel文件
    /// </summary>
    [AllowAnonymous]
    public class ImportController : ApiControllerBase
    {
        /// <summary>
        /// 上传的Excel存入的位置
        /// </summary>
        private readonly IWebHostEnvironment hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public ImportController(IWebHostEnvironment env)
        {
            this.hostingEnvironment = env;
        }
        /// <summary>
        /// 上传Excel并且存入到服务器上
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public OnlineOrdersResultDto<UpTotalModel> ImportExcel(IFormFile formFile)
        {
            OnlineOrdersResultDto<UpTotalModel> result = new OnlineOrdersResultDto<UpTotalModel>() { StatusCode = 0, Message = "上传Excel失败", Ext1 = new UpTotalModel() { } };
            if (Request.Form.Files == null || Request.Form.Files.Count == 0)
            {
                result.Message = "上传的Excel文件为空";
                return result;
            }
            try
            {
                IFormFile fileinput = Request.Form.Files[0];
                var filename = ContentDispositionHeaderValue.Parse(fileinput.ContentDisposition).FileName; // 原文件名（包括路径）
                var extName = filename.Substring(filename.LastIndexOf('.')).Replace("\"", "");// 扩展名
                if (extName.ToLower() != ".xls" && extName.ToLower() != ".xlsx")
                {
                    result.Message = "上传文件格式不正确";
                    return result;
                }
                string shortfilename = $"{Guid.NewGuid()}{extName}";// 新文件名
                string fileSavePath = hostingEnvironment.WebRootPath + @"\upload\stock\";//文件临时目录，导入完成后 删除
                filename = fileSavePath + shortfilename; // 新文件名（包括路径）
                if (!Directory.Exists(fileSavePath))
                {
                    Directory.CreateDirectory(fileSavePath);
                }
                using (FileStream fs = System.IO.File.Create(filename)) // 创建新文件
                {
                    fileinput.CopyTo(fs);// 复制文件
                    fs.Flush();// 清空缓冲区数据
                }
                //读取Excel中的文件到DataTable中
                using (Stream sm = System.IO.File.OpenRead(filename))
                {
                    DataTable dt = OfficeHelper.ReadStreamToDataTable(sm, extName, null, true, 4);
                    List<UploadModel> models = dt.GetUpLoad();
                    result.StatusCode = 1;
                    result.Message = "上传Excel成功";
                    result.Ext1.FileName = shortfilename;
                    result.Ext1.listResult = models;
                    result.Ext1.listHead = ConstUpLoadDic.StockHead.GetUpLoadHead();
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = 2;
                result.Message = ex.Message;
            }
           
            return result;
        }

        /// <summary>
        /// 测试导出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public FileStreamResult TestExport()
        {
            ExportAdmin param = new ExportAdmin();
            param.Title = "这是一个测试";
            param.Items = new List<ExportAdminItem>();
            param.FileName = "exportdemo.xls";
            param.FilterContent = "记录筛选的内容";

            param.Items.Add(new ExportAdminItem()
            {
                Username = "user1",
                Nickname = "nick1",
                CreateTime = DateTime.Now,
                LoginTime = DateTime.Now,
                Money = 100,
                //NoInclude = 1000
            });
            param.Items.Add(new ExportAdminItem()
            {
                Username = "user2",
                Nickname = "nick2",
                CreateTime = DateTime.Now,
                LoginTime = DateTime.Now,
                Money = 50.2321M,
                //NoInclude = 1000
            });
            param.Items.Add(new ExportAdminItem()
            {
                Username = "user3",
                Nickname = "nick3",
                CreateTime = DateTime.Now,
                LoginTime = DateTime.Now,
                Money = -100.2M,
                //NoInclude = 1000
            });

            OfficeHelper bll = new OfficeHelper();
            IWorkbook book = bll.ExportExcel<ExportAdminItem>(param);
            MemoryStream ms = new MemoryStream();
            book.Write(ms);
            ms.Position = 0;
            FileStreamResult fResult = new FileStreamResult(ms, "application/octet-stream") { FileDownloadName = param.FileName };
            return fResult;
        }
    }
}