using System;
using System.Collections.Generic;

namespace Expenses.Model.Model
{
    public partial class TypeMeasure
    {
        public TypeMeasure()
        {
            ShoppingProduct = new HashSet<ShoppingProduct>();
        }

        public int IdTypeMeasure { get; set; }
        public string Description { get; set; }

        public ICollection<ShoppingProduct> ShoppingProduct { get; set; }
    }
}
