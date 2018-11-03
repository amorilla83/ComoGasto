using System;
using System.Collections.Generic;

namespace Expenses.Model.Model
{
    public partial class ExpenseItem
    {
        public int IdExpenseItem { get; set; }
        public int IdExpense { get; set; }
        public int? IdProductBrand { get; set; }
        public int? IdTypeMeasure { get; set; }
        public string Quantity { get; set; }
        public int? Units { get; set; }
        public decimal Price { get; set; }
        public int IdProduct { get; set; }

        public Expense IdExpenseNavigation { get; set; }
        public Product IdProductNavigation { get; set; }
        public TypeMeasure IdTypeMeasureNavigation { get; set; }
    }
}
