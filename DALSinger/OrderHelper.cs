using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DALSinger
{
    public static class OrderHelper
    {
        public static bool PropertyExists<T>(string propertyName)
        {
            return typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                BindingFlags.Public | BindingFlags.Instance) != null;
        }

        public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string propertyName, bool isDesc)
        {
            if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
            BindingFlags.Public | BindingFlags.Instance) == null)
            {
                return null;
            }

            ParameterExpression paramterExpression = Expression.Parameter(typeof(T));
            Expression orderByProperty = Expression.Property(paramterExpression, propertyName);
            LambdaExpression lambda = Expression.Lambda(orderByProperty, paramterExpression);

            string typeSort = isDesc ? "OrderByDescending" : "OrderBy";

            var OrderByMethod = typeof(Queryable).GetMethods().Single(method => method.Name == typeSort && method.GetParameters().Length == 2);

            MethodInfo genericMethod = OrderByMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
            object ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<T>)ret;
        }
    }
}
