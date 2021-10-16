using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Expenses.Core.Entities;
using Expenses.Core.Entities.Communication;

namespace Expenses.Core.ApplicationService
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetBrandsByProduct(int id);

        Task<IEnumerable<Brand>> GetAllBrandsAsync();

        Task<Brand> FindBrandByNameAsync(string name);

        Task<Brand> FindBrandByIdAsync(int id);

        Task<BrandResponse> SaveBrandAsync(Brand brand);
    }
}
