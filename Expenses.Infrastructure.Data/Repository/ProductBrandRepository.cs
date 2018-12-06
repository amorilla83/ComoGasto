using Expenses.Core.DomainService;
using Expenses.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Infrastructure.Data.Repository
{
    public class ProductBrandRepository : IProductBrandRepository
    {
        public ProductBrandRepository ()
        {
            if (FakeDB.productBrands.Count == 0)
            {
                var pb = new ProductBrand
                {
                    Name = "Calvé",
                    CurrentMoney = 1.45,
                    Size = 450
                };
                FakeDB.productBrands.Add(pb);

                var pb2 = new ProductBrand
                {
                    Name = "Calvé",
                    CurrentMoney = 1.15,
                    Size = 250
                };

                FakeDB.productBrands.Add(pb2);
            }
        }

        public IEnumerable<ProductBrand> GetAll()
        {
            return FakeDB.productBrands;
        }

        public ProductBrand GetById(int id)
        {
            foreach (ProductBrand pb in FakeDB.productBrands)
            {
                if (pb.Id == id)
                {
                    return pb;
                }
            }
            return null;
        }

        public ProductBrand Insert(ProductBrand productBrand)
        {
            productBrand.Id = FakeDB.idProductBrand++;
            FakeDB.productBrands.Add(productBrand);
            return productBrand;
        }

        public ProductBrand Update(ProductBrand productBrandUpdate)
        {
            ProductBrand pb = GetById(productBrandUpdate.Id);
            pb.Name = productBrandUpdate.Name;
            pb.Size = productBrandUpdate.Size;
            pb.CurrentMoney = productBrandUpdate.CurrentMoney;
            pb.Product = productBrandUpdate.Product;
            return pb;
        }

        public ProductBrand Delete(int id)
        {
            ProductBrand pb = GetById(id);
            if (pb == null)
            {
                return null;
            }
            FakeDB.productBrands.Remove(pb);
            return pb;
        }
    }
}
