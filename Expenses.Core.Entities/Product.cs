using System.Collections.Generic;

namespace Expenses.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Image { get; set; }
        public List<ProductBrand> ProductBrands { get; set; }

        public Product ()
        {
            ProductBrands = new List<ProductBrand>();
        }
    }
}
