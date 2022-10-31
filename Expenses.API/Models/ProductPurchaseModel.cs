using Expenses.Core.Entities;

namespace Expenses.API.Models
{
    public class ProductPurchaseModel
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; }
        public PurchaseModel Purchase { get; set; }
        public ProductDetailsModel ProductDetail { get; set; }
        public ProductModel Product { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double? Weight { get; set; }
        public string Details { get; set; }
    }
}
