using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expenses.Core.Entities
{
    public class Purchase
    {
        public Purchase ()
        {
            ProductList = new List<ProductPurchase>();
        }

        [Key]
        public int IdPurchase { get; set; }

        public Store Store { get; set; }
        public int StoreId { get; set; }

        public DateTime Date { get; set; }

        public ICollection<ProductPurchase> ProductList { get; set; }

        public double Total { get; set; }
    }
}
