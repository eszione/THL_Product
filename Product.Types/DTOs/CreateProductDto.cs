using System.ComponentModel.DataAnnotations;

namespace Product.Types.DTOs
{
    public class CreateProductDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
