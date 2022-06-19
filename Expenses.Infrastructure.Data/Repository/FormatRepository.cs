using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expenses.Core.DomainService;
using Expenses.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Infrastructure.Data.Repository
{
    public class FormatRepository : GenericRepository<Format>, IFormatRepository
    {
        public FormatRepository(ExpensesContext context)
            :base (context) {    }

        public async Task<IEnumerable<Format>> GetAllAsync()
        {
            return await _context.Format.OrderBy(f => f.Name).ToListAsync();
        }
    }
}
