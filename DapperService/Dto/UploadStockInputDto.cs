using DapperUtilTool.CoreModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperService.Dto
{
    /// <summary>
    /// 物料上传的Model
    /// </summary>
    public class UploadStockInputDto
    {
        /// <summary>
        /// 上传的文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 数据库字段与Excel具体的列对应
        /// </summary>
        public List<UpLoadHead> UpFileds{get;set;}
    }

}
