using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expenses.Core.DomainService;
using Expenses.Core.Entities;
using Expenses.Core.Entities.Communication;
using Expenses.Core.Entities.Infrastructure;
using Microsoft.Extensions.Caching.Memory;

namespace Expenses.Core.ApplicationService.ServicesImpl
{
    public class ProductService : IProductService
    {
        private IUnitOfWork _unitOfWork;
        private IProductRepository _productRepository;
        private IProductPurchaseRepository _productPurchaseRepository;

        private readonly IMemoryCache _cache;

        public ProductService (IUnitOfWork unitOfWork, IMemoryCache cache,
            IProductRepository productRepository, IProductPurchaseRepository productPurchaseRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _productPurchaseRepository = productPurchaseRepository;
            _cache = cache;
        }

        public Product NewProduct(string name)
        {
            Product product = new()
            {
                Name = name
            };
            return product;
        }

        public async Task<ProductResponse> SaveProductAsync(Product product)
        {
            try
            {
                var newProduct = await _productRepository.AddAsync(product);
                await _unitOfWork.Commit();
                UpdateCache();
                return new ProductResponse(newProduct);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occurred when saving a store: {ex.Message}");
            }
        }

        public async Task<ProductResponse> FindProductByIdAsync(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);

                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occurred when finding a store with id {id}: {ex.Message}");
            }
        }

        public async Task<PaginatedEntity<Product>> GetAllProductsAsync(int page = 1, int itemsPerPage = 0)
        {
            //Solo aplica la caché cuando se obtienen todos los productos
            if (itemsPerPage == 0)
            {
                var products = await _cache.GetOrCreateAsync(CacheKeys.ProductsList, (entry) =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                    return _productRepository.GetAllAsync(page, itemsPerPage);
                });

                return products;
            }
            else
            {
                return await _productRepository.GetAllAsync(page, itemsPerPage);
            }
        }

        public async Task<IEnumerable<ProductPurchase>> GetProductPurchaseByIdProduct(int idProduct)
        {
            return await _productPurchaseRepository.GetPurchasesByProduct(idProduct);
        }

        public async Task<Product> GetProductDetailsAsync (int id)
        {
            return await _productRepository.GetProductDetailsAsync(id);
        }

        public async Task<ProductResponse> UpdateProductAsync(int id, Product productUpdate)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);

            if (existingProduct == null)
            {
                return new ProductResponse("Product not found");
            }

            existingProduct.Name = productUpdate.Name;

            try
            {
                //_productRepository.Update(existingProduct);
                //await _unitOfWork.Commit();
                //UpdateCache();
                //return new ProductResponse(existingProduct);
                return null;
            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occurred when updating the store: {ex.Message}");
            }
        }

        public async Task<ProductResponse> DeleteProductAsync(int id)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);

            if (existingProduct == null)
            {
                return new ProductResponse("Product not found");
            }

            try
            {
                await _productRepository.DeleteByIdAsync(id);
                await _unitOfWork.Commit();
                UpdateCache();
                return new ProductResponse(existingProduct);

            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occurred when removing the product: {ex.Message}");
            }
        }

        private void UpdateCache()
        {
            try
            {
                _cache.Remove(CacheKeys.StoresList);
            }
            catch (Exception)
            {
                throw;
            }
        }


        //public Product FindProductByIdIncludeBrands (int id)
        //{
        //    Product product = _productRepository.GetByIdIncludeProductBrands(id);
        //    //Si lo hacemos así, obtenemos todos los ProductBrand y luego aplica el where
        //    //Por eso hay que llevar estas condiciones a un nuevo método del repositorio
        //    /*Product product = _productRepository.GetById(id);
        //    product.ProductBrands = _productBrandRepository.GetAll().
        //        Where(pb => pb.Product != null && pb.Product.Id == id).ToList();*/
        //    return product;
        //}

    }
}
