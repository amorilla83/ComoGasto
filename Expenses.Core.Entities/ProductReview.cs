using System;
using System.Collections.Generic;
using Expenses.Core.Entities;

namespace Expenses.Core.Entities
{
    public class ProductReview
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
        public List<ProductPurchase> PurchaseList { get; set; }
        public List<int?> FormatsList { get; set; }
        public List<int?> BrandsList { get; set; }
        public DateTime? LastDate { get; set; }
        public double? LastPrice { get; set; }
    }
}

