using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Core
{
    public interface IUnitOfWork : IDisposable
    {


        void Commit();

        
    }
}
