using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Shared.Paging
{
    public class PagedRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; }
        public SortOrder SortOrder { get; set; } = SortOrder.Ascending;
        public string GetSortByClause(string defaultPropertyName)
        {
            string clause;
            string propertyName = string.IsNullOrWhiteSpace(SortBy) ? defaultPropertyName : SortBy;
            clause = $"{propertyName} {(SortOrder == SortOrder.Ascending ? "asc" : "desc")}";
            return clause;
        }
    }
}
