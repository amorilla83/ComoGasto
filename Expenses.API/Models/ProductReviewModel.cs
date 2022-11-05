using System;
namespace Expenses.API.Models
{
    public class ProductReviewModel
    {
        public int Id {get; set;}
        public string Name { get; set; }
        public int PurchaseCount { get; set; }
        public int FormatsCount { get; set; }
        public int BrandsCount { get; set; }
        public DateTime LastDate { get; set; }
        public double LastPrice { get; set; }
    }
}

