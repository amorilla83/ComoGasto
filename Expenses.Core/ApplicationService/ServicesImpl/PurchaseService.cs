using System;
using System.Collections.Generic;
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

        public PurchaseService(IUnitOfWork unitOfWork, IPurchaseRepository purchaseRepository)
        {
            _unitOfWork = unitOfWork;
            _purchaseRepository = purchaseRepository;
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
                return new PurchaseResponse($"An error occurred when saving a store: {ex.Message}");
            }
        }

        public async Task<PurchaseResponse> GetPurchaseByIdAsync (int id)
        {
            try
            {
                var purchase = await _purchaseRepository.GetWithProductsByIdAsync(id);

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

        public async Task<PurchaseResponse> AddProductToPurchase(int id, ProductPurchase product)
        {
            try
            {
                Purchase currentPurchase = await _purchaseRepository.GetWithProductsByIdAsync(id);

                if (currentPurchase == null)
                {
                    throw new Exception("La compra no existe en base de datos");
                }

                currentPurchase.ProductList.Add(product);
                currentPurchase.Total += product.Price;

                _purchaseRepository.Update(currentPurchase);
                await _unitOfWork.Commit();

                return new PurchaseResponse(currentPurchase);
            }
            catch (Exception ex)
            {
                return new PurchaseResponse($"An error occurred when adding a product to a purchase with id {id}: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Purchase>> GetAllAsync()
        {
            return await _purchaseRepository.GetAllAsync();
        }

        public async Task<IEnumerable<ProductPurchase>> GetProductsPurchase(int id)
        {
            Purchase purchase = await _purchaseRepository.GetWithProductsByIdAsync(id);

            return purchase.ProductList;
        }
    }
}
