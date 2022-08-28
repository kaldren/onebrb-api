using Microsoft.EntityFrameworkCore;
using Onebrb.Domain.Entities;
using System.Linq.Expressions;

namespace Onebrb.Application.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes)
            where T : BaseEntity
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            return query;
        }
    }

}
