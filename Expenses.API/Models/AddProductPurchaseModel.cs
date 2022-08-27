using System;
namespace Expenses.API.Models
{
    public class AddProductPurchaseModel
    {
        public int? Id { get; set; }
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public AddProductDetailsModel ProductDetail { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double? Weight { get; set; }
        public string Details { get; set; }
    }
}
