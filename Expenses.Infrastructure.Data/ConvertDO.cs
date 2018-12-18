using Expenses.Core.Entities;
using Expenses.Infrastructure.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Expenses.Infrastructure.Data
{
    public static class ConvertDO
    {
        //Se podría hacer un Factory que seleccione el convert correcto para cada Objeto



        //Si dejamos los objetos de dominio, debemos hacer un convert para pasar de BO a Entidad
        //TODO: Comprobar si es necesario y si tiene que estar en esta capa
        //TODO: Utilizar genericos para no tener que picar todo el código
        public static Product Convert (ProductDO productDO)
        {
            return new Product()
            {
                Id = productDO.Id,
                Detail = productDO.Detail,
                Image = productDO.Image,
                Name = productDO.Name,
                ProductBrands = productDO.ProductBrands.Select(pb => Convert(pb)).ToList()
            };
        }

        public static ProductDO Convert(Product product)
        {
            return new ProductDO()
            {
                Id = product.Id,
                Detail = product.Detail,
                Image = product.Image,
                Name = product.Name,
                //Con poner solo el método ya entiende la conversión que tiene que hacer, recibe lo que selecciona y devuelve el objeto que debe
                ProductBrands = product.ProductBrands.Select(Convert).ToList()
            };
        }

        public static ProductBrand Convert (ProductBrandDO productBrandDO)
        {
            return new ProductBrand()
            {
                Id = productBrandDO.Id,
                Name = productBrandDO.Name,
                Packaging = productBrandDO.Packaging,
                CurrentMoney = productBrandDO.CurrentMoney,
                Product = Convert(productBrandDO.Product)
            };
        }

        public static ProductBrandDO Convert(ProductBrand productBrand)
        {
            return new ProductBrandDO()
            {
                Id = productBrand.Id,
                Name = productBrand.Name,
                Packaging = productBrand.Packaging,
                CurrentMoney = productBrand.CurrentMoney,
                Product = Convert(productBrand.Product)
            };
        }
    }
}
