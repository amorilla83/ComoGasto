using System;
using System.Collections.Generic;

namespace Expenses.Model.Model
{
    public partial class Shopping
    {
        public Shopping()
        {
            ShoppingProduct = new HashSet<ShoppingProduct>();
        }

        public int IdExpense { get; set; }
        public int? IdStoreLocation { get; set; }

        public Expense IdExpenseNavigation { get; set; }
        public StoreLocation IdStoreLocationNavigation { get; set; }
        public ICollection<ShoppingProduct> ShoppingProduct { get; set; }
    }
}
