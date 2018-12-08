using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Core.Entities
{
    public class Filter
    {
        //Serán los parámetros que se tienen que poner en la query de la llamada al servicio
        //api/ProductBrands?CurrentPage=1&&ItemsPerPage=10
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
