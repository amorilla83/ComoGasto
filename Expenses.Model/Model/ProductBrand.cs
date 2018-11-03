using System;
using System.Collections.Generic;

namespace Expenses.Model.Model
{
    public partial class ProductBrand
    {
        public ProductBrand()
        {
            Favourite = new HashSet<Favourite>();
            HistoPrice = new HashSet<HistoPrice>();
        }

        public int IdProductBrand { get; set; }
        public int IdProduct { get; set; }
        public string Quantity { get; set; }
        public int IdBrand { get; set; }

        public Brand IdBrandNavigation { get; set; }
        public Product IdProductNavigation { get; set; }
        public ICollection<Favourite> Favourite { get; set; }
        public ICollection<HistoPrice> HistoPrice { get; set; }
    }
}
