using Expenses.Core.DomainService;
using Expenses.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expenses.Infrastructure.Data.Repository
{
    public class ProductBrandRepository : IProductBrandRepository
    {
        private readonly ExpensesContext _context;

        public ProductBrandRepository (ExpensesContext context)
        {
            _context = context;
        }

        public ProductBrandRepository ()
        {
            /*if (FakeDB.productBrands.Count == 0)
            {
                var pb = new ProductBrand()
                {
                    Id = FakeDB.idProductBrand++,
                    Name = "Calvé",
                    CurrentMoney = 1.45,
                    Size = 450,
                    Product = new Product () { Id = 1}
                };
                FakeDB.productBrands.Add(pb);

                var pb2 = new ProductBrand()
                {
                    Id = FakeDB.idProductBrand++,
                    Name = "Calvé",
                    CurrentMoney = 1.15,
                    Size = 250,
                    Product = new Product () { Id = 1}
                };

                FakeDB.productBrands.Add(pb2);
            }*/
        }

        public IEnumerable<ProductBrand> GetAll()
        {
            // return FakeDB.productBrands;
            return _context.ProductBrand;
        }

        public ProductBrand GetById(int id)
        {
            /* foreach (ProductBrand pb in FakeDB.productBrands)
             {
                 if (pb.Id == id)
                 {
                     return pb;
                 }
             }
             return null;*/
            return _context.ProductBrand.FirstOrDefault(pb => pb.Id == id);
        }

        public ProductBrand Insert(ProductBrand productBrand)
        {
            /*productBrand.Id = FakeDB.idProductBrand++;
            FakeDB.productBrands.Add(productBrand);
            return productBrand;*/
            return _context.ProductBrand.Add(productBrand).Entity;
        }

        public ProductBrand Update(ProductBrand productBrandUpdate)
        {
            /*ProductBrand pb = GetById(productBrandUpdate.Id);
            pb.Name = productBrandUpdate.Name;
            pb.Size = productBrandUpdate.Size;
            pb.CurrentMoney = productBrandUpdate.CurrentMoney;
            pb.Product = productBrandUpdate.Product;
            return pb;*/
            return _context.ProductBrand.Update(productBrandUpdate).Entity;
        }

        public ProductBrand Delete(int id)
        {
            /* ProductBrand pb = GetById(id);
             if (pb == null)
             {
                 return null;
             }
             FakeDB.productBrands.Remove(pb);
             return pb;*/
            return null;
        }
    }
}
