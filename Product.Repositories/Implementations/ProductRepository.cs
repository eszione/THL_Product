using Product.Repositories.Interfaces;
using Product.Types.Models;
using System.Collections.Generic;
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

        public async Task<IEnumerable<ProductRecord>> ListByNameAsync(string name)
        {
            var products = await ListAsync();

            return products.Where(product => product.Name == name);
        }
    }
}
