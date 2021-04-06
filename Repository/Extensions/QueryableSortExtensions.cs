using Microsoft.EntityFrameworkCore;
using Models.Enums;
using Models.Shared;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class QueryableSortExtensions
    {
        public static async Task<PagedResult<TItem>> ToPagedResultAsync<TItem, TSortField>(this IQueryable<TItem> items, GetListQuery query, Expression<Func<TItem, TSortField>> sort = null, OrderDirections direction = OrderDirections.Asc) where TItem : class
        {
            var result = new PagedResult<TItem> { Count = await items.CountAsync() };

            if (result.Count == 0)
            {
                result.Items = new TItem[] { };
                return result;
            }

            if (sort != null)
            {
                items = direction == OrderDirections.Desc
                    ? items.OrderByDescending(sort)
                    : items.OrderBy(sort);
            }

            result.Items = await items.Skip(query.Offset).Take(query.Limit).ToListAsync();

            return result;
        }

        public static async Task<PagedResult<TItem>> ToPagedResultAsync<TItem>(this IQueryable<TItem> items, GetListQuery query) where TItem : class
        {
            return await items.ToPagedResultAsync(query, (Expression<Func<TItem, object>>)null);
        }
    }
}
