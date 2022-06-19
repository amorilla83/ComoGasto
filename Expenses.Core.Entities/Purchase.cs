using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Expenses.Core.Entities
{
    public class Purchase
    {
        public Purchase ()
        {
            ProductList = new List<ProductPurchase>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public Store Store { get; set; }
        public int StoreId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public ICollection<ProductPurchase> ProductList { get; set; }

        public double Total { get; set; }
    }
}
