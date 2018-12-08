using Expenses.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Core.ApplicationService
{
    public interface IProductBrandService
    {
        //GET
        ProductBrand FindProductBrandById(int id);

        List<ProductBrand> GetAllProductBrands();
        
        List<ProductBrand> GetFilteredProductBrands(Filter filter);

        //POST
        ProductBrand SaveProductBrand(ProductBrand productBrand);

        //PUT
        ProductBrand UpdateProductBrand(ProductBrand productBrandUpdate);

        //DELETE
        ProductBrand DeleteProductBrand(int id);
    }
}
