namespace Expenses.API.Models
{
    public class ProductPurchaseModel
    {
        public int ProductId { get; set; }

        public ProductModel Product { get; set; }

        public Brands.BrandModel Brand { get; set; }

        public FormatModel Format { get; set; }

        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Weight { get; set; }
    }
}
