using System;
using System.Collections.Generic;

namespace Expenses.Model.Model
{
    public partial class Favourite
    {
        public int IdFavourite { get; set; }
        public int? IdProductBrand { get; set; }
        public int? IdStore { get; set; }

        public ProductBrand IdProductBrandNavigation { get; set; }
        public Store IdStoreNavigation { get; set; }
    }
}
