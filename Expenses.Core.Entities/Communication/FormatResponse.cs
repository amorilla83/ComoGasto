using System;
namespace Expenses.Core.Entities.Communication
{
    public class FormatResponse : BaseResponse<Format>
    {
        public FormatResponse(Format format)
            : base(format)
        { }

        public FormatResponse(string message)
            : base(message)
        { }
    }
}
