using Product.Types.Models;

namespace Product.Types.DTOs
{
    public class ProductRecordDto : IProductDto
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public ProductRecordDto(ProductRecord product)
        {
            Id = product.Id;
            Name = product.Name;
        }
    }
}
