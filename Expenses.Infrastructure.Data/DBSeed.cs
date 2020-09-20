using Expenses.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Infrastructure.Data
{
    public class DBSeed
    {
        public static void Seed (ExpensesContext context)
        {
            //Borra la base de datos cada vez que arranca.
            //TODO: ELIMINAR CUANDO TENGAMOS UNA BASE DE DATOS DE VERDAD
            context.Database.EnsureDeleted();
            //Confirma si la base de datos está creada
            context.Database.EnsureCreated();

            //var product = context.Product.Add(new Product()
            //{
            //    Name = "Mayonesa",
            //    Detail = "Detalles",
            //    Image = "Este sería el icono"
            //}).Entity;

            //var product2 = context.Product.Add(new Product()
            //{
            //    Name = "Pan de molde",
            //    Detail = "Detalles pan de molde",
            //    Image = "Icono pan de molde"
            //}).Entity;

            //var pb = context.ProductBrand.Add(new ProductBrand()
            //{
            //    Name = "Calvé",
            //    CurrentMoney = 1.45,
            //    Packaging = "450 gr.",
            //    Product = product
            //}).Entity;

            //var pb2 = context.ProductBrand.Add(new ProductBrand()
            //{
            //    Name = "Calvé",
            //    CurrentMoney = 1.15,
            //    Packaging = "250 gr.",
            //    Product = product
            //}).Entity;
            context.SaveChanges();
        }
    }
}
