using System.Collections.Generic;
using System.Linq;

namespace Product.Types.Models
{
    public class PagedResults<T>
    {
        public IEnumerable<T> Results { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults => Results.Count();
    }
}
