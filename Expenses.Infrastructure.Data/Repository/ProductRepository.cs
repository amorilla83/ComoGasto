using Expenses.Core.DomainService;
using Expenses.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Infrastructure.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository()
        {
            if (FakeDB.products.Count == 0)
            {
                var product = new Product
                {
                    Id = FakeDB.idProduct++,
                    Name = "Mayonesa",
                    Detail = "Detalles",
                    Image = "Este sería el icono"
                };

                var product2 = new Product
                {
                    Id = FakeDB.idProduct++,
                    Name = "Pan de molde",
                    Detail = "Detalles pan de molde",
                    Image = "Icono pan de molde"
                };

                FakeDB.products.Add(product);
                FakeDB.products.Add(product2);
            }
        }

        public IEnumerable<Product> GetAll()
        {
            return FakeDB.products;
        }

        public Product GetById(int id)
        {
            foreach (Product p in FakeDB.products)
            {
                if (p.Id == id)
                {
                    return p;
                }
            }

            return null;
        }

        public Product Insert(Product product)
        {
            product.Id = FakeDB.idProduct++;
            FakeDB.products.Add(product);
            return product;
        }

        public Product Update(Product productUpdate)
        {
            Product p = GetById(productUpdate.Id);
            p.Name = productUpdate.Name;
            p.Detail = productUpdate.Detail;
            p.Image = productUpdate.Image;
            return p;
        }

        public Product Delete(int id)
        {
            Product p = GetById(id);
            if (p == null)
            {
                return null;
            }
            FakeDB.products.Remove(p);
            return p;
        }
    }
}
