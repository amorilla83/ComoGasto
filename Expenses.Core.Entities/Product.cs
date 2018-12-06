using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Image { get; set; }
        public List<Product> Products { get; set; }

        public Product ()
        {
            Products = new List<Product>();
        }
    }
}
