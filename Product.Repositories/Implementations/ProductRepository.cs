using Product.Repositories.Interfaces;
using Product.Types.Models;
using Product.Utilities.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Repositories.Implementations
{
    public class ProductRepository : Repository<int, ProductRecord, ProductContext>, IProductRepository
    {
        public ProductRepository(ProductContext context) : base(context)
        {
        }

        public async Task<ProductRecord> GetByIdAsync(int id)
        {
            return await GetAsync(id);
        }

        public async Task<PagedResults<ProductRecord>> ListByNameAsync(string name, int page, int pageSize)
        {
            var products = await ListAsync();

            return products.Where(product => product.Name == name).Paginate(page, pageSize);
        }
    }
}
