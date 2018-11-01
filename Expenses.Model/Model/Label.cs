using System;
using System.Collections.Generic;

namespace Expenses.Model.Model
{
    public partial class Label
    {
        public Label()
        {
            Product = new HashSet<Product>();
        }

        public int IdLabel { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? IdSection { get; set; }

        public Section IdSectionNavigation { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
