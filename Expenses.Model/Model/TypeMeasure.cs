using System;
using System.Collections.Generic;

namespace Expenses.Model.Model
{
    public partial class TypeMeasure
    {
        public TypeMeasure()
        {
            ExpenseItem = new HashSet<ExpenseItem>();
        }

        public int IdTypeMeasure { get; set; }
        public string Description { get; set; }

        public ICollection<ExpenseItem> ExpenseItem { get; set; }
    }
}
