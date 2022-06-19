using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Expenses.Core.Entities;
using Expenses.Core.Entities.Communication;

namespace Expenses.Core.ApplicationService
{
    public interface IFormatService
    {
        Task<FormatResponse> SaveFormatAsync(Format format);
        Task<IEnumerable<Format>> GetAllFormatsAsync();
    }
}
