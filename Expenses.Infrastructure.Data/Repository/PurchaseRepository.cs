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
            return await _context.Purchase.Include(p => p.Store).ToListAsync();
        }

        public async Task<Purchase> GetWithProductsByIdAsync(int id)
        {
            return await _context.Purchase.Include(p => p.Store)
                .Include(p => p.ProductList).FirstOrDefaultAsync(p => p.IdPurchase == id);
        }
    }
}
