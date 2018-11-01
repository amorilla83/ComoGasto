using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        
    }
}
