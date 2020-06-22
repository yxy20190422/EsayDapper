using DapperModelCore.DapperCore;
using DapperModelCore.DBModel;
using DapperService.Dto;
using DapperService.Dto.OtherServiceDto;
using DapperService.Export;
using DapperService.IService;
using DapperUtilTool.Common;
using DapperUtilTool.CoreModels;
using DapperUtilTool.Extensions;
using Kogel.Dapper.Extension;
using Kogel.Dapper.Extension.Expressions;
using Kogel.Dapper.Extension.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Primitives;
using MySqlX.XDevAPI.Common;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace DapperService.Service
{
    /// <summary>
    /// 物料服务
    /// </summary>
    public class StockServices : IStockServices
    {
        /// <summary>
        /// 仓储接口
        /// </summary>
        private IRepository _iRepository { get; set; }

        private IHttpClientFactory _apiRequest { get; set; }

        /// <summary>
        /// 上传的Excel存入的位置
        /// </summary>
        private readonly IWebHostEnvironment hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iRepository"></param>
        /// <param name="env"></param>
        /// <param name="apiRequest"></param>
        public StockServices(IRepository iRepository, IWebHostEnvironment env, IHttpClientFactory apiRequest)
        {
            _iRepository = iRepository;
            this.hostingEnvironment = env;
            _apiRequest = apiRequest;
        }

        /// <summary>
        /// 点击保存按键写入数据库操作
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        public OnlineOrdersResultDto UploadStock(UploadStockInputDto inputDto)
        {
            OnlineOrdersResultDto res = new OnlineOrdersResultDto() { StatusCode = 0, Message = "保存数据失败" };
            if (string.IsNullOrWhiteSpace(inputDto.FileName))
            {
                res.Message = "Excel文件名系统未找到";
                return res;
            }
            string fileSavePath = hostingEnvironment.WebRootPath + @"\upload\stock\";
            string filename = fileSavePath + inputDto.FileName;
            if (!File.Exists(filename))
            {
                res.Message = "系统未找到上传的文件";
                return res;
            }
            if (inputDto.UpFileds == null || inputDto.UpFileds.Count == 0)
            {
                res.Message = "请下方拖拽要上传的字段";
                return res;
            }

            #region 获取表头最新的流水号
            var flownumber = string.Empty;
            string preFixNo = "BJ" + DateTime.Now.Date.ToString("yyyy-MM-dd").Replace("-", "");
            var uploadinfo = _iRepository.GetList<Ppc_UploadInfo, Ppc_UploadInfo>(m => m.UploadNO.StartsWith(preFixNo), null, null, m => new Ppc_UploadInfo { UploadNO = Kogel.Dapper.Extension.Function.Max(m.UploadNO) });
            if (uploadinfo == null || uploadinfo.Count == 0 || string.IsNullOrWhiteSpace(uploadinfo[0].UploadNO))
            {
                flownumber = preFixNo + "0001";
            }
            else
            {
                var dbnumber = uploadinfo.FirstOrDefault().UploadNO;
                flownumber = preFixNo + Common.GetMaxNo(dbnumber);
            }
            if (string.IsNullOrEmpty(flownumber))
            {
                res.Message = "未生成比价流水号";
                return res;
            }
            #endregion
            string extName = Path.GetExtension(filename);
            List<Ppc_StockInfo> stockList = new List<Ppc_StockInfo>();//物料信息表
            List<Ppc_UploadStockStepqty> stepList = new List<Ppc_UploadStockStepqty>();//阶梯表
            StringBuilder sb = new StringBuilder();
            StringBuilder sbTotal = new StringBuilder();
            DateTime dtNow = DateTime.Now;
            //读取Excel数据
            using (Stream sm = System.IO.File.OpenRead(filename))
            {
                ISheet sheet = null;
                IRow firstRow = null;
                Dictionary<string, int> Dicols = new Dictionary<string, int>();
                //数据开始行(排除标题行)
                int startRow = 0;
                try
                {
                    //根据文件流创建excel数据结构,NPOI的工厂类WorkbookFactory会自动识别excel版本，创建出不同的excel数据结构
                    IWorkbook workbook = extName.ToLower() == ".xls" ? new HSSFWorkbook(sm) : WorkbookFactory.Create(sm);
                    //如果没有指定的sheetName，则尝试获取第一个sheet
                    sheet = workbook.GetSheetAt(0);
                    if (sheet != null)
                    {
                        firstRow = sheet.GetRow(0);
                        //一行最后一个cell的编号 即总的列数
                        int cellCount = firstRow.LastCellNum;

                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (!string.IsNullOrWhiteSpace(cellValue) && !Dicols.ContainsKey(cellValue))
                                {
                                    Dicols.Add(cellValue, i);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                        //最后一列的标号
                        int rowCount = sheet.LastRowNum;
                        Guid guidRow = Guid.Empty; //关联物料表与价格梯度表;
                        for (int i = startRow; i <= rowCount; i++)
                        {
                            bool xinghaoRowFlag = true;//标记是否为型号行,默认为是型号行
                            Ppc_StockInfo dbModel = new Ppc_StockInfo();
                            IRow row = sheet.GetRow(i);
                            #region Excel已经保存在服务器上面，客户端怎么拖拽上传都无法改变行的性质(要么物料行，要么阶梯行)
                            ICell cell = row.GetCell(0);
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString().Trim()))
                            {
                                guidRow = Guid.NewGuid();
                                xinghaoRowFlag = true;
                            }
                            else if (i == 1)
                            {
                                res.Message = "第一行数据行必须为型号行";
                                return res;
                            }
                            else
                            {
                                xinghaoRowFlag = false;
                            }
                            #endregion
                            if (row == null || row.FirstCellNum < 0) continue; //没有数据的行默认是null　　　　　　　
                            bool IsEmptyRow = false;//是否是空行判断
                            for (int j = row.FirstCellNum; j < cellCount; j++)
                            {
                                string columnname = firstRow.GetCell(j).StringCellValue;//excel中上传的列名对应数据库中的列名称
                                string uploadcolname = inputDto.UpFileds.Find(m => m.Key == columnname) != null ? inputDto.UpFileds.Find(m => m.Value == columnname).Value : string.Empty; //用户拖拽的实际列名                                                                                                                                                                                          
                                if (xinghaoRowFlag)
                                {
                                    IsEmptyRow = SetDbModelData(dbModel, Dicols, columnname, uploadcolname, row, i, sb, flownumber, dtNow, IsEmptyRow, stepList, guidRow.ToString());
                                    dbModel.Guid = guidRow.ToString();
                                }
                                else
                                {
                                    //针对阶梯行的数据检查
                                    IsEmptyRow = SetDbModelLadderData(dbModel, Dicols, columnname, uploadcolname, row, i, sb, flownumber, dtNow, IsEmptyRow, stepList, guidRow.ToString());
                                }
                            }
                            if (sb.Length == 0 && xinghaoRowFlag)
                            {
                                stockList.Add(dbModel);
                            }
                            if (!IsEmptyRow)
                            {
                                break;
                            }
                            if (sb.Length > 0 && IsEmptyRow)
                            {
                                sbTotal.Append(sb.ToString());
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            #region 写入数据库
            if (sbTotal != null && sbTotal.Length > 0)
            {
                res.Message = sbTotal.ToString();
                return res;
            }
            if (stockList != null && stockList.Count > 0)
            {
                var rowsCount = _iRepository.InsertEntityList<Ppc_StockInfo>(stockList);
                if (rowsCount > 0)
                {
                    Ppc_UploadInfo head = new Ppc_UploadInfo()
                    {
                        UploadNO = flownumber,
                        Des = inputDto.Remark,
                        Status = (int)EnumCenter.Ppc_UploadInfo_Status.InterfaceDocking,
                        CreateTime = dtNow,
                        UpdateTime = dtNow,
                        CreateID = SessionDataResult.LoginUseID,
                        CreateName = SessionDataResult.LoginUseName
                    };
                    var headCount = _iRepository.Insert<Ppc_UploadInfo>(head);
                    if (headCount > 0)
                    {
                        #region 写入价格阶梯配置表
                        if (stepList != null && stepList.Count > 0)
                        {
                            var stepCount = _iRepository.InsertEntityList<Ppc_UploadStockStepqty>(stepList);
                            if (stepCount < 0)
                            {
                                res.Message = "数据导入成功但是阶梯数据未导入成功";
                                return res;
                            }
                        }
                        #endregion
                        res.StatusCode = 1;
                        res.Message = "导入库存数据成功";
                        return res;
                    }
                    else
                    {
                        res.Message = "写入上传信息失败";
                        return res;
                    }
                }
                else
                {
                    res.Message = "写入上传物料信息失败";
                    return res;
                }
            }
            #endregion
            return res;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbModel"></param>
        /// <param name="Dicols"></param>
        /// <param name="columnname"></param>
        /// <param name="uploadcolname"></param>
        /// <param name="row"></param>
        /// <param name="rowIndex"></param>
        /// <param name="sb"></param>
        /// <param name="flownumber"></param>
        /// <param name="dtNow"></param>
        /// <param name="IsEmptyRow"></param>
        /// <param name="stepList"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        private bool SetDbModelData(Ppc_StockInfo dbModel, Dictionary<string, int> Dicols, string columnname, string uploadcolname, IRow row, int rowIndex, StringBuilder sb, string flownumber, DateTime dtNow, bool IsEmptyRow, List<Ppc_UploadStockStepqty> stepList, string guid)
        {
            if (Dicols.ContainsKey(uploadcolname))
            {
                int rightIndex = Dicols[uploadcolname];
                ICell cell = row.GetCell(rightIndex);
                if (cell != null)
                {
                    if (columnname.Contains("型号"))
                    {
                        if (!string.IsNullOrWhiteSpace(cell.ToString().Trim()))
                        {
                            dbModel.Model = row.GetCell(rightIndex).ToString().Trim();
                            IsEmptyRow = true;
                        }
                        else
                        {
                            sb.Append(string.Format("第{0}行型号不能为空", rowIndex));
                        }
                    }
                    else if (columnname.Contains("品牌"))
                    {
                        if (!string.IsNullOrWhiteSpace(cell.ToString().Trim()))
                        {
                            dbModel.Brand = row.GetCell(rightIndex).ToString().Trim();
                            IsEmptyRow = true;
                        }
                        else
                        {
                            sb.Append(string.Format("第{0}行品牌不能为空", rowIndex));
                        }
                    }
                    else if (columnname.Contains("库存"))
                    {
                        int Qty = 0;
                        if (!string.IsNullOrWhiteSpace(cell.ToString().Trim()))
                        {
                            if (int.TryParse(row.GetCell(rightIndex).ToString().Trim(), out Qty))
                            {
                                dbModel.InvQty = Qty;
                                IsEmptyRow = true;
                            }
                            else
                            {
                                sb.Append(string.Format("第{0}行库存只能填入数字", rowIndex));
                            }
                        }
                        else
                        {
                            sb.Append(string.Format("第{0}行库存不能为空", rowIndex));
                        }

                    }
                    else if (columnname.Contains("SPQ"))
                    {
                        int SPQ = 0;
                        if (!string.IsNullOrWhiteSpace(cell.ToString().Trim()))
                        {
                            if (int.TryParse(row.GetCell(rightIndex).ToString().Trim(), out SPQ))
                            {
                                dbModel.SPQ = SPQ;
                                IsEmptyRow = true;
                            }
                            else
                            {
                                sb.Append(string.Format("第{0}行SPQ只能填入数字", rowIndex));
                            }
                        }
                        else
                        {
                            sb.Append(string.Format("第{0}行SPQ不能为空", rowIndex));
                        }
                    }
                    else if (columnname.Contains("MOQ"))
                    {
                        int MOQ = 0;
                        if (!string.IsNullOrWhiteSpace(cell.ToString().Trim()))
                        {
                            if (int.TryParse(row.GetCell(rightIndex).ToString().Trim(), out MOQ))
                            {
                                dbModel.MOQ = MOQ;
                                IsEmptyRow = true;
                            }
                            else
                            {
                                sb.Append(string.Format("第{0}行MOQ只能填入数字", rowIndex));
                            }
                        }
                        else
                        {
                            sb.Append(string.Format("第{0}行MOQ不能为空", rowIndex));
                        }
                    }
                    else if (columnname.Contains("供应商报价"))
                    {
                        decimal CostPrice = 0;
                        if (!string.IsNullOrWhiteSpace(cell.ToString().Trim()))
                        {
                            if (decimal.TryParse(row.GetCell(rightIndex).ToString().Trim(), out CostPrice))
                            {
                                dbModel.CostPrice = CostPrice;
                                IsEmptyRow = true;
                            }
                            else
                            {
                                sb.Append(string.Format("第{0}行供应商报价只能填入数字", rowIndex));
                            }
                        }
                        else
                        {
                            sb.Append(string.Format("第{0}行供应商报价不能为空", rowIndex));
                        }
                    }
                    else if (columnname.Contains("批次"))
                    {
                        if (!string.IsNullOrWhiteSpace(cell.ToString().Trim()))
                        {
                            dbModel.BatchNo = row.GetCell(rightIndex).ToString().Trim();
                            IsEmptyRow = true;
                        }
                    }
                    else if (columnname.Contains("封装"))
                    {
                        if (!string.IsNullOrWhiteSpace(cell.ToString().Trim()))
                        {
                            dbModel.Encapsulation = row.GetCell(rightIndex).ToString().Trim();
                            IsEmptyRow = true;
                        }
                    }
                    else if (columnname.Contains("描述"))
                    {
                        if (!string.IsNullOrWhiteSpace(cell.ToString().Trim()))
                        {
                            dbModel.Remark = row.GetCell(rightIndex).ToString().Trim();
                            IsEmptyRow = true;
                        }

                    }
                    else if (columnname.Contains("货期(SZ)"))
                    {

                        if (!string.IsNullOrWhiteSpace(cell.ToString().Trim()))
                        {
                            dbModel.SZDelivery = row.GetCell(rightIndex).ToString().Trim(); ;
                            IsEmptyRow = true;
                        }
                        else
                        {
                            sb.Append(string.Format("第{0}行型号不能为空", rowIndex));
                        }
                    }
                    else if (columnname.Contains("货期(HK)"))
                    {
                        if (!string.IsNullOrWhiteSpace(cell.ToString().Trim()))
                        {
                            dbModel.HKDelivery = row.GetCell(rightIndex).ToString().Trim();
                            IsEmptyRow = true;
                        }
                    }
                    else if (columnname.Contains("阶梯"))
                    {
                        if (!string.IsNullOrWhiteSpace(cell.ToString().Trim()))
                        {
                            int Qty = 0;
                            if (int.TryParse(row.GetCell(rightIndex).ToString().Trim(), out Qty))
                            {
                                var stepModel = MakeStockStepqty(Qty, dtNow, flownumber, guid);
                                stepList.Add(stepModel);
                                IsEmptyRow = true;
                            }
                            else
                            {
                                sb.Append(string.Format("第{0}行阶梯只能填入数字", rowIndex));
                            }
                        }
                    }
                }
            }
            else
            {
                if (columnname.Contains("型号"))
                {
                    sb.Append(string.Format("第{0}行型号必须拖拽上传", rowIndex));
                }
                else if (columnname.Contains("品牌"))
                {
                    sb.Append(string.Format("第{0}行品牌必须拖拽上传", rowIndex));
                }
                else if (columnname.Contains("库存"))
                {
                    sb.Append(string.Format("第{0}行库存必须拖拽上传", rowIndex));
                }
                else if (columnname.Contains("SPQ"))
                {
                    sb.Append(string.Format("第{0}行SPQ必须拖拽上传", rowIndex));
                }
                else if (columnname.Contains("MOQ"))
                {
                    sb.Append(string.Format("第{0}行MOQ必须拖拽上传", rowIndex));
                }
                else if (columnname.Contains("供应商报价"))
                {
                    sb.Append(string.Format("第{0}行供应商报价必须拖拽上传", rowIndex));
                }
                else if (columnname.Contains("货期(SZ)"))
                {
                    sb.Append(string.Format("第{0}行货期(sz)必须拖拽上传", rowIndex));
                }
            }
            if (!dbModel.Status.HasValue)
            {
                dbModel.Status = (int)EnumCenter.Ppc_StockInfo_Status.InterfaceDocking;
            }
            if (!dbModel.CreateTime.HasValue)
            {
                dbModel.CreateTime = dtNow;
            }
            if (string.IsNullOrEmpty(dbModel.UploadNo))
            {
                dbModel.UploadNo = flownumber;
            }
            if (string.IsNullOrWhiteSpace(guid))
            {
                dbModel.Guid = guid;
            }
            return IsEmptyRow;
        }


        /// <summary>
        /// 上传阶梯的参数
        /// </summary>
        /// <param name="Qty"></param>
        /// <param name="dtNow"></param>
        /// <param name="UploadNo"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        private Ppc_UploadStockStepqty MakeStockStepqty(int? Qty, DateTime dtNow, string UploadNo, string guid)
        {
            Ppc_UploadStockStepqty step = new Ppc_UploadStockStepqty();
            step.MaxVal = Qty;
            step.StockinfoGuid = guid;
            step.UpdateTime = dtNow;
            step.UploadNo = UploadNo;
            return step;
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="dbModel"></param>
       /// <param name="Dicols"></param>
       /// <param name="columnname"></param>
       /// <param name="uploadcolname"></param>
       /// <param name="row"></param>
       /// <param name="rowIndex"></param>
       /// <param name="sb"></param>
       /// <param name="flownumber"></param>
       /// <param name="dtNow"></param>
       /// <param name="IsEmptyRow"></param>
       /// <param name="stepList"></param>
       /// <param name="guid"></param>
       /// <returns></returns>
        private bool SetDbModelLadderData(Ppc_StockInfo dbModel, Dictionary<string, int> Dicols, string columnname, string uploadcolname, IRow row, int rowIndex, StringBuilder sb, string flownumber, DateTime dtNow, bool IsEmptyRow, List<Ppc_UploadStockStepqty> stepList, string guid)
        {
            //阶梯行阶梯列的检查
            if (columnname.Contains("阶梯"))
            {
                if (!string.IsNullOrWhiteSpace(uploadcolname) && Dicols.ContainsKey(uploadcolname))
                {
                    int rightIndex = Dicols[uploadcolname];
                    ICell cell = row.GetCell(rightIndex);
                    if (cell != null)
                    {
                        if (!string.IsNullOrWhiteSpace(cell.ToString().Trim()))
                        {
                            int Qty = 0;
                            if (int.TryParse(row.GetCell(rightIndex).ToString().Trim(), out Qty))
                            {
                                var stepModel = MakeStockStepqty(Qty, dtNow, flownumber, guid);
                                stepList.Add(stepModel);
                                IsEmptyRow = true;
                            }
                            else
                            {
                                sb.Append(string.Format("第{0}行阶梯只能填入数字", rowIndex));
                            }
                        }
                        else
                        {
                            sb.Append(string.Format("第{0}行阶梯行数量不能为空", rowIndex));
                        }
                    }
                }
                else
                {
                    sb.Append(string.Format("第{0}行阶梯列数据必须上传", rowIndex));
                }
            }
            return IsEmptyRow;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public OnlineOrdersResultDto<PageInfo<Ppc_UploadInfoOutPutPageListDto>> GetPageUploadinfo(Ppc_UploadInfoInputPageListDto input)
        {
            
            OnlineOrdersResultDto<PageInfo<Ppc_UploadInfoOutPutPageListDto>> list = new OnlineOrdersResultDto<PageInfo<Ppc_UploadInfoOutPutPageListDto>>() { StatusCode = 1, Message = "获取信息成功" };
            if (string.IsNullOrEmpty(SessionDataResult.LoginUseID))
            {
                list.StatusCode = 0;
                list.Message = "登录信息已过期";
                return list;
            }
            ComparePriceToCrmApiResultDto<List<CrmPurchaseOutPutDto>> crmOutPut = new ComparePriceToCrmApi(_apiRequest).GetPurchaseBelongsFromCrm(new PurchaseInputDto() { CRMUserId = SessionDataResult.LoginUseID }).Result;
            if (crmOutPut.State <= 0)
            {
                list.StatusCode = 0;
                list.Message = crmOutPut.Message;
                return list;
            }
            string[] ennameArray = crmOutPut.ReValue.Where(m => !string.IsNullOrWhiteSpace(m.EName)).Select(m => m.EName).Distinct().ToArray();
            Expression<Func<Ppc_UploadInfo,bool>> exp= m=>1 > 0;
            if (ennameArray != null && ennameArray.Length > 0)
            {
                exp = exp.And(m => ennameArray.Contains(m.CreateName));
            }
            Dictionary<int, Expression<Func<Ppc_UploadInfo, object>>> dic = new Dictionary<int, Expression<Func<Ppc_UploadInfo, object>>>();
            dic.Add(2, m => m.CreateTime);
            var selector = ExpExtension.GetSelector<Ppc_UploadInfo, Ppc_UploadInfoOutPutPageListDto>();
            var pageEntity = _iRepository.GetConditionPageList<Ppc_UploadInfo, Ppc_UploadInfoOutPutPageListDto>(exp, input.PageIndex, input.PageSize, dic, selector);
            list.Ext1 = pageEntity.GetCommonPageEntityies<Ppc_UploadInfoOutPutPageListDto>();
            return list;
        }

        #region 根据比价单号获取物料上传信息的详情
        /// <summary>
        /// 根据比价单号获取物料上传信息的详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OnlineOrdersResultDto<PageInfo<Ppc_StockInfoOutputPageListDto>> GetPageStock(Ppc_StockInfoInputPageListDto input)
        {
            OnlineOrdersResultDto<PageInfo<Ppc_StockInfoOutputPageListDto>> result = new OnlineOrdersResultDto<PageInfo<Ppc_StockInfoOutputPageListDto>>()
            {
                StatusCode = 0,
                Message = "获取信息失败",
                Ext1 = new PageInfo<Ppc_StockInfoOutputPageListDto>()
            };
            if (string.IsNullOrWhiteSpace(input.UploadNo))
            {
                result.Message = "上传比价单号不能为空";
                return result;
            }
            var updatetor = _iRepository.FindEntity<Ppc_UploadInfo>(m => m.UploadNO == input.UploadNo);
            if (updatetor == null)
            {
                result.Message = "上传记录未找到";
                return result;
            }
            if (updatetor.Status != 0)
            {
                result.StatusCode = 1;
                result.Message = "物料正在比对中";
                return result;
            }
            Expression<Func<Ppc_StockInfo, bool>> where = m => m.UploadNo == input.UploadNo;
            where = GetCommonWhere(where, input);
            var selector = ExpExtension.GetSelector<Ppc_StockInfo, Ppc_StockInfoOutputPageListDto>();
            var pageEntity = _iRepository.GetConditionPageList<Ppc_StockInfo, Ppc_StockInfoOutputPageListDto>(where, input.PageIndex, input.PageSize, null, selector);
            var resultList = pageEntity.GetCommonPageEntityies<Ppc_StockInfoOutputPageListDto>();
            if (resultList != null && resultList.ResultList != null && resultList.ResultList.Count > 0)
            {
                var list = GetExtendPropertities(resultList.ResultList);
                resultList.ResultList = list;
            }
            return result;
        }

        /// <summary>
        /// 扩展属性
        /// </summary>
        /// <param name="List"></param>
        /// <returns></returns>
        private List<Ppc_StockInfoOutputPageListDto> GetExtendPropertities(List<Ppc_StockInfoOutputPageListDto> List)
        {
            long?[] PIDArray = List.Where(m => m.PID.GetValueOrDefault(0) > 0).Select(m => m.PID).Distinct().ToArray();
            if (PIDArray != null && PIDArray.Length > 0)
            {
                #region 获取系统阶梯价格
                Expression<Func<Ppc_StockinfoPrice, bool>> where = m => PIDArray.Contains(m.StockID);
                Dictionary<int, Expression<Func<Ppc_StockinfoPrice, object>>> order = new Dictionary<int, Expression<Func<Ppc_StockinfoPrice, object>>>();
                order.Add(1, m => m.MaxVal);
                List<Ppc_StockinfoPrice> stockpriceList = _iRepository.GetEntitiesListByConditions<Ppc_StockinfoPrice>(where, order);
                #endregion
                #region 获取第三方价格配置表
                Expression<Func<Ppc_PlatformDiffprice, bool>> wherediff = m => PIDArray.Contains(m.StockID);
                Dictionary<int, Expression<Func<Ppc_PlatformDiffprice, object>>> orderdic = new Dictionary<int, Expression<Func<Ppc_PlatformDiffprice, object>>>();
                orderdic.Add(1, m => m.Platformid);
                List<Ppc_PlatformDiffprice> stockdiffList = _iRepository.GetEntitiesListByConditions<Ppc_PlatformDiffprice>(wherediff, orderdic);
                #endregion

                #region 第三方价格梯度表
                Expression<Func<Ppc_PlatformPrice, bool>> otherwhere = m => PIDArray.Contains(m.StockID);
                Dictionary<int, Expression<Func<Ppc_PlatformPrice, object>>> otherdic = new Dictionary<int, Expression<Func<Ppc_PlatformPrice, object>>>();
                otherdic.Add(1, m => m.MaxVal);
                List<Ppc_PlatformPrice> otherpriceList = _iRepository.GetEntitiesListByConditions<Ppc_PlatformPrice>(otherwhere, otherdic);

                #endregion
                List<Ppc_StockinfoPriceGradientDto> SIDGradientList = new List<Ppc_StockinfoPriceGradientDto>();
                List<PlatformStockList> OtherPlatformList = new List<PlatformStockList>();
                List<Ppc_PlatformPriceGradientDto> ppcList = new List<Ppc_PlatformPriceGradientDto>();

                List.ForEach(m =>
                {
                    List<Ppc_StockinfoPrice> modelpriceList = stockpriceList.FindAll(q => q.StockID == m.PID);
                    if (modelpriceList != null && modelpriceList.Count > 0)
                    {
                        m.QtyStr = modelpriceList.Max(q => q.MaxVal).GetValueOrDefault(0).ToString();
                        foreach (var item in modelpriceList)
                        {
                            Ppc_StockinfoPriceGradientDto pricedto = new Ppc_StockinfoPriceGradientDto();
                            pricedto.MaxVal = item.MaxVal;
                            pricedto.CNYPrice = item.CNYPrice;
                            SIDGradientList.Add(pricedto);
                        }
                        m.SIDGradient = SIDGradientList;
                    }

                    List<Ppc_PlatformDiffprice> modelList = stockdiffList.FindAll(q => q.StockID == m.PID);
                    if (modelList != null && modelList.Count > 0)
                    {
                        foreach (var item in modelList)
                        {
                            PlatformStockList diff = new PlatformStockList();
                            diff.TiltleName = item.Name + "价格价差";
                            diff.PriceDiff = item.PriceDiff;
                            diff.MinPlatformPrice = item.MinPlatformPrice;
                            diff.Platformid = item.Platformid.GetValueOrDefault(0);
                            var platItemList = otherpriceList.FindAll(q => q.PlatformID == item.Platformid);
                            if (platItemList != null && platItemList.Count > 0)
                            {
                                foreach (var cc in platItemList)
                                {
                                    Ppc_PlatformPriceGradientDto dto = new Ppc_PlatformPriceGradientDto();
                                    dto.MaxVal = cc.MaxVal;
                                    dto.CNYPrice = cc.CNYPrice;
                                    ppcList.Add(dto);
                                }
                                diff.GradientList = ppcList;
                            }
                            OtherPlatformList.Add(diff);
                        }
                        m.OtherPlatformList = OtherPlatformList;
                    }
                });
            }
            return List;
        }


        #region 获取公共查询条件
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Expression<Func<Ppc_StockInfo, bool>> GetCommonWhere(Expression<Func<Ppc_StockInfo, bool>> where, Ppc_StockInfoInputPageListDto input)
        {
            if (!string.IsNullOrWhiteSpace(input.Model))
            {
                where = where.And(m => m.Model == input.Model);
            }
            if (!string.IsNullOrWhiteSpace(input.Brand))
            {
                where = where.And(m => m.Brand == input.Brand);
            }
            if (input.MinProfitBegin.GetValueOrDefault(0) > 0)
            {
                where = where.And(m => m.MinProfit >= input.MinProfitBegin);
            }
            if (input.MinProfitEnd.GetValueOrDefault(0) > 0)
            {
                where = where.And(m => m.MinProfit <= input.MinProfitEnd);
            }
            if (input.ProfitRateBegin.GetValueOrDefault(0) > 0)
            {
                decimal? beginrate = input.ProfitRateBegin / 100;
                where = where.And(m => m.ProfitRate >= beginrate);
            }
            if (input.ProfitRateEnd.GetValueOrDefault(0) > 0)
            {
                decimal? endrate = input.ProfitRateEnd / 100;
                where = where.And(m => m.ProfitRate <= endrate);
            }
            if (input.NegativeStockType.GetValueOrDefault(0) > 0)
            {
                where = where.And(m => m.NegativeStockType == input.NegativeStockType);
            }
            if (input.PricePlatCount.GetValueOrDefault(0) > 0)
            {
                where = where.And(m => m.PricePlatCount == input.PricePlatCount);
            }
            if (input.HeadNegativeStockType.GetValueOrDefault(0) > 0)
            {
                where = where.And(m => m.NegativeStockType == 1);
            }
            if (input.HeadPricePlatCount.GetValueOrDefault(0) > 0)
            {
                where = where.And(m => m.PricePlatCount > 0);
            }
            return where;
        }
        #endregion



        #endregion


        #region 统一配置价格策略
        /// <summary>
        /// 统一设置价格策略
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OnlineOrdersResultDto AddPriceConfig(Ppc_PriceConfigInputDto input)
        {
            OnlineOrdersResultDto result = new OnlineOrdersResultDto() { StatusCode = 0, Message = "统一设置价格策略失败" };

            if (string.IsNullOrWhiteSpace(input.UploadNo))
            {
                result.Message = "比价单号不能为空";
                return result;
            }
            var updatetor = _iRepository.FindEntity<Ppc_UploadInfo>(m => m.UploadNO == input.UploadNo);
            if (updatetor == null)
            {
                result.Message = "上传记录未找到";
                return result;
            }
            if (updatetor.Status != 0)
            {
                result.Message = "物料正在比对中,请等程序运行完成后再设置价格策略";
                return result;
            }
            if (input.AdjustType == 1)
            {
                //自动调价过滤
                if (input.MaxProfitRate.GetValueOrDefault(0) <= 0)
                {
                    result.Message = "最高毛利率不能为空";
                    return result;
                }
                if (input.MinProfitRate.GetValueOrDefault(0) <= 0)
                {
                    result.Message = "最低毛利率不能为空";
                    return result;
                }
                if (input.PriceRatio.GetValueOrDefault(0) <= 0)
                {
                    result.Message = "跟价系数不能为空";
                    return result;
                }
                if (input.PlatformIDArray == null || input.PlatformIDArray.Length == 0)
                {
                    result.Message = "对标平台必须选择";
                    return result;
                }
                if (input.IsUnpacking == 1)
                {
                    if (input.IsPriceLadder == 1)
                    {
                        //不使用对标平台梯度价格
                        if (input.CustermerCongigList == null || input.CustermerCongigList.Count == 0)
                        {
                            result.Message = "未提交有效的价格梯度";
                            return result;
                        }
                    }
                    //可拆包
                }
                else if (input.IsUnpacking == 2)
                {
                    if (input.CustomerSupplierList == null || input.CustomerSupplierList.Count == 0)
                    {
                        result.Message = "未提交有效的SPQ价格梯度";
                        return result;
                    }
                }
            }
            else if (input.AdjustType == 2)
            {
                //手动条件过滤
                if (input.CurrentProfitRate.GetValueOrDefault(0) < 0)
                {
                    result.Message = "调整的当前利率不能为空";
                    return result;
                }
            }

            if (input.AdjustArange.GetValueOrDefault(0) == 1)
            {
                if (input.StockIDArray == null || input.StockIDArray.Length == 0)
                {
                    result.Message = "未勾选任何有效的物料";
                    return result;
                }
                _iRepository.UpdateEntity<Ppc_StockInfo>(m => input.StockIDArray.Contains(m.PID), t => new Ppc_StockInfo
                {
                    Status = 2
                });
            }
            else if (input.AdjustArange.GetValueOrDefault(0) == 2)
            {
                Ppc_StockInfoInputPageListDto inputwhere = new Ppc_StockInfoInputPageListDto()
                {
                    UploadNo = input.UploadNo,
                    Model = input.Model,
                    Brand = input.Brand,
                    MinProfitBegin = input.MinProfitBegin,
                    MinProfitEnd = input.MinProfitEnd,
                    ProfitRateBegin = input.ProfitRateBegin,
                    ProfitRateEnd = input.ProfitRateEnd,
                    NegativeStockType = input.NegativeStockType,
                    PricePlatCount = input.PricePlatCount,
                    HeadNegativeStockType = input.HeadNegativeStockType,
                    HeadPricePlatCount = input.HeadPricePlatCount,
                };
                var where = GetCommonWhere(m => 1 == 1, inputwhere);
                _iRepository.UpdateEntity<Ppc_StockInfo>(where, t => new Ppc_StockInfo
                {
                    Status = 2
                });
            }
            #region 更新上传记录的操作次数，将上传信息更新为待对比
            _iRepository.UpdateEntity<Ppc_UploadInfo>(m => m.UploadNO == input.UploadNo, t => new Ppc_UploadInfo
            {
                Status = 1
            });
            #endregion

            #region 存在未删除的价格配置数据,就把它置为删除
            var configExs = _iRepository.FindEntity<Ppc_PriceConfig>(m => m.UploadNo == input.UploadNo && m.IsDelete == 1);
            if (configExs != null && configExs.ID.GetValueOrDefault(0) > 0)
            {
                _iRepository.UpdateEntity<Ppc_PriceConfig>(m => m.UploadNo == input.UploadNo && m.IsDelete == 1, t => new Ppc_PriceConfig
                {
                    IsDelete = 0
                });
            }
            #endregion
            List<Ppc_PlatformPriceConfig> itemList = new List<Ppc_PlatformPriceConfig>();
            Ppc_PriceConfig config = new Ppc_PriceConfig();
            config.UploadNo = input.UploadNo;
            if (input.AdjustType == 1)
            {
                config.AdjustType = 1;
                config.MaxProfitRate = input.MaxProfitRate / 100;
                config.MinProfitRate = input.MinProfitRate / 100;
                config.PriceRatio = input.PriceRatio;
                #region 获取所有的对标平台信息
                List<Ppc_PlatformConfig> platList = _iRepository.GetEntitiesListByConditions<Ppc_PlatformConfig>(m => input.PlatformIDArray.Contains(m.ID), null);
                if (platList == null || platList.Count == 0)
                {
                    result.Message = "平台信息未找到";
                    return result;
                }
                #endregion
                config.ComparePlatform = string.Join(",", platList.Select(m => m.PlatformType).Distinct().ToArray());
                config.IsUnpacking = input.IsUnpacking;
                if (input.IsUnpacking == 1)
                {
                    //自动调价
                    if (input.IsPriceLadder == 0)
                    {
                        //使用对标平台价格策略
                        config.IsPriceLadder = 1;
                    }
                    else if (input.IsPriceLadder == 1)
                    {
                        config.IsPriceLadder = 0;
                        foreach (var item in input.CustermerCongigList)
                        {
                            Ppc_PlatformPriceConfig model = new Ppc_PlatformPriceConfig();
                            model.MaxVal = item.Qty;
                            model.ProfitRate = item.ProfitRate/100M;
                            itemList.Add(model);
                        }
                    }

                }
                else if (input.IsUnpacking == 2)
                {
                    //写入ppc_platformpriceconfig
                    foreach (var item in input.CustomerSupplierList)
                    {
                        Ppc_PlatformPriceConfig model = new Ppc_PlatformPriceConfig();
                        model.MaxVal = item.Qty;
                        model.ProfitRate = item.ProfitRate/100M;
                        itemList.Add(model);
                    }
                }
            }
            else if (input.AdjustType == 2)
            {
                //手动条件
                config.AdjustType = 2;
                config.ChangeProfitRate = input.CurrentProfitRate / 100M;
            }
            var rowCount = _iRepository.InsertIdentity<Ppc_PriceConfig>(config);
            if (rowCount > 0)
            {
                if (itemList != null && itemList.Count > 0)
                {
                    itemList.ForEach(m =>
                    {
                        m.PriceConfigID = rowCount;
                    });
                    var ccCount = _iRepository.InsertEntityList<Ppc_PlatformPriceConfig>(itemList);
                    if (ccCount < 0)
                    {
                        result.Message = "平台价格配置写入失败";
                        return result;
                    }
                }
                result.StatusCode = 1;
                result.Message = "设置价格策略成功";
            }
            else {
                result.Message = "价格策略设置失败";
            }
            return result;
        }
        #endregion


        #region 导出层Excel功能开发
        /// <summary>
        /// 导出Excel供库存上传使用
        /// </summary>
        /// <param name="UploadNo">上传的库存的操作流水号</param>
        /// <returns></returns>
        public List<StockUploadModel> ExportUploadOutData(string UploadNo)
        {
            List<StockUploadModel> resultList = new List<StockUploadModel>();
            if (string.IsNullOrWhiteSpace(UploadNo))
            {
                return resultList;
            }
            Expression<Func<Ppc_StockInfo, bool>> where = m => m.UploadNo == UploadNo;
            var selector = ExpExtension.GetSelector<Ppc_StockInfo, Ppc_StockInfoExportDto>();
            var pageEntity = _iRepository.GetList<Ppc_StockInfo, Ppc_StockInfoExportDto>(where, null, selector);
            if (resultList != null && pageEntity.Count > 0)
            {
                List<Ppc_StockinfoPrice> listPrice = new List<Ppc_StockinfoPrice>();
                long?[] PIDArray = pageEntity.Where(m => m.PID.GetValueOrDefault(0) > 0).Select(m => m.PID).Distinct().ToArray();
                if (PIDArray != null && PIDArray.Length > 0)
                {
                    listPrice = _iRepository.GetEntitiesListByConditions<Ppc_StockinfoPrice>(m => PIDArray.Contains(m.StockID), null);
                }
                foreach (var item in pageEntity)
                {
                    var modelListPrice = listPrice.FindAll(m => m.StockID == item.PID).OrderBy(m => m.MaxVal).ToList();
                    StockUploadModel model = new StockUploadModel()
                    {
                        Model = item.Model,
                        Brand = item.Brand,
                        BatchNo = item.BatchNo,
                        Encapsulation = item.Encapsulation,
                        Remark = item.Remark,
                        InvQty = item.InvQty,
                        SPQ = item.SPQ,
                        MOQ = item.MOQ,
                        SZDelivery = item.SZDelivery,
                        HKDelivery = item.HKDelivery,
                        AreaQty1 = modelListPrice != null && modelListPrice.Count > 0 ? modelListPrice[0].MaxVal : null,
                        AreaPurchaseUnitPriceCNY1 = item.CostPrice,
                        AreaSalesPriceCNY1 = modelListPrice != null && modelListPrice.Count > 0 ? modelListPrice[0].CNYPrice : null,
                        AreaQty2 = modelListPrice != null && modelListPrice.Count > 1 ? modelListPrice[1].MaxVal : null,
                        AreaSalesPriceCNY2 = modelListPrice != null && modelListPrice.Count > 1 ? modelListPrice[1].CNYPrice : null,
                        AreaQty3 = modelListPrice != null && modelListPrice.Count > 2 ? modelListPrice[2].MaxVal : null,
                        AreaSalesPriceCNY3 = modelListPrice != null && modelListPrice.Count > 2 ? modelListPrice[2].CNYPrice : null,
                        AreaQty4 = modelListPrice != null && modelListPrice.Count > 3 ? modelListPrice[3].MaxVal : null,
                        AreaSalesPriceCNY4 = modelListPrice != null && modelListPrice.Count > 3 ? modelListPrice[3].CNYPrice : null,
                        AreaQty5 = modelListPrice != null && modelListPrice.Count > 4 ? modelListPrice[4].MaxVal : null,
                        AreaSalesPriceCNY5 = modelListPrice != null && modelListPrice.Count > 4 ? modelListPrice[4].CNYPrice : null,
                        AreaQty6 = modelListPrice != null && modelListPrice.Count > 5 ? modelListPrice[5].MaxVal : null,
                        AreaSalesPriceCNY6 = modelListPrice != null && modelListPrice.Count > 5 ? modelListPrice[5].CNYPrice : null,
                        AreaQty7 = modelListPrice != null && modelListPrice.Count > 6 ? modelListPrice[6].MaxVal : null,
                        AreaSalesPriceCNY7 = modelListPrice != null && modelListPrice.Count > 6 ? modelListPrice[6].CNYPrice : null,
                        AreaQty8 = modelListPrice != null && modelListPrice.Count > 7 ? modelListPrice[7].MaxVal : null,
                        AreaSalesPriceCNY8 = modelListPrice != null && modelListPrice.Count > 7 ? modelListPrice[7].CNYPrice : null,
                        AreaQty9 = modelListPrice != null && modelListPrice.Count > 8 ? modelListPrice[8].MaxVal : null,
                        AreaSalesPriceCNY9 = modelListPrice != null && modelListPrice.Count > 8 ? modelListPrice[8].CNYPrice : null,
                    };
                    resultList.Add(model);
                }
;            }
            return resultList;
        }
        #endregion
    }
}
