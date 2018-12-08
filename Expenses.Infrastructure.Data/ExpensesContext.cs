using Expenses.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Infrastructure.Data
{
    public class ExpensesContext : DbContext
    {
        public ExpensesContext (DbContextOptions<ExpensesContext> options) : base (options)
        {        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ProductBrand tiene un product, que tiene varios ProductBrands
            modelBuilder.Entity<ProductBrand>()
                .HasOne(pb => pb.Product)
                .WithMany(p => p.ProductBrands)
                .OnDelete(DeleteBehavior.SetNull); //Al borrar un producto, poner a null el product de ProductBrand
            //TODO: Comprobar si es el behavior que quiero. 
            //Cascada puede ser peligroso. Vale mas poner setNull y borrarlo manualmete para evitar problemas
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductBrand> ProductBrand { get; set; }

    }
}
