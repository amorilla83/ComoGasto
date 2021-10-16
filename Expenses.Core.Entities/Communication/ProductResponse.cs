using System;
namespace Expenses.Core.Entities.Communication
{
    public class ProductResponse :BaseResponse<Product>
    {
        public ProductResponse(Product product)
            : base(product)
        { }

        public ProductResponse(string message)
            : base(message)
        { }
    }
}
