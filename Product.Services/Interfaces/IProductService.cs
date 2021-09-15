using Product.Types.Enums;
using Product.Types.Models;
using System.Threading.Tasks;

namespace Product.Services.Interfaces
{
    public interface IProductService
    {
        Task<(ProductRecord, ProductCreationResult)> CreateProduct(ProductRecord product);
        Task<ProductRecord> GetByIdAsync(int id);
        Task<PagedResults<ProductRecord>> ListByNameAsync(string name, int page, int pageSize);
    }
}
