using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Expenses.Core.Entities;

namespace Expenses.Core.DomainService
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        //Task<IEnumerable<Brand>> GetBrandsByProduct(int id);

        Task<IEnumerable<Brand>> GetAllAsync();

        Task<Brand> GetBrandByNameAsync(Expression<Func<Brand, bool>> match);
    }
}
