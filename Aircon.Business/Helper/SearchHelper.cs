using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Aircon.Business.Helper
{
    public class SearchHelpler
    {
        public static IQueryable<T> GetBasicSearchResults<T>(IQueryable<T> query, Dictionary<string, object> properties)
        {
            var qry = query;
            var type = typeof(T);
            List<Expression> expressions = new List<Expression>();
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression condition = null;
            foreach (var key in properties.Keys)
            {
                var value = properties[key];
                var property = type.GetProperty(key);
                if (value != null)
                {
                    if (property != null && property.PropertyType == typeof(int))
                    {
                        MemberExpression mn = Expression.MakeMemberAccess(parameter, property);
                        
                        MethodInfo min = typeof(int).GetMethod("Equals", new Type[] { typeof(int) });
                        if (value != null)
                        {
                            if (value is int)
                            {
                                ConstantExpression cn = Expression.Constant(value, typeof(int));

                                int result = (int)value;
                                if (result > 0)
                                {
                                    Expression calln = Expression.Call(mn, min, cn);
                                    if (condition == null)
                                        condition = calln;
                                    else
                                        condition = Expression.OrElse(condition, calln);
                                }
                            }
                        }
                    }
                    else
                    {
                        MemberExpression m = Expression.MakeMemberAccess(parameter, property);
                        ConstantExpression c = Expression.Constant(value, typeof(string));
                        MethodInfo mi = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });

                        Expression call = Expression.Call(m, mi, c);
                        if (condition == null)
                            condition = call;
                        else
                            condition = Expression.OrElse(condition, call);
                    }
                }
            }
            if (condition != null)
            {
                Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(condition, parameter);
                qry = qry.Where(lambda);
            }
            return qry;
        }

    }
}
