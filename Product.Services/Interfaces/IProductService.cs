using Product.Types.Models;
using System.Threading.Tasks;

namespace Product.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductRecord> GetByIdAsync(int id);
        Task<PagedResults<ProductRecord>> ListByNameAsync(string name, int page, int pageSize);
    }
}
