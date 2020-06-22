using Kogel.Dapper.Extension.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DapperModelCore.DapperCore
{
    public interface IRepository
    {
        #region Property
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        static string ConnectionString { get;  }
        #region 根据条件查询单个实体
        /// <summary>
        /// 根据条件查询单个实体
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>        
        /// <param name="predicate">条件</param>
        /// <returns>返回实体</returns>
        T FindEntity<T>(Expression<Func<T, bool>> predicate) where T : class;
        #endregion
        #region 单表分页查询
        /// <summary>
        /// 单表分页操作
        /// </summary>
        /// <typeparam name="T">分页的DBModel</typeparam>
        /// <param name="predicate">查询的条件</param>
        /// <param name="pageIndex">当前页码从1开始</param>
        /// <param name="pageSize">每页几条数据</param>
        /// <param name="dic">分页的排序字典key=1代表升序2代表降序</param>
        /// <returns></returns>
        PageList<T> GetConditionPageList<T>(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize, Dictionary<int,Expression<Func<T, object>>> dic);
        /// <summary>
        /// 单表分页操作
        /// </summary>
        /// <typeparam name="T">分页的DBModel</typeparam>
        /// <param name="predicate">查询的条件</param>
        /// <param name="pageIndex">当前页码从1开始</param>
        /// <param name="pageSize">每页几条数据</param>
        /// <param name="dic">分页的排序字典key=1代表升序2代表降序</param>
        /// <returns></returns>
        PageList<K> GetConditionPageList<T,K>(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize, Dictionary<int, Expression<Func<T, object>>> dic, Expression<Func<T, K>> Selector);
        #endregion
        #region 根据条件获取单个表数据的集合
        /// <summary>
        /// 根据条件获取实体的List集合
        /// </summary>
        /// <typeparam name="T">DBModel</typeparam>
        /// <param name="predicate">条件</param>
        /// <param name="dic">排序的字典</param>
        /// <returns></returns>
        List<T> GetEntitiesListByConditions<T>(Expression<Func<T, bool>> predicate, Dictionary<int, Expression<Func<T, object>>> dic);
        #endregion
        #region 新增单个实体
        /// <summary>
        /// 插入单个实体
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="entity">要插入的实体</param>
        /// <returns>返回受影响行数</returns>
        int Insert<T>(T entity) where T : class;
        #endregion
        #region 插入实体后获取最新的自增ID
        int InsertIdentity<T>(T entity) where T : class;
        #endregion
        #region 批量新增数据库实体
        /// <summary>
        /// 批量新增数据库实体
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="entity">要插入的实体</param>
        /// <returns>返回受影响行数</returns>
        int InsertEntityList<T>(List<T> list) where T : class;
        #endregion
        #region 根据传入的Lamada表达式删除数据
        /// <summary>
        /// 根据条件删除数据库表中的数据
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>        
        /// <param name="predicate">表达式</param>
        /// <returns>返回实体</returns>
        int DeleteEntity<T>(Expression<Func<T, bool>> predicate) where T : class;
        #endregion
        #region 根据传入的Lamada表达式修改表中的数据
        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        int UpdateEntity<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> t) where T : class;
        #endregion
        #region 根据条件获取GetList方法
        /// <summary>
        /// 根据条件返回查询的List集合
        /// </summary>
        /// <typeparam name="T">具体的表实体对象</typeparam>
        /// <param name="predicate">查询的条件</param>
        /// <param name="Groupby">分组的列</param>
        /// <param name="Having">GroupBy的Having条件</param>
        /// <param name="Selector">读取的列</param>
        /// <returns></returns>
        List<K> GetList<T,K>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> Groupby, Expression<Func<T, object>> Having, Expression<Func<T, K>> Selector);
        #endregion
        #region 重载不带having条件的筛选
        /// <summary>
        /// 重载不带Having条件List集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="Groupby"></param>
        /// <param name="Selector"></param>
        /// <returns></returns>
        List<K> GetList<T, K>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> Groupby, Expression<Func<T, K>> Selector);
        #endregion
        #region 重载不带having与GroupBy的筛选条件的筛选
        /// <summary>
        /// 输出的实体K集合(一般未DTO)
        /// </summary>
        /// <typeparam name="T">DBModel</typeparam>
        /// <typeparam name="K">输出的实体</typeparam>
        /// <param name="predicate">条件</param>
        /// <param name="Selector">筛选的列</param>
        /// <returns></returns>
        List<K> GetList<T, K>(Expression<Func<T, bool>> predicate, Expression<Func<T, K>> Selector);
        #endregion
        #endregion
    }
}
