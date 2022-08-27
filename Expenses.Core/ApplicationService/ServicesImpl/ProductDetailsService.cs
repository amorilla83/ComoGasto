using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expenses.Core.DomainService;
using Expenses.Core.Entities;

namespace Expenses.Core.ApplicationService.ServicesImpl
{
    public class ProductDetailsService : IProductDetailsService
    {
        private IProductDetailsRepository _productDetailsRepository;
        private IProductRepository _productRepository;

        public ProductDetailsService (IProductDetailsRepository productDetailsRepository,
            IProductRepository productRepository)
        {
            _productDetailsRepository = productDetailsRepository;
            _productRepository = productRepository;
        }

        public async Task<ProductDetails> GetProductDetailsByDataAsync (int productId, int? brandId, int? formatId)
        {
            return await _productDetailsRepository.GetByDataAsync(productId, brandId, formatId);
        }

        public ProductDetails FindProductBrandById(int id)
        {
            //return _productBrandRepository.GetById(id);
            return null;
        }

        public List<ProductDetails> GetAllProductBrands()
        {
            //return _productBrandRepository.GetAll().ToList();
            return null;
        }

        public List<ProductDetails> GetFilteredProductBrands(Filter filter)
        {
            //if (filter.CurrentPage <= 0 || filter.ItemsPerPage <= 0)
            //{
            //    throw new InvalidDataException("CurrentPage and ItemsPerPage must be greater than 0");
            //}
            //if (((filter.CurrentPage - 1) * filter.ItemsPerPage) >= _productBrandRepository.Count())
            //{
            //    throw new InvalidDataException("Index out of bounds. CurrentPage is too high");
            //}

            //return _productBrandRepository.GetAll(filter).ToList();
            return null;
        }

        public ProductDetails SaveProductBrand(ProductDetails productBrand)
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

        public ProductDetails UpdateProductBrand(ProductDetails productBrandUpdate)
        {
            //return _productBrandRepository.Update(productBrandUpdate);
            return null;
        }

        public ProductDetails DeleteProductBrand(int id)
        {
            //return _productBrandRepository.Delete(id);
            return null;
        }

        public List<Format> GetFormatsByBrand(int idBrand)
        {
            return _productDetailsRepository.GetFormatsByBrand(idBrand).OrderBy(f => f.Name).ToList();
        }
    }
}
