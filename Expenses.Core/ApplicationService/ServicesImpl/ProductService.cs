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
        private IUnitOfWork _unitOfWork;
        private IProductRepository _productRepository;
        private IProductBrandRepository _productBrandRepository;

        public ProductService (IUnitOfWork unitOfWork, IProductRepository productRepository, IProductBrandRepository productBrandRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _productBrandRepository = productBrandRepository;
        }

        public Product FindProductById(int id)
        {
            return _productRepository.GetById(id);
        }

        public Product FindProductByIdIncludeBrands (int id)
        {
            Product product = _productRepository.GetByIdIncludeProductBrands(id);
            //Si lo hacemos así, obtenemos todos los ProductBrand y luego aplica el where
            //Por eso hay que llevar estas condiciones a un nuevo método del repositorio
            /*Product product = _productRepository.GetById(id);
            product.ProductBrands = _productBrandRepository.GetAll().
                Where(pb => pb.Product != null && pb.Product.Id == id).ToList();*/
            return product;
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAll().ToList();
        }

        public Product SaveProduct(Product product)
        {
            using (_unitOfWork)
            {
                Product productSaved = _productRepository.Insert(product);
                _unitOfWork.Commit();
                return productSaved;
            }
        }

        public Product UpdateProduct(Product productUpdate)
        {
            using (_unitOfWork)
            {

                Product product = _productRepository.Update(productUpdate);
                _unitOfWork.Commit();
                return product;
            }
        }

        public Product DeleteProduct(int id)
        {
            using (_unitOfWork)
            {
                Product product = _productRepository.Delete(id);
                _unitOfWork.Commit();
                return product;
            }
        }
    }
}
