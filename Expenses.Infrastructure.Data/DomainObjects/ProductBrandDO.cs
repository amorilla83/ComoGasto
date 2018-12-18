using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Infrastructure.DomainObjects
{
    public class ProductBrandDO
    {
        public int Id { get; set; }
        public ProductDO Product { get; set; }
        public string Name { get; set; }
        public string Packaging { get; set; }
        public double CurrentMoney { get; set; }

        //Hacemos esta prueba como cambio en la entidad DO que no debería estar en la entidad
        public string FullName
        {
            get { return $"{Name} {Packaging}"; }
        }
    }
}
