using System;
using System.Collections.Generic;
using System.Linq;

namespace BandsApi.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool HasPrevious => (CurrentPage > 1);
        public bool hasNext => (CurrentPage < TotalPages);

        public PagedList(List<T> items, int totalCount, int currentPage, int pageSize)
        {
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            CurrentPage = currentPage;
            PageSize = pageSize;
            AddRange(items);
        }

        public static PagedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
        {
            int count = source.Count();
            List<T> items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
