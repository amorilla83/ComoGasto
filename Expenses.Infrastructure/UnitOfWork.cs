using Expenses.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Infrastructure
{
    class UnitOfWork : IUnitOfWork
    {
        private DB_ExpensesContext _context;

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
