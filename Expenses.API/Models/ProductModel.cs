using System.Collections.Generic;

namespace Expenses.API.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductDetailsModel> ProductDetails { get; set; }
    }
}
