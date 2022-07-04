using System;
using System.Collections.Generic;
using Expenses.Core.Entities;

namespace Expenses.API.Models
{
    public class PurchaseModel
    {
        public int IdPurchase {get; set;}
        public DateTime Date { get; set; }
        public string DateString { get; set; }
        public Stores.StoreModel Store { get; set; }
        public List<ProductPurchaseModel> ProductList { get; set; }
        public int Count { get; set; }
        public double Total { get; set; }
    }
}
