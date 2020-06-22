using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUtilTool.CoreModels
{
    /// <summary>
    /// 返回给前端的键值对
    /// </summary>
    public class UploadModel
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 值组合
        /// </summary>
        public string[] Value { get; set; }
    }

    public class UpTotalModel
    {
        /// <summary>
        /// Excel中的前3列的值
        /// </summary>
        public List<UploadModel> listResult { get; set; }
        /// <summary>
        /// 文件名字
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<UpLoadHead> listHead { get; set; }
    }
    /// <summary>
    /// 上传物料的写入的Head字段
    /// </summary>
    public class UpLoadHead
    {
        /// <summary>
        ///键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
}
