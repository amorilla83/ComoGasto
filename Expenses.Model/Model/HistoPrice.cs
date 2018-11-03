using System;
using System.Collections.Generic;

namespace Expenses.Model.Model
{
    public partial class HistoPrice
    {
        public int IdHistoPrice { get; set; }
        public int IdProductBrand { get; set; }
        public decimal Price { get; set; }
        public int? IdStore { get; set; }
        public DateTime Date { get; set; }

        public ProductBrand IdProductBrandNavigation { get; set; }
        public Store IdStoreNavigation { get; set; }
    }
}
