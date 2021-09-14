using Product.Types.Constants;
using Product.Types.Models;
using System.Collections.Generic;
using System.Linq;

namespace Product.Utilities.Extensions
{
    public static class Paginator
    {
        public static PagedResults<T> Paginate<T>(this IEnumerable<T> list, int page, int pageSize)
        {
            page = page < 0 ? Integers.MIN_PAGE_NUMBER : page;
            pageSize = pageSize < Integers.MIN_PAGE_SIZE ? Integers.MIN_PAGE_SIZE : pageSize;

            var totalRecords = list.Count();
            var totalPages = totalRecords / pageSize + (totalRecords % pageSize > 0 ? 1 : 0);
            var resultsToSkip = (page - 1) * pageSize;

            if (resultsToSkip >= totalRecords)
            {
                page = Integers.MIN_PAGE_NUMBER;
                pageSize = totalRecords;
                resultsToSkip = 0;
                totalPages = Integers.MIN_PAGE_NUMBER;
            }

            return new PagedResults<T>
            {
                Page = page,
                PageSize = pageSize,
                Results = list.Skip(resultsToSkip).Take(pageSize),
                TotalPages = totalPages
            };
        }
    }
}
