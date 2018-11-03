using System;
using System.Collections.Generic;

namespace Expenses.Model.Model
{
    public partial class Expense
    {
        public Expense()
        {
            ExpenseItem = new HashSet<ExpenseItem>();
        }

        public int IdExpense { get; set; }
        public string Concept { get; set; }
        public string Details { get; set; }
        public decimal? Price { get; set; }
        public int? IdTypePayment { get; set; }
        public DateTime? Date { get; set; }
        public int? IdStore { get; set; }
        public int? IdSection { get; set; }

        public Section IdSectionNavigation { get; set; }
        public Store IdStoreNavigation { get; set; }
        public TypePayment IdTypePaymentNavigation { get; set; }
        public ICollection<ExpenseItem> ExpenseItem { get; set; }
    }
}
