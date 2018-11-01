using System;
using System.Collections.Generic;

namespace Expenses.Model.Model
{
    public partial class Store
    {
        public Store()
        {
            StoreLocation = new HashSet<StoreLocation>();
        }

        public int IdStore { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public int? IdTypeStore { get; set; }

        public TypeStore IdTypeStoreNavigation { get; set; }
        public ICollection<StoreLocation> StoreLocation { get; set; }
    }
}
