using System.Collections.Generic;
using System.Threading.Tasks;
using Expenses.Core.Entities;

namespace Expenses.Core.DomainService
{
    public interface IProductDetailsRepository
    {
        //int Count();

        ////GET
        //IEnumerable<ProductDetails> GetAll(Filter filter = null);
        IEnumerable<Format> GetFormatsByBrand(int idBrand);

        Task<ProductDetails> GetByDataAsync(int idProduct, int? idBrand, int? idFormat);

        //ProductDetails GetById(int id);

        ////POST
        //ProductDetails Insert(ProductDetails productBrand);

        ////PUT
        //ProductDetails Update(ProductDetails productBrandUpdate);

        ////DELETE
        //ProductDetails Delete(int id);
    }
}
