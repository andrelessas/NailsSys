using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Core.Models
{
    public class PaginationResult<TEntity>
    {
        public PaginationResult(int page, int totalPages, int pageSize, int itemsCount, List<TEntity> data)
        {
            Page = page;
            TotalPages = totalPages;
            PageSize = pageSize;
            ItemsCount = itemsCount;
            Data = data;
        }

        public PaginationResult()
        {
            
        }

        public int Page { get; set; }   
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int ItemsCount { get; set; }
        public List<TEntity> Data { get; set; }
    }
}