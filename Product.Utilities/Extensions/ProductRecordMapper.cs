using Product.Types.DTOs;
using Product.Types.Models;

namespace Product.Utilities.Extensions
{
    public static class ProductRecordMapper
    {
        public static ProductRecord Map(this IProductDto product)
        {
            return new ProductRecord
            {
                Id = product.Id,
                Name = product.Name
            };
        }

        public static ProductRecordDto Map(this ProductRecord product)
        {
            return new ProductRecordDto(product);
        }
    }
}
