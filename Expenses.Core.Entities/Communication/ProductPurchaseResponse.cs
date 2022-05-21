using System;
namespace Expenses.Core.Entities.Communication
{
    public class ProductPurchaseResponse: BaseResponse<ProductPurchase>
    {
        public ProductPurchaseResponse(ProductPurchase product)
            : base(product)
        { }

        public ProductPurchaseResponse(string message)
            : base(message)
        { }
    }
}
