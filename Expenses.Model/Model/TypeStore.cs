using System;
using System.Collections.Generic;

namespace Expenses.Model.Model
{
    public partial class TypeStore
    {
        public TypeStore()
        {
            Store = new HashSet<Store>();
        }

        public int IdTypeStore { get; set; }
        public string Name { get; set; }

        public ICollection<Store> Store { get; set; }
    }
}
