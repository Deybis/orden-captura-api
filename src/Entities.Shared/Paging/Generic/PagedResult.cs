using System.Collections.Generic;

namespace Entities.Shared.Paging.Generic
{
    public class PagedResult<T> : PagedResultBase
    {
        public IEnumerable<T> Result { get; set; }
        public IEnumerable<T> data { get; set; }
        public IEnumerable<T> Entity { get; set; }
        public PagedResult(int pageNumber, int pageSize, int totalCount) : base(pageNumber, pageSize, totalCount)
        {
        }
        public PagedResult()
        {

        }
    }
}
