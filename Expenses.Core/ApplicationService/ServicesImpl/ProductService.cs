using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Expenses.Core.DomainService;
using Expenses.Core.Entities;

namespace Expenses.Core.ApplicationService.ServicesImpl
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService (IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product FindProductById(int id)
        {
            return _productRepository.GetById(id);
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAll().ToList();
        }

        public Product SaveProduct(Product product)
        {
            return _productRepository.Insert(product);
        }

        public Product UpdateProduct(Product productUpdate)
        {
            return _productRepository.Update(productUpdate);
        }

        public Product DeleteProduct(int id)
        {
            return _productRepository.Delete(id);
        }
    }
}
