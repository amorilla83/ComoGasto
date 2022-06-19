using System;
namespace Expenses.API.Models
{
	public class ProductDetailsModel
	{
        public int Id { get; set; }
        public ProductModel Product { get; set; }
        public ItemModel Brand { get; set; }
        public ItemModel Format { get; set; }
        public double LastPrice { get; set; }
    }
}

