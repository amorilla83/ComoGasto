using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Expenses.Core.Entities
{
    public class ProductPurchase
    {
        [Key]
        public int Id { get; set; }

        public ProductDetails ProductDetail { get; set; }
        public int? ProductDetailId { get; set; }

        [Required]
        public Purchase Purchase { get; set;}
        public int PurchaseId { get; set; }

        [Required]
        public Product Product { get; set; }
        public int ProductId { get; set; }

        [Required]
        public double Price { get; set; }
        public int? Quantity { get; set; }
        public double? Weight { get; set; }
    }
}
