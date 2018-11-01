using System;
using System.Collections.Generic;

namespace Expenses.Model.Model
{
    public partial class ShoppingProduct
    {
        public int IdShoppingProduct { get; set; }
        public int? IdProduct { get; set; }
        public decimal? Quantity { get; set; }
        public int? IdTypeMeasure { get; set; }
        public decimal? Price { get; set; }
        public int? IdShopping { get; set; }
        public string Comment { get; set; }
        public int? IdBrand { get; set; }

        public Brand IdBrandNavigation { get; set; }
        public Product IdProductNavigation { get; set; }
        public Shopping IdShoppingNavigation { get; set; }
        public TypeMeasure IdTypeMeasureNavigation { get; set; }
    }
}
