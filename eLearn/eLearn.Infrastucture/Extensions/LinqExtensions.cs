using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Infrastucture.Extensions
{
    [ExcludeFromCodeCoverage]
    static class Traverse
    {
        public static IEnumerable<T> Across<T>(T first, Func<T, T> next)
            where T : class
        {
            var item = first;
            while (item != null)
            {
                yield return item;
                item = next(item);
            }
        }
    }

    [ExcludeFromCodeCoverage]
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }

            return items;
        }

        public static string Join<T>(this IEnumerable<T> items, Func<T, string> selector)
        {
            return items.Join(selector, string.Empty);
        }

        public static string Join<T>(this IEnumerable<T> items, Func<T, string> selector, string separator)
        {
            return string.Join(separator, items.Select(selector));
        }

        public static IEnumerable<T> Except<T, TKey>(this IEnumerable<T> items, IEnumerable<T> other,
                                                                            Func<T, TKey> getKey)
        {
            return from item in items
                   join otherItem in other on getKey(item)
                   equals getKey(otherItem) into tempItems
                   from temp in tempItems.DefaultIfEmpty()
                   where ReferenceEquals(null, temp) || temp.Equals(default(T))
                   select item;

        }

    }

    [ExcludeFromCodeCoverage]
    public static class LinqExpressions
    {
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string memberName)
        {

            ParameterExpression arg = Expression.Parameter(typeof(T), "x");
            Expression expr = arg;

            foreach (string prop in memberName.Split('.'))
            {
                // use reflection (not ComponentModel) to mirror LINQ
                expr = Expression.PropertyOrField(expr, prop);
            }

            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), expr.Type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            return (IOrderedQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    "OrderBy",
                    new Type[] { typeof(T), expr.Type },
                    query.Expression,
                    lambda)
            );
        }

        [ExcludeFromCodeCoverage]
        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string memberName)
        {
            ParameterExpression arg = Expression.Parameter(typeof(T), "x");
            Expression expr = arg;

            foreach (string prop in memberName.Split('.'))
            {
                // use reflection (not ComponentModel) to mirror LINQ
                expr = Expression.PropertyOrField(expr, prop);
            }

            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), expr.Type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            return (IOrderedQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    "OrderByDescending",
                    new Type[] { typeof(T), expr.Type },
                    query.Expression,
                    lambda)
            );
        }
    }

    [ExcludeFromCodeCoverage]
    public static class IDictionaryExtensions
    {
        public static T GetValueOrDefault<T>(this IDictionary<string, object> dictionary, string key)
        {
            object value;

            if (dictionary.TryGetValue(key, out value))
            {
                return (T)value;
            }
            else
            {
                return default(T);
            }
        }

    }

    [ExcludeFromCodeCoverage]
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
        /*
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                           Expression<Func<T, bool>> expr2)
        {
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, expr2.Body), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, expr2.Body), expr1.Parameters);
        }
        */
    }
}
