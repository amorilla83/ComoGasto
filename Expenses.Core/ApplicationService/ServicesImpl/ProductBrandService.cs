using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Expenses.Core.DomainService;
using Expenses.Core.Entities;

namespace Expenses.Core.ApplicationService.ServicesImpl
{
    public class ProductBrandService : IProductBrandService
    {
        private IProductBrandRepository _productBrandRepository;
        private IProductRepository _productRepository;

        public ProductBrandService (IProductBrandRepository productBrandRepository,
            IProductRepository productRepository)
        {
            _productBrandRepository = productBrandRepository;
            _productRepository = productRepository;
        }

        public ProductBrand FindProductBrandById(int id)
        {
            return _productBrandRepository.GetById(id);
        }

        public List<ProductBrand> GetAllProductBrands()
        {
            return _productBrandRepository.GetAll().ToList();
        }

        public List<ProductBrand> GetFilteredProductBrands(Filter filter)
        {
            if (filter.CurrentPage <= 0 || filter.ItemsPerPage <= 0)
            {
                throw new InvalidDataException("CurrentPage and ItemsPerPage must be greater than 0");
            }
            if (((filter.CurrentPage - 1) * filter.ItemsPerPage) >= _productBrandRepository.Count())
            {
                throw new InvalidDataException("Index out of bounds. CurrentPage is too high");
            }

            return _productBrandRepository.GetAll(filter).ToList();
        }

        public ProductBrand SaveProductBrand(ProductBrand productBrand)
        {
            return null;
            ////El producto no es obligatorio pero lo pongo como muestra de validación
            //if (productBrand.Product == null || productBrand.Product.Id <= 0)
            //{
            //    throw new InvalidDataException("To create a product Brand, you need a Product");
            //}
            //if (_productRepository.GetById(productBrand.Product.Id) == null)
            //{
            //    throw new InvalidDataException("The product doesn't exists");
            //}
            //return _productBrandRepository.Insert(productBrand);
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
