using DapperModelCore.DapperCore;
using DapperModelCore.DBModel;
using DapperService.Dto;
using DapperService.IService;
using DapperUtilTool.Extensions;
using Kogel.Dapper.Extension;
using Kogel.Dapper.Extension.Expressions;
using Kogel.Dapper.Extension.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DapperService.Service
{
    #region Values表操作的服务
    /// <summary>
    /// 
    /// </summary>
    public class ValuesServices : IValuesServices
    {
        /// <summary>
        /// 仓储接口
        /// </summary>
        private IRepository _iRepository { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iRepository"></param>      
        public ValuesServices(IRepository iRepository)
        {
            _iRepository = iRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Values RepositoryGetByID(int Id)
        {
            return _iRepository.FindEntity<Values>(m => m.Id == Id);
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PageList<Values> RepositoryPageList(int pageIndex, int pageSize)
        {
            Dictionary<int, Expression<Func<Values, object>>> dic = new Dictionary<int, Expression<Func<Values, object>>>();
            dic.Add(2, m => m.Id);
            return _iRepository.GetConditionPageList<Values>(m => m.Id > 0, pageIndex, pageSize, null);

        }
        /// <summary>
        /// 获取指定列的集合
        /// </summary>
        /// <returns></returns>
        public List<ValuesOutputDto> GetList()
        {
            Expression<Func<Values, bool>> prediate = m => m.Id > 0;
            var selector = ExpExtension.GetSelector<Values, ValuesOutputDto>();
            return _iRepository.GetList<Values, ValuesOutputDto>(prediate, null, null, selector);
        }
        /// <summary>
        /// 新增一个实体
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int InsertEntity(ValuesInputDto input)
        {
            var model = new Values() { Vals = input.Vals,Keys=input.Keys };
            return _iRepository.InsertIdentity<Values>(model);
        }
        /// <summary>
        /// 测试批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public int InsertBatchEntity(ValuesInputDto input)
        {
            List<Values> modelList = new List<Values>();
            for (var i = 0; i < 100; i++)
            {
                var model = new Values() { Vals = input.Vals+i.ToString(), Keys = input.Keys+i.ToString() };
                modelList.Add(model);
            }
            return _iRepository.InsertEntityList<Values>(modelList);
        }


        /// <summary>
        /// 修改一个实体
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int UpdateEntity(ValuesInputDto input)
        {
            if (input.Id.GetValueOrDefault(0) == 0)
            {
                return 0;
            }
            return _iRepository.UpdateEntity<Values>(m => m.Id == input.Id, q => new Values() { Vals = input.Vals, Keys = input.Keys }) ;
        }

        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int DeleteEntity(ValuesInputDto input)
        {
            if (input.Id.GetValueOrDefault(0) == 0)
            {
                return 0;
            }
            return _iRepository.DeleteEntity<Values>(m => m.Id == input.Id);
        }

        /// <summary>
        /// 获取指定列的分页信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PageList<ValuesOutputDto> GetStockPageList(int pageIndex, int pageSize)
        {
            Expression<Func<Values, bool>> prediate = m => m.Id > 0;
            prediate = prediate.And(m => m.Id > 10);
            var selector = ExpExtension.GetSelector<Values, ValuesOutputDto>();
            return _iRepository.GetConditionPageList<Values, ValuesOutputDto>(prediate, pageIndex, pageSize, null,selector);
        }
        
    }
    #endregion

}
