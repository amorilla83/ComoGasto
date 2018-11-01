using System;
using System.Collections.Generic;

namespace Expenses.Model.Model
{
    public partial class Expense
    {
        public int IdExpense { get; set; }
        public string Concept { get; set; }
        public string Details { get; set; }
        public decimal? Price { get; set; }
        public int? IdTypePayment { get; set; }
        public DateTime? Date { get; set; }

        public TypePayment IdTypePaymentNavigation { get; set; }
        public Shopping Shopping { get; set; }
    }
}
