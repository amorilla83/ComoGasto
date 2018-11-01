using System;
using System.Collections.Generic;

namespace Expenses.Model.Model
{
    public partial class Brand
    {
        public Brand()
        {
            ShoppingProduct = new HashSet<ShoppingProduct>();
        }

        public int IdBrand { get; set; }
        public string Name { get; set; }

        public ICollection<ShoppingProduct> ShoppingProduct { get; set; }
    }
}
