using Microsoft.EntityFrameworkCore;
using Product.Repositories.Implementations;
using Product.Types.Constants;
using Product.Types.Models;
using Product.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace Product.Tests.RepositoryTests
{
    public class ProductTestBase
    {
        protected readonly DbContextOptions<ProductContext> Options;

        protected ProductTestBase(DbContextOptions<ProductContext> options)
        {
            Options = options;

            Seed();
        }

        private void Seed()
        {
            using (var context = new ProductContext(Options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var products = JsonReader.GetFromFile<IEnumerable<ProductRecord>>(FolderNames.MOCK, FileNames.PRODUCT_FILE).Result;
                if (products?.Any() ?? false)
                {
                    foreach (var product in products)
                    {
                        context.Products.Add(product);
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
