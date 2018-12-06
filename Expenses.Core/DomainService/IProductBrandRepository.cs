using Expenses.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Core.DomainService
{
    public interface IProductBrandRepository
    {
        //GET
        IEnumerable<ProductBrand> GetAll();

        ProductBrand GetById(int id);

        //POST
        ProductBrand Insert(ProductBrand productBrand);

        //PUT
        ProductBrand Update(ProductBrand productBrandUpdate);

        //DELETE
        ProductBrand Delete(int id);
    }
}
