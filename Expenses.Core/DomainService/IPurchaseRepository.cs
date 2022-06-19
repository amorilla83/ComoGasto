using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Expenses.Core.Entities;

namespace Expenses.Core.DomainService
{
    public interface IPurchaseRepository : IGenericRepository<Purchase>
    {
        Task<IEnumerable<Purchase>> GetAllAsync();
        Task<Purchase> GetAllDataByIdAsync(int id);
        Task<Purchase> GetWithProductsByIdAsync(int id);
        void UpdateProduct(Purchase updatePurchase, ProductPurchase updateProduct);
    }
}
