using Expenses.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Infrastructure.Data
{
    public static class FakeDB
    {
        public static int idProduct = 1;
        public static readonly List<Product> products = new List<Product>();

        public static int idStore = 1;
        public static readonly List<Store> stores = new List<Store>();

        public static int idProductBrand = 1;
        public static readonly List<ProductBrand> productBrands = new List<ProductBrand>();
    }
}
