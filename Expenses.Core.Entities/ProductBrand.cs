using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Core.Entities
{
    public class ProductBrand
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public string Name { get; set; }
        public string Packaging { get; set; }
        public double CurrentMoney { get; set; }
    }
}
