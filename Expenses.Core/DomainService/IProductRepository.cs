using Expenses.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Expenses.Core.DomainService
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllAsync();
        ////GET
        //IEnumerable<Product> GetAll();

        //Product GetById(int id);

        //Product GetByIdIncludeProductBrands(int id);

        ////POST
        //Product Insert(Product product);

        ////PUT
        //Product Update(Product productUpdate);

        ////DELETE
        //Product Delete(int id);
    }
}
