﻿using System;

namespace Entities.Shared.Paging
{
    public class PagedResultBase
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (decimal)PageSize);
        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;

        public PagedResultBase(int pageNumber, int pageSize, int totalCount)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
        public PagedResultBase()
        {

        }
    }
}
