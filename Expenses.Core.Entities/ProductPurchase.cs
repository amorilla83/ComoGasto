using System;
namespace Expenses.Core.Entities
{
    public class ProductPurchase
    {
        public int ProductPurchaseId { get; set; }

        public Purchase Purchase { get; set;}
        public int PurchaseId { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }

        public Brand Brand { get; set; }
        public int? BrandId { get; set; }

        public Format Format { get; set; }
        public int? FormatId { get; set; }

        public double Price { get; set; }
        public int? Quantity { get; set; }
        public double? Weight { get; set; }
    }
}
