using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Expenses.Core.Entities
{
    public class Product
    {
        public Product ()
        {
            ProductDetails = new List<ProductDetails>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public List<ProductDetails> ProductDetails { get; set; }
    }
}
