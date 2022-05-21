using System;
using Expenses.API.Models.Stores;
using Expenses.Core.Entities;

namespace Expenses.API.Models
{
    public class AddPurchaseModel
    {
        public int? IdPurchase { get; set; }
        public DateTime Date { get; set; }
        public StoreModel Store { get; set; }
        public double Total { get; set; }
    }
}
