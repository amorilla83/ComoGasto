using Expenses.Core.Entities;
using Expenses.Core.Entities.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Expenses.Core.ApplicationService
{
    public interface IProductService
    {
        //POST
        //Product NewProduct(string name);

        Task<ProductResponse> SaveProductAsync(Product product);

        ////GET
        //Task<ProductResponse> FindProductByIdAsync(int id);

        Task<PaginatedEntity<Product>> GetAllProductsAsync(int page, int itemsPerPage);

        Task<Product> GetProductDetailsAsync(int id);

        Task<IEnumerable<ProductPurchase>> GetProductPurchaseByIdProduct(int idProduct);

        List<ProductReview> GetProductReview();

        ////PUT
        //Task<ProductResponse> UpdateProductAsync(int id, Product productUpdate);

        ////DELETE
        //Task<ProductResponse> DeleteProductAsync(int id);
    }
}
