using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using DapperModelCore.DBModel;
using Kogel.Dapper.Extension;
using Kogel.Dapper.Extension.Core.SetQ;
using Kogel.Dapper.Extension.Model;
using Kogel.Dapper.Extension.MySql;
using MySql.Data.MySqlClient;

namespace DapperModelCore.DapperCore.MySql
{
    /// <summary>
    /// 数据库操作
    /// </summary>
    public class MySqlRepository : IRepository
    {
        public static MySqlConnection MySqlConnection()
        {
            string mysqlConnectionString = ConnectionString;
            var connection = new MySqlConnection(mysqlConnectionString);
            connection.Open();
            return connection;
        }

        public static string ConnectionString
        {
            get { return "Database=preorder_dev;Data Source=rm-wz9li05w3782te7e72o.mysql.rds.aliyuncs.com;User Id=dev_preorder;Password=dev0501#preorder;CharSet=utf8;port=3306;"; }
        }
        #region 实体分页操作
        /// <summary>
        /// 实体分页操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">条件</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页几条数据</param>
        /// <param name="dic">排序的集合:(前端开发时,请注意1为升序,2为降序)</param>
        /// <returns></returns>
        public PageList<T> GetConditionPageList<T>(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize, Dictionary<int, Expression<Func<T, object>>> dic)
        {
            using (var conn = MySqlConnection())
            {
                var result = conn.QuerySet<T>().Where(predicate);
                if (dic != null && dic.Count > 0)
                {
                    Order<T> orderresult = null;
                    foreach (var item in dic)
                    {
                        if (item.Key == 1)
                        {
                            orderresult = result.OrderBy<T>(item.Value);
                        }
                        else
                        {
                            orderresult = result.OrderByDescing<T>(item.Value);
                        }
                    }
                    return orderresult.PageList<T>(pageIndex, pageSize);
                }
                var res = result.PageList<T>(pageIndex, pageSize);
                return res;
            }
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="dic"></param>
        /// <param name="Selector"></param>
        /// <returns></returns>
        public PageList<K> GetConditionPageList<T, K>(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize, Dictionary<int, Expression<Func<T, object>>> dic, Expression<Func<T, K>> Selector)
        {
            using (var conn = MySqlConnection())
            {
                var result = conn.QuerySet<T>().Where(predicate);
                if (dic != null && dic.Count > 0)
                {
                    Order<T> orderresult = null;
                    foreach (var item in dic)
                    {
                        if (item.Key == 1)
                        {
                            orderresult = result.OrderBy<T>(item.Value);
                        }
                        else
                        {
                            orderresult = result.OrderByDescing<T>(item.Value);
                        }
                    }
                    return orderresult.PageList<K>(pageIndex, pageSize, Selector);
                }
                var res = result.PageList<K>(pageIndex, pageSize, Selector);
                return res;
            }
        }


        #region 根据条件获取单个表数据的集合
        /// <summary>
        /// 根据条件获取单个表数据的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public List<T> GetEntitiesListByConditions<T>(Expression<Func<T, bool>> predicate, Dictionary<int, Expression<Func<T, object>>> dic)
        {
            using (var conn = MySqlConnection())
            {
                var result = conn.QuerySet<T>().Where(predicate);
                if (dic != null && dic.Count > 0)
                {
                    foreach (var item in dic)
                    {
                        if (item.Key == 1)
                        {
                            result.OrderBy<T>(item.Value);
                        }
                        else
                        {
                            result.OrderByDescing<T>(item.Value);
                        }
                    }
                }
                var res = result.ToList<T>();
                return res;
            }
        }
        #endregion
        #region 根据条件获取单个的实体
        /// <summary>
        /// 返回查询的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">动态Lamada表达式</param>
        /// <returns></returns>
        public T FindEntity<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            using (var conn = MySqlConnection())
            {
                var result = conn.QuerySet<T>().Where(predicate).Get();
                return result;
            }
        }
        #endregion
        #region 插入单个的数据库实体
        /// <summary>
        /// 插入实体到数据库
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">DBModel</param>
        /// <returns></returns>
        public int Insert<T>(T t) where T : class
        {
            using (var conn = MySqlConnection())
            {
                var result = conn.CommandSet<T>().Insert(t);
                return result;
            }
        }
        #endregion
        #region 插入单个的数据库实体成功后返回最新的自增ID
        /// <summary>
        /// 插入实体到数据库
        /// </summary>
        /// <typeparam name="T">数据库实体</typeparam>
        /// <param name="t">DBModel</param>
        /// <returns>返回最新的ID</returns>
        public int InsertIdentity<T>(T t) where T : class
        {
            using (var conn = MySqlConnection())
            {
                var result = conn.CommandSet<T>().InsertIdentity(t);
                return result;
            }
        }
        #endregion
        #region 批量新增数据库实体
        /// <summary>
        /// 批量新增数据库实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="EntityList">传过来List<Model></param>
        /// <returns></returns>
        public int InsertEntityList<T>(List<T> EntityList) where T : class
        {
            using (var conn = MySqlConnection())
            {
                var result = conn.CommandSet<T>().Insert(EntityList);
                return result;
            }
        }
        #endregion
        #region 根据条件删除数据库表中的数据
        /// <summary>
        /// 批量新增数据库实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="EntityList">传过来List<Model></param>
        /// <returns></returns>
        public int DeleteEntity<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            using (var conn = MySqlConnection())
            {
                var result = conn.CommandSet<T>().Where(predicate).Delete();
                return result;
            }
        }
        #endregion
        #region 根据传入的条件修改表中的数据
        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">传入的表达式</param>
        /// <param name="t">表中的实体</param>
        /// <returns></returns>
        public int UpdateEntity<T>(Expression<Func<T, bool>> predicate, Expression<Func<T,T>>  t) where T : class
        {
            using (var conn = MySqlConnection())
            {
                var result = conn.CommandSet<T>().Where(predicate).Update(t);
                return result;
            }
        }
        #endregion
        #region 获取表的实体对象集合列表
        /// <summary>
        /// 获取表的实体对象集合列表
        /// </summary>
        /// <typeparam name="T">DBmodel对象</typeparam>
        /// <typeparam name="K">返回实体</typeparam>
        /// <param name="predicate"></param>
        /// <param name="Groupby">分组的键值</param>
        /// <param name="Having">条件</param>
        /// <param name="Selector">要筛选的列</param>
        /// <returns>获取表的实体对象集合列表</returns>
        public List<K> GetList<T,K>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> Groupby, Expression<Func<T, object>> Having, Expression<Func<T, K>> Selector)
        {
            using (var conn = MySqlConnection())
            {
                var result = conn.QuerySet<T>()
                             .Where(predicate);
                if (Groupby != null)
                {
                    result = result.GroupBy(Groupby);
                }
                if (Having != null)
                {
                    result = result.Having(Having);
                }
                var res = result.ToList(Selector);
                return res;
            }
        }
        #endregion
        #region 重载不带Having条件List集合
        /// <summary>
        /// 不带Having条件的重载
        /// </summary>
        /// <typeparam name="T">DBModel</typeparam>
        /// <typeparam name="K">输出的实体</typeparam>
        /// <param name="predicate">条件</param>
        /// <param name="Groupby">分组的键值</param>
        /// <param name="Selector">筛选的列</param>
        /// <returns></returns>
        public List<K> GetList<T, K>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> Groupby, Expression<Func<T, K>> Selector)
        {
            return GetList<T, K>(predicate, Groupby, null, Selector);
        }
        #endregion
        #region 重载不带Having,Group条件List集合
        /// <summary>
        /// 重载不带Having,Group条件List集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="Selector">输出指定的列</param>
        /// <returns></returns>
        public List<K> GetList<T, K>(Expression<Func<T, bool>> predicate, Expression<Func<T, K>> Selector)
        {
            return GetList<T, K>(predicate, null, null, Selector);
        }
        #endregion

    }
}
