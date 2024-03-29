﻿using System.ComponentModel.DataAnnotations;

namespace Product.Types.Models
{
    public class ProductRecord
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
