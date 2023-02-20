using System.Collections.Generic;

namespace Entities.Shared.Paging
{
    public class PagedRequestBase
    {
        public static int MaxPageSize = 300;
        public static int MinPageSize = 1;
        private int _pageSize = MinPageSize;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value < MinPageSize ? MinPageSize : (value > MaxPageSize ? MaxPageSize : value);
            }
        }
        private int _pageNumber = 1;
        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                _pageNumber = value < 1 ? 10 : value;
            }
        }

        public List<SortByClause> SortByClauses { get; set; } = new List<SortByClause>();
    }
}
