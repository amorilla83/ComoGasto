using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Expenses.Core.Entities;
using Expenses.Core.Entities.Communication;

namespace Expenses.Core.ApplicationService
{
    public interface IPurchaseService
    {
        Task<IEnumerable<Purchase>> GetAllAsync();
        Task<IEnumerable<ProductPurchase>> GetProductsPurchase(int id);
        Task<PurchaseResponse> GetPurchaseByIdAsync(int id);

        Task<PurchaseResponse> SavePurchaseAsync(Purchase purchase);
        Task<PurchaseResponse> FindPurchaseByIdAsync(int id);
        Task<ProductPurchaseResponse> AddProductToPurchase(int id, ProductPurchase product);
        Task<ProductPurchaseResponse> DeleteProductFromPurchase(int idPurchase, int idProduct);
    }
}
