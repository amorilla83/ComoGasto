using System;
using System.Collections.Generic;

namespace Expenses.Model.Model
{
    public partial class Store
    {
        public Store()
        {
            Expense = new HashSet<Expense>();
            Favourite = new HashSet<Favourite>();
            HistoPrice = new HashSet<HistoPrice>();
        }

        public int IdStore { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public int? IdTypeStore { get; set; }

        public TypeStore IdTypeStoreNavigation { get; set; }
        public ICollection<Expense> Expense { get; set; }
        public ICollection<Favourite> Favourite { get; set; }
        public ICollection<HistoPrice> HistoPrice { get; set; }
    }
}
