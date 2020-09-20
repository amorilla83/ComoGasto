using System.Collections.Generic;
using Expenses.Core.Entities;

namespace Expenses.Core.DomainService
{
    public interface IProductBrandRepository
    {
        int Count();

        //GET
        IEnumerable<ProductBrand> GetAll(Filter filter = null);

        ProductBrand GetById(int id);

        //POST
        ProductBrand Insert(ProductBrand productBrand);

        //PUT
        ProductBrand Update(ProductBrand productBrandUpdate);

        //DELETE
        ProductBrand Delete(int id);
    }
}
