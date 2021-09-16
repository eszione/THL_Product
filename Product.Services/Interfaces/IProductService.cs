using Product.Types.DTOs;
using Product.Types.Enums;
using Product.Types.Models;
using System.Threading.Tasks;

namespace Product.Services.Interfaces
{
    public interface IProductService
    {
        Task<(ProductRecordDto, ProductCommandResult)> CreateProduct(ProductRecord product);
        Task<ProductRecordDto> GetByIdAsync(int id);
        Task<PagedResults<ProductRecordDto>> ListByNameAsync(string name, int page, int pageSize);
        Task<(ProductRecordDto, ProductCommandResult)> UpdateProduct(ProductRecord product);
    }
}
