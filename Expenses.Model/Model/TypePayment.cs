using System;
using System.Collections.Generic;

namespace Expenses.Model.Model
{
    public partial class TypePayment
    {
        public TypePayment()
        {
            Expense = new HashSet<Expense>();
        }

        public int IdTypePayment { get; set; }
        public string Description { get; set; }

        public ICollection<Expense> Expense { get; set; }
    }
}
