using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Expenses.Core.DomainService;
using Expenses.Core.Entities;

namespace Expenses.Core.ApplicationService.ServicesImpl
{
    public class ProductBrandService : IProductBrandService
    {
        private IProductBrandRepository _productBrandRepository;

        public ProductBrandService (IProductBrandRepository productBrandRepository)
        {
            _productBrandRepository = productBrandRepository;
        }

        public ProductBrand FindProductBrandById(int id)
        {
            return _productBrandRepository.GetById(id);
        }

        public List<ProductBrand> GetAllProductBrands()
        {
            return _productBrandRepository.GetAll().ToList();
        }

        public ProductBrand SaveProductBrand(ProductBrand productBrand)
        {
            return _productBrandRepository.Insert(productBrand);
        }

        public ProductBrand UpdateProductBrand(ProductBrand productBrandUpdate)
        {
            return _productBrandRepository.Update(productBrandUpdate);
        }

        public ProductBrand DeleteProductBrand(int id)
        {
            return _productBrandRepository.Delete(id);
        }
    }
}
