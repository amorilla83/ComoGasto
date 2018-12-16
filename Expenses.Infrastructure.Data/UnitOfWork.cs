using Expenses.Core;
using Expenses.Core.DomainService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private ExpensesContext _context;

        public UnitOfWork (ExpensesContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
