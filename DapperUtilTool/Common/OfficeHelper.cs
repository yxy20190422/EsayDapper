using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DapperUtilTool.Common
{
    /// <summary>
    /// Summary description for OfficeHelper
    /// </summary>
    public class OfficeHelper
    {
        /// <summary>
        /// 将excel文件内容读取到DataTable数据表中(这部分代码有bug，需要调整下。目前址修改了)
        /// </summary>
        /// <param name="fileName">文件完整路径名</param>
        /// <param name="sheetName">指定读取excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名：true=是，false=否</param>
        /// <returns>DataTable数据表</returns>
        public static DataTable ReadExcelToDataTable(string fileName, string sheetName = null, bool isFirstRowColumn = true)
        {
            //定义要返回的datatable对象
            DataTable data = new DataTable();
            //excel工作表
            ISheet sheet = null;
            //数据开始行(排除标题行)
            int startRow = 0;
            try
            {
                if (!File.Exists(fileName))
                {
                    return null;
                }
                //根据指定路径读取文件
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                //根据文件流创建excel数据结构
                IWorkbook workbook = WorkbookFactory.Create(fs);
                //IWorkbook workbook = new HSSFWorkbook(fs);
                //如果有指定工作表名称
                if (!string.IsNullOrEmpty(sheetName))
                {
                    sheet = workbook.GetSheet(sheetName);
                    //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    if (sheet == null)
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    //如果没有指定的sheetName，则尝试获取第一个sheet
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    //一行最后一个cell的编号 即总的列数
                    int cellCount = firstRow.LastCellNum;
                    //如果第一行是标题列名
                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="sheetName"></param>
        /// <param name="isFirstRowColumn"></param>
        /// <param name="RowCount">读excel几行</param>
        /// <param name="extName">文件类型:xls用HSSFWorkbook对象操作,xlxs用WorkbookFactory操作</param>
        /// <returns></returns>
        public static DataTable ReadStreamToDataTable(Stream fileStream,string extName, string sheetName = null, bool isFirstRowColumn = true, int RowCount = 0)
        {
            //定义要返回的datatable对象
            DataTable data = new DataTable();
            //excel工作表
            ISheet sheet = null;
            //数据开始行(排除标题行)
            int startRow = 0;
            try
            {
                //根据文件流创建excel数据结构,NPOI的工厂类WorkbookFactory会自动识别excel版本，创建出不同的excel数据结构
                IWorkbook workbook = extName.ToLower() == ".xls" ? new HSSFWorkbook(fileStream) : WorkbookFactory.Create(fileStream);
                //如果有指定工作表名称
                if (!string.IsNullOrEmpty(sheetName))
                {
                    sheet = workbook.GetSheet(sheetName);
                    //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    if (sheet == null)
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    //如果没有指定的sheetName，则尝试获取第一个sheet
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    //一行最后一个cell的编号 即总的列数
                    int cellCount = firstRow.LastCellNum;
                    //如果第一行是标题列名
                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (!string.IsNullOrWhiteSpace(cellValue))
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }
                    //最后一列的标号
                    int rowCount = RowCount == 0 ? sheet.LastRowNum : 4;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null || row.FirstCellNum < 0) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        bool IsEmptyRow = false;//是否是空行判断
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            //同理，没有数据的单元格都默认是null
                            ICell cell = row.GetCell(j);
                            if (cell != null)
                            {
                                if (cell.CellType == CellType.Numeric)
                                {
                                    //判断是否日期类型
                                    if (DateUtil.IsCellDateFormatted(cell))
                                    {
                                        dataRow[j] = row.GetCell(j).DateCellValue;
                                    }
                                    else
                                    {
                                        dataRow[j] = row.GetCell(j).ToString().Trim();
                                    }
                                }
                                else
                                {
                                    dataRow[j] = row.GetCell(j).ToString().Trim();
                                }
                                if (dataRow[j] != null && !string.IsNullOrWhiteSpace(dataRow[j].ToString()))
                                {
                                    IsEmptyRow = true;
                                }
                            }
                        }
                        if (!IsEmptyRow)
                        {
                            break;
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 通用导出Excel
        public IWorkbook ExportExcel<T>(ExportModel<T> param)
        {
            int fieldCount = GetPropertyCount<T>();
            int rowIndex = 0;

            IWorkbook wb = new HSSFWorkbook();
            //设置工作簿的名称
            string sheetName = string.IsNullOrEmpty(param.SheetName) ? "sheet1" : param.SheetName;
            //创建一个工作簿
            ISheet sh = wb.CreateSheet(sheetName);

            #region 第一行，文件标题
            ////合并单元格
            //sh.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, fieldCount - 1));
            //IRow row0 = sh.CreateRow(rowIndex);
            //ICell icell1top0 = row0.CreateCell(0);
            //icell1top0.SetCellValue(param.Title);
            //#endregion

            //rowIndex++;

            //#region 第二行，筛选内容
            //sh.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 0, fieldCount - 1));
            //IRow row1 = sh.CreateRow(rowIndex);
            //ICell icell1top1 = row1.CreateCell(0);
            //icell1top1.SetCellValue(param.FilterContent);
            #endregion

            //rowIndex++;

            List<ExportFieldAttribute> colList = new List<ExportFieldAttribute>();
            #region 表头(第3行)
            IRow row2 = sh.CreateRow(rowIndex);
            var properties = typeof(T).GetProperties();
            int colIndex = 0;
            foreach (PropertyInfo item in properties)
            {
                ExportFieldAttribute attr = ((ExportFieldAttribute)Attribute.GetCustomAttribute(item, typeof(ExportFieldAttribute)));
                if (attr == null)
                {
                    continue;
                }
                ICell tableCell = row2.CreateCell(colIndex);
                tableCell.SetCellValue(attr.Title);

                if (attr.Width > 0)
                {
                    sh.SetColumnWidth(colIndex, attr.Width * 256);
                }

                colIndex++;

                colList.Add(attr);
            }
            #endregion

            rowIndex++;
            #region 数据内容

            foreach (T line in param.Items)
            {
                colIndex = 0;
                IRow bodyRow = sh.CreateRow(rowIndex);
                foreach (PropertyInfo pi in properties)
                {
                    ExportFieldAttribute attr = ((ExportFieldAttribute)Attribute.GetCustomAttribute(pi, typeof(ExportFieldAttribute)));
                    if (attr == null)
                    {
                        continue;
                    }
                    ICell rowCell = bodyRow.CreateCell(colIndex);
                    object obj = pi.GetValue(line, null);
                    string value = obj != null ? obj.ToString() : string.Empty;
                    value = GetDataValue(value, attr);
                    rowCell.SetCellValue(value);

                    colIndex++;
                }
                rowIndex++;
            }

            #endregion

            return wb;
        }

        private string GetDataValue(string value, ExportFieldAttribute attr)
        {
            if (!string.IsNullOrWhiteSpace(attr.Format))
            {
                if (attr.Type == FieldType.DateTime)
                {
                    DateTime tt;
                    if (DateTime.TryParse(value, out tt))
                    {
                        return tt.ToString(attr.Format);
                    }
                }
                else if (attr.Type == FieldType.Numeric)
                {
                    decimal dd;
                    if (decimal.TryParse(value, out dd))
                    {
                        return dd.ToString(attr.Format);
                    }
                }
            }

            return value;
        }

        private int GetPropertyCount<T>()
        {
            var properties = typeof(T).GetProperties();
            int result = 0;
            foreach (System.Reflection.PropertyInfo info in properties)
            {
                var attributes = info.GetCustomAttributes(typeof(ExportFieldAttribute), false).FirstOrDefault();
                if (attributes != null)
                {
                    result++;
                }
            }

            return result;
        }

        private ICellStyle GetStyle(IWorkbook wb, ExportFieldAttribute attr)
        {
            ICellStyle cellStyle = wb.CreateCellStyle();
            if (attr.Align == FieldAlign.Left)
            {
                cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
            }
            else if (attr.Align == FieldAlign.Center)
            {
                cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            }
            else
            {
                cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
            }


            return cellStyle;
        }

        /// <summary>
        /// 格式设置
        /// </summary>
        private ICellStyle Getcellstyle(IWorkbook wb, string type)
        {
            ICellStyle cellStyle = wb.CreateCellStyle();
            //定义字体  
            IFont font = wb.CreateFont();
            font.FontName = "微软雅黑";
            //水平对齐  
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
            //垂直对齐  
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            //自动换行  
            cellStyle.WrapText = true;
            //缩进
            cellStyle.Indention = 0;

            switch (type)
            {
                case "head":
                    cellStyle.SetFont(font);
                    cellStyle.Alignment = HorizontalAlignment.Center;
                    break;
                default:
                    cellStyle.SetFont(font);
                    break;
            }
            return cellStyle;
        }
        #endregion
    }
}
