using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expenses.Core.DomainService;
using Expenses.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Infrastructure.Data.Repository
{
    public class ProductPurchaseRepository : GenericRepository<ProductPurchase>, IProductPurchaseRepository
    {
        public ProductPurchaseRepository(ExpensesContext context)
            : base(context) { }

        public async Task<ProductPurchase> GetByIdAsync(int id)
        {
            return await _context.ProductPurchase
                .Include(pr => pr.Product)
                .Include(pr => pr.ProductDetail).ThenInclude(pd => pd.Product)
                .Include(pr => pr.ProductDetail).ThenInclude(pd => pd.Brand).DefaultIfEmpty()
                .Include(pr => pr.ProductDetail).ThenInclude(pd => pd.Format).DefaultIfEmpty()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ProductPurchase>> GetPurchasesByProduct (int idProduct)
        {
            return await _context.ProductPurchase
                .Include(p => p.Purchase).ThenInclude(p => p.Store)
                .Include(pr => pr.ProductDetail).ThenInclude(pd => pd.Brand).DefaultIfEmpty()
                .Include(pr => pr.ProductDetail).ThenInclude(pd => pd.Format).DefaultIfEmpty()
                .Where(p => p.ProductId == idProduct).ToListAsync();
        }
    }
}

