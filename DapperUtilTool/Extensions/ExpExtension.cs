using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
using System.Linq;
using DapperUtilTool.Common;

namespace DapperUtilTool.Extensions
{
    /// <summary>
    /// 扩展类
    /// </summary>
    public class ExpExtension
    {
        /// <summary>
        /// 动态生成Lamada表达式
        /// </summary>
        /// <typeparam name="T">源数据类型</typeparam>
        /// <typeparam name="TReturn">目标类型</typeparam>
        /// <returns>返回表达式树</returns>
        public static Expression<Func<T, TReturn>> GetSelector<T, TReturn>()
        {
            Expression<Func<T, TReturn>> Selector = null;
            ParameterExpression left = Expression.Parameter(typeof(T), "m");
            var properties = typeof(TReturn).GetProperties().Where(m => m.GetCustomAttributes(typeof(IgnoreAttribute)).Count() == 0);
            Dictionary<string, string> dic = properties.ToDictionary(m => m.Name, y => y.Name);
            List<MemberBinding> bindList = new List<MemberBinding>();
            var v0 = Expression.New(typeof(TReturn));
            foreach (var item in dic)
            {
                MemberInfo speciesMember = typeof(TReturn).GetMember(item.Key)[0];
                MemberExpression mem = Expression.Property(left, item.Key);
                MemberBinding memberBinding = Expression.Bind(speciesMember, mem);
                bindList.Add(memberBinding);
            }
            if (bindList != null && bindList.Count > 0)
            {
                var body = Expression.MemberInit(v0, bindList);
                Selector = (Expression<Func<T, TReturn>>)Expression.Lambda(body, left);
            }
            return Selector;
        }
    }
}
