using System.ComponentModel.DataAnnotations;

namespace Product.Types.DTOs
{
    public class CreateProductDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The id must be greater than 0")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
