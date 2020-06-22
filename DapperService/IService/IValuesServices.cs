using DapperModelCore.DBModel;
using DapperService.Dto;
using Kogel.Dapper.Extension.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DapperService.IService
{
    /// <summary>
    /// 注入的服务接口
    /// </summary>
    public interface IValuesServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Values RepositoryGetByID(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        PageList<Values> RepositoryPageList(int pageIndex, int pageCount);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        PageList<ValuesOutputDto> GetStockPageList(int pageIndex, int pageCount);
        /// <summary>
        /// 测试集合
        /// </summary>
        /// <returns></returns>
        List<ValuesOutputDto> GetList();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        int InsertEntity(ValuesInputDto input);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        int UpdateEntity(ValuesInputDto input);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        int DeleteEntity(ValuesInputDto input);
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int InsertBatchEntity(ValuesInputDto input);
    }
}
