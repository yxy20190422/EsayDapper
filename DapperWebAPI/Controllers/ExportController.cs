using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DapperService.Export;
using DapperService.IService;
using DapperUtilTool.Common;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;

namespace DapperWebAPI.Controllers
{
    /// <summary>
    /// 导出控制器
    /// </summary>
    public class ExportController : Controller
    {

        /// <summary>
        /// 物料的服务
        /// </summary>
        private IStockServices _service { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public ExportController(IStockServices services)
        {
            _service = services;
        }
        /// <summary>
        /// 测试导出
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public FileStreamResult ExportStockUpload(string UploadNo)
        {
            var dataList = _service.ExportUploadOutData(UploadNo);
            ExportModel<StockUploadModel> param = new ExportModel<StockUploadModel> ();
            param.Items = dataList;
            DateTime dtNow = DateTime.Now;
            string filename = dtNow.ToString("yyyy-MM-dd hh:mm:ss").Replace("-", "").Replace(":", "").Replace(" ", "_");
            param.FileName = filename + ".xls";
            OfficeHelper bll = new OfficeHelper();
            IWorkbook book = bll.ExportExcel<StockUploadModel>(param);
            MemoryStream ms = new MemoryStream();
            book.Write(ms);
            ms.Position = 0;
            FileStreamResult fResult = new FileStreamResult(ms, "application/octet-stream") { FileDownloadName = param.FileName };
            return fResult;
        }
    }
}