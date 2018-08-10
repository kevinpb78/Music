using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Music.Utilities
{
    public class PagedList<T> : List<T>
    {
        public int PageId { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public List<T> Artists { get; }

        public PagedList(IQueryable<T> source, int pageId, int pageSize)
        {
            TotalItems = source.Count();
            PageId = pageId;
            PageSize = pageSize;
            Artists = source.Skip(pageSize * (pageId - 1))
                         .Take(pageSize)
                         .ToList();
        }

        public int PageCount =>
              (int)Math.Ceiling(this.TotalItems / (double)this.PageSize);
        

        public PagedHeader GetHeader()
        {
            return new PagedHeader(TotalItems, PageId, PageSize, PageCount);
        }

    }
}
