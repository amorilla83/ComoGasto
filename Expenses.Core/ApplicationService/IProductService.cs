using Expenses.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Core.ApplicationService
{
    public interface IProductService
    {
        //GET
        Product FindProductById(int id);

        Product FindProductByIdIncludeBrands(int id);

        List<Product> GetAllProducts();

        //POST
        Product SaveProduct(Product product);

        //PUT
        Product UpdateProduct(Product productUpdate);

        //DELETE
        Product DeleteProduct(int id);
    }
}
