using System;
using System.Collections.Generic;

namespace Expenses.Model.Model
{
    public partial class StoreLocation
    {
        public StoreLocation()
        {
            Shopping = new HashSet<Shopping>();
        }

        public int IdStoreLocation { get; set; }
        public int? IdStore { get; set; }
        public string Location { get; set; }

        public Store IdStoreNavigation { get; set; }
        public ICollection<Shopping> Shopping { get; set; }
    }
}
