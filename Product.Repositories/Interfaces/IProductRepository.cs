﻿using Product.Types.Models;
using System.Threading.Tasks;

namespace Product.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductRecord> CreateProduct(ProductRecord product);
        Task<ProductRecord> GetByIdAsync(int id);
        Task<PagedResults<ProductRecord>> ListByNameAsync(string name, int page, int pageSize);
        Task UpdateProduct(ProductRecord product);
    }
}
