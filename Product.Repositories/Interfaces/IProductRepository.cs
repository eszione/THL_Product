using Product.Repositories.Implementations;
using Product.Types.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<int, ProductRecord, ProductContext>
    {
        Task<ProductRecord> GetByIdAsync(int id);
        Task<IEnumerable<ProductRecord>> ListByNameAsync(string name);
    }
}
