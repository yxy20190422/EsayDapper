using DapperUtilTool.CoreModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace DapperUtilTool.Extensions
{
    /// <summary>
    /// 共前端返回的数据
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// 返回前端需要的Key,Value(首列，一般是列名)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<UploadModel> GetUpLoad(this DataTable dt)
        {
            List<UploadModel> listModel = new List<UploadModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                var res = dt.AsEnumerable().Take(3);
                
                for (var colindex = 0; colindex < dt.Columns.Count; colindex++)
                {   //第一行是Key  
                    UploadModel model = new UploadModel();
                    var datavalue = dt.Columns[colindex].ColumnName;
                    if (!string.IsNullOrWhiteSpace(datavalue))
                    {
                        model.Key = datavalue.ToString();
                        listModel.Add(model);
                    }
                }
                if (listModel != null && listModel.Count > 0)
                {
                    foreach (var item in listModel)
                    {
                        List<string> strList = new List<string>();
                        foreach (var row in res)
                        {
                            var valuestr = row[item.Key];
                            var strvalue = valuestr != null ? valuestr.ToString() : string.Empty;
                            strList.Add(strvalue);
                        }
                        if (strList != null && strList.Count > 0)
                        {
                            item.Value = strList.ToArray();
                        }
                    }
                }
            }
            return listModel;
        }
    }
}
