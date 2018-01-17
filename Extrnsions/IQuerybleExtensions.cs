using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace vega.Extrnsions
{
    public static class IQuerybleExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObj, Dictionary<string, Expression<Func<T, object>>> mapping)
        {
            if (String.IsNullOrWhiteSpace(queryObj.SortBy) || !mapping.ContainsKey(queryObj.SortBy))
                return query;

            if (queryObj.isSortAscending) {
                return query.OrderBy(mapping[queryObj.SortBy]);
            } else {
                return query.OrderByDescending(mapping[queryObj.SortBy]);
            }
        }

        public static IQueryable<T> ApplyPageing<T>(this IQueryable<T> query, IQueryObject queryObj)
        {
            if (queryObj.Page <= 0)
                queryObj.Page = 1;
            if (queryObj.PageSize <= 0)
                queryObj.PageSize = 10;
            return query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);
        }
    }
}