using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Shared.Paging.Generic
{
    public static class PagedResultExtensions
    {
        public static PagedResult<T> ToPagedResult<T>(this IOrderedQueryable<T> query, int PageNumber, int PageSize)
        {
            PagedResult<T> result = new PagedResult<T>();
            result.PageNumber = PageNumber;
            result.PageSize = PageSize;
            if (query != null)
            {
                result.TotalCount = query.Count();
                result.Result = query.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
            }
            return result;
        }
        public static PagedResult<T> ToPagedResult<T>(this IOrderedQueryable<T> query, PagedRequest request)
        {
            return ToPagedResult(query, request.PageNumber, request.PageSize);
        }
    }
}
