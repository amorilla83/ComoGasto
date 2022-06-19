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
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(ExpensesContext context)
            : base(context) { }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _context.Brand.OrderBy(b => b.Name).ToListAsync();
        }

        public async Task<IEnumerable<Brand>> GetBrandsByProduct(int id)
        {
            //return await _context.Brand.Include(b => b.FormatList).Where(b => b.ProductList.Any(p => p.Id == id)).ToListAsync();
            return null;
        }

        public async Task<Brand> GetBrandByNameAsync(Expression<Func<Brand, bool>> match)
        {
            return await _context.Brand.FirstOrDefaultAsync(match);
        }
    }
}
