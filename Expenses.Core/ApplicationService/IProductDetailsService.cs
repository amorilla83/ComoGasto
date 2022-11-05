using Expenses.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Expenses.Core.ApplicationService
{
    public interface IProductDetailsService
    {
        //GET
        //ProductDetails FindProductBrandById(int id);

        //List<ProductDetails> GetAllProductBrands();

        //List<ProductDetails> GetFilteredProductBrands(Filter filter);

        List<Format> GetFormatsByBrand(int idBrand);
        Task<ProductDetails> GetProductDetailsByDataAsync(int productId, int? brandId, int? formatId);
        
        ////POST
        //ProductDetails SaveProductBrand(ProductDetails productBrand);

        ////PUT
        //ProductDetails UpdateProductBrand(ProductDetails productBrandUpdate);

        ////DELETE
        //ProductDetails DeleteProductBrand(int id);
    }
}
