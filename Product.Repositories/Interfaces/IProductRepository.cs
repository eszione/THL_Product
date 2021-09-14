using Product.Repositories.Implementations;
using Product.Types.Models;
using System.Threading.Tasks;

namespace Product.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<int, ProductRecord, ProductContext>
    {
        Task<ProductRecord> GetByIdAsync(int id);
        Task<PagedResults<ProductRecord>> ListByNameAsync(string name, int page, int pageSize);
    }
}
