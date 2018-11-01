using Expenses.Domain;
using Expenses.Domain.IRepository;
using Expenses.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Data.Repository
{
    public class ExpenseRepository : GenericRepository<Expense>, IExpenseRepository
    {
        public ExpenseRepository (DB_ExpensesContext context) 
            :base (context)
        { }

        public Expense GetExpense(int idExpense)
        {
            throw new NotImplementedException();
        }
    }
}
