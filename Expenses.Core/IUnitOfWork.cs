using System;
using System.Threading.Tasks;

namespace Expenses.Core
{
    public interface IUnitOfWork : IDisposable
    {
        Task Commit();        
    }
}
