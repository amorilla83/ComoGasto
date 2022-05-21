using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expenses.Core.DomainService;
using Expenses.Core.Entities;
using Expenses.Core.Entities.Communication;

namespace Expenses.Core.ApplicationService.ServicesImpl
{
    public class PurchaseService : IPurchaseService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IPurchaseRepository _purchaseRepository;
        public readonly IBrandRepository _brandRepository;
        public readonly IFormatRepository _formatRepository;
        public readonly IProductRepository _productRepository;

        public PurchaseService(IUnitOfWork unitOfWork,
            IPurchaseRepository purchaseRepository,
            IBrandRepository brandRepository,
            IFormatRepository formatRepository,
            IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _purchaseRepository = purchaseRepository;
            _brandRepository = brandRepository;
            _formatRepository = formatRepository;
            _productRepository = productRepository;
        }

        public async Task<PurchaseResponse> SavePurchaseAsync(Purchase purchase)
        {
            try
            {
                var newPurchase = await _purchaseRepository.AddAsync(purchase);
                await _unitOfWork.Commit();
                return new PurchaseResponse(newPurchase);
            }
            catch (Exception ex)
            {
                return new PurchaseResponse($"An error occurred when saving a purchase: {ex.Message}");
            }
        }

        public async Task<PurchaseResponse> GetPurchaseByIdAsync (int id)
        {
            try
            {
                var purchase = await _purchaseRepository.GetAllDataByIdAsync(id);

                return new PurchaseResponse(purchase);
            }
            catch (Exception ex)
            {
                return new PurchaseResponse($"An error occurred when getting a purchase with id {id}: {ex.Message}");
            }
        }

        public async Task<PurchaseResponse> FindPurchaseByIdAsync(int id)
        {
            try
            {
                var purchase = await _purchaseRepository.GetByIdAsync(id);

                return new PurchaseResponse(purchase);
            }
            catch (Exception ex)
            {
                return new PurchaseResponse($"An error occurred when finding a purchase with id {id}: {ex.Message}");
            }
        }

        public async Task<ProductPurchaseResponse> AddProductToPurchase(int id, ProductPurchase product)
        {
            try
            {
                Purchase currentPurchase = await _purchaseRepository.GetAllDataByIdAsync(id);

                if (currentPurchase == null)
                {
                    throw new Exception("La compra no existe en base de datos");
                }

                currentPurchase.ProductList.Add(product);
                currentPurchase.Total += product.Price;

                _purchaseRepository.Update(currentPurchase);
                await _unitOfWork.Commit();

                //Cargamos en el producto los datos que faltan
                if (product.Brand == null && product.BrandId.HasValue)
                {
                    product.Brand = await _brandRepository.GetByIdAsync(product.BrandId);
                }

                if (product.Product == null)
                {
                    product.Product = await _productRepository.GetByIdAsync(product.ProductId);
                }

                if (product.Format == null && product.FormatId.HasValue)
                {
                    product.Format = await _formatRepository.GetByIdAsync(product.FormatId);
                }

                return new ProductPurchaseResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductPurchaseResponse($"An error occurred when adding a product to a purchase with id {id}: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Purchase>> GetAllAsync()
        {
            return await _purchaseRepository.GetAllAsync();
        }

        public async Task<IEnumerable<ProductPurchase>> GetProductsPurchase(int id)
        {
            Purchase purchase = await _purchaseRepository.GetAllDataByIdAsync(id);

            return purchase.ProductList;
        }

        public async Task<ProductPurchaseResponse> DeleteProductFromPurchase(int idPurchase, int idProduct)
        {
            try
            {
                Purchase purchase = await _purchaseRepository.GetWithProductsByIdAsync(idPurchase);
                ProductPurchase product = purchase.ProductList.SingleOrDefault(p => p.ProductId == idProduct);

                if (product == null)
                {
                    throw new Exception($"No existe el producto {idProduct} en la compra {idPurchase}");
                }

                purchase.ProductList.Remove(purchase.ProductList.Single(p => p.ProductId == idProduct));

                _purchaseRepository.Update(purchase);
                await _unitOfWork.Commit();

                return new ProductPurchaseResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductPurchaseResponse($"An error occurred when deleting a product with id {idProduct} " +
                    $"from a purchase with id { idPurchase }: { ex.Message}");
            }
            
        }
    }
}
