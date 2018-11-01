using System;
using System.Collections.Generic;

namespace Expenses.Model.Model
{
    public partial class Product
    {
        public Product()
        {
            ShoppingProduct = new HashSet<ShoppingProduct>();
        }

        public int IdProduct { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public int? IdLabel { get; set; }
        public string Image { get; set; }

        public Label IdLabelNavigation { get; set; }
        public ICollection<ShoppingProduct> ShoppingProduct { get; set; }
    }
}
