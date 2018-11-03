using System;
using System.Collections.Generic;

namespace Expenses.Model.Model
{
    public partial class Brand
    {
        public Brand()
        {
            ProductBrand = new HashSet<ProductBrand>();
        }

        public int IdBrand { get; set; }
        public string Name { get; set; }

        public ICollection<ProductBrand> ProductBrand { get; set; }
    }
}
