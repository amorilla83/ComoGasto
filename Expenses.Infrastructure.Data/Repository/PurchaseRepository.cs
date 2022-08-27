using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Expenses.Core.DomainService;
using Expenses.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Infrastructure.Data.Repository
{
    public class PurchaseRepository : GenericRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(ExpensesContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Purchase>> GetAllAsync()
        {
            return await _context.Purchase.Include(p => p.Store).OrderBy(p => p.Date).ToListAsync();
        }

        public async Task<Purchase> GetAllDataByIdAsync(int id)
        {
            return await _context.Purchase.Include(p => p.Store)
                .Include(p => p.ProductList).ThenInclude(pr => pr.Product)
                .Include(p => p.ProductList).ThenInclude(pr => pr.ProductDetail).ThenInclude(pd => pd.Product)
                .Include(p => p.ProductList).ThenInclude(pr => pr.ProductDetail).ThenInclude(pd => pd.Brand)
                .Include(p => p.ProductList).ThenInclude(pr => pr.ProductDetail).ThenInclude(pd => pd.Format)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Purchase> GetWithProductsByIdAsync (int id)
        {
            return await _context.Purchase.Include(p => p.ProductList)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public void UpdateProduct(Purchase updatePurchase, ProductPurchase updateProduct)
        {
            _context.Set<Purchase>().Update(updatePurchase);

            _context.Entry(updateProduct).CurrentValues.SetValues(updateProduct);
            _context.Entry(updateProduct).State = EntityState.Modified;
        }
    }
}
