using System;
using System.Collections.Generic;

namespace Expenses.API.Models.Brands
{
    public class BrandModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FormatModel> FormatList { get; set; }
    }
}
