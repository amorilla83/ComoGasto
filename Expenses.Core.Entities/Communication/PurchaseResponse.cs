using System;
namespace Expenses.Core.Entities.Communication
{
    public class PurchaseResponse : BaseResponse<Purchase>
    {
        public PurchaseResponse(Purchase purchase)
            : base(purchase)
        { }

        public PurchaseResponse(string message)
            : base(message)
        { }
    }
}
