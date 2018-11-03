using System;
using System.Collections.Generic;

namespace Expenses.Model.Model
{
    public partial class Product
    {
        public Product()
        {
            ExpenseItem = new HashSet<ExpenseItem>();
            ProductBrand = new HashSet<ProductBrand>();
        }

        public int IdProduct { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public int? IdLabel { get; set; }
        public string Image { get; set; }

        public Label IdLabelNavigation { get; set; }
        public ICollection<ExpenseItem> ExpenseItem { get; set; }
        public ICollection<ProductBrand> ProductBrand { get; set; }
    }
}
