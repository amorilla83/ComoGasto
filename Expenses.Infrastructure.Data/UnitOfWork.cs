using Expenses.Core;
using Expenses.Core.DomainService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Expenses.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private ExpensesContext _context;

        public UnitOfWork (ExpensesContext context)
        {
            _context = context;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
