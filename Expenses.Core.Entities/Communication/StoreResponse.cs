using System;
namespace Expenses.Core.Entities.Communication
{
    public class StoreResponse :  BaseResponse <Store>
    {
        public StoreResponse(Store store)
            : base(store)
        {        }

        public StoreResponse (string message)
            :base (message)
        { }
    }
}
