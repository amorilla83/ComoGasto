using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Expenses.Core.Entities
{
    public class Brand
    {
        public Brand ()
        {
            ProductList = new List<Product>();
            FormatList = new List<Format>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Product> ProductList { get; set; }
        public ICollection<Format> FormatList { get; set; }
    }
}
