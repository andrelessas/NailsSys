using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NailsSys.Core.Models;

namespace NailsSys.Infrastructure.Persistense.Extensions
{
    public static class Extensions
    {
        public static async Task<PaginationResult<TEntity>> GetPagination<TEntity>(
            this IQueryable<TEntity> query,
            int page,
            int pageSize) where TEntity : class
        {
            var result = new PaginationResult<TEntity>();

            result.Page = page;
            result.PageSize = pageSize;
            result.ItemsCount = await query.CountAsync();

            var pageCount = (double)result.ItemsCount / pageSize;
            result.TotalPages = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;

            result.Data = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }
    }
}