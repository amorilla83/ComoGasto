using System;
namespace Expenses.API.Models
{
    public class AddProductPurchaseModel
    {
        public int ProductId { get; set; }
        public int? BrandId { get; set; }
        public int? FormatId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double? Weight { get; set; }
    }
}
