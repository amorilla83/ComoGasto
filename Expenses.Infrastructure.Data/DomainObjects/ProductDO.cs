using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Infrastructure.DomainObjects
{
    public class ProductDO
    {
        //TODO: Comprobar si con el EF Core es necesario hacer esto y si están en el sitio correcto
        //Si lo dejamos, todas las entidades de la capa infrastructure tienen que ser DO
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Image { get; set; }
        public List<ProductBrandDO> ProductBrands { get; set; }

        public ProductDO ()
        {
            ProductBrands = new List<ProductBrandDO>();
        }
    }
}
