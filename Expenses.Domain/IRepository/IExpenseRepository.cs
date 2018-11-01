using Expenses.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Domain.IRepository
{
    public interface IExpenseRepository : IGenericRepository<Expense>
    {
        Expense GetExpense(int idExpense);
    }
}
