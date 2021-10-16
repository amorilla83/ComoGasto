using System;
namespace Expenses.Core.Entities.Communication
{
    public class BrandResponse : BaseResponse<Brand>
    {
        public BrandResponse(Brand brand)
            : base(brand)
        { }

        public BrandResponse(string message)
            : base(message)
        { }
    }
}
