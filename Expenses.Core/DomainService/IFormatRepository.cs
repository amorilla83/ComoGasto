using System.Collections.Generic;
using System.Threading.Tasks;
using Expenses.Core.Entities;

namespace Expenses.Core.DomainService
{
    public interface IFormatRepository : IGenericRepository<Format>
    {
        Task<IEnumerable<Format>> GetAllAsync();
    }
}
