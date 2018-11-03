using System;
using System.Collections.Generic;

namespace Expenses.Model.Model
{
    public partial class Section
    {
        public Section()
        {
            Expense = new HashSet<Expense>();
            Label = new HashSet<Label>();
        }

        public int IdSection { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Expense> Expense { get; set; }
        public ICollection<Label> Label { get; set; }
    }
}
