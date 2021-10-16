using System;
using Expenses.Core.DomainService;
using Expenses.Core.Entities;

namespace Expenses.Infrastructure.Data.Repository
{
    public class FormatRepository : GenericRepository<Format>, IFormatRepository
    {
        public FormatRepository(ExpensesContext context)
            :base (context) {    }
    }
}
