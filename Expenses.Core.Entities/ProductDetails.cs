using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Expenses.Core.Entities
{
    public class ProductDetails
    {
        public ProductDetails()
        {
            ProductPurchaseList = new List<ProductPurchase>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Brand Brand { get; set; }
        public int? BrandId { get; set; }
        public Format Format { get; set; }
        public int? FormatId { get; set; }
        [DefaultValue(0)]
        public double LastPrice { get; set; }

        public ICollection<ProductPurchase> ProductPurchaseList { get; set; }
    }
}
