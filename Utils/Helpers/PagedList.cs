using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utils.Helpers
{
    public class PagedList<T> : List<T>
    {
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            AddRange(items);
        }

        public int CurrentPage { get; private set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get => (int)Math.Ceiling(TotalCount / (double)PageSize); }
        public bool HasPrevious { get => CurrentPage > 1; }
        public bool HasNext { get => CurrentPage < TotalPages; }
       
    }
}
