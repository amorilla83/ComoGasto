using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Expenses.Core.Entities
{
    public class Product
    {
        public Product ()
        {
            BrandList = new List<Brand>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Brand> BrandList { get; set; }
        //public string Detail { get; set; }
        //public string Image { get; set; }
        //public List<ProductBrand> ProductBrands { get; set; }

        //public Product ()
        //{
          //  ProductBrands = new List<ProductBrand>();
        //}
    }
}
