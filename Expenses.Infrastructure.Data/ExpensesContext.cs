using Expenses.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Infrastructure.Data
{
    public class ExpensesContext : DbContext
    {
        //dotnet ef migrations add LogoInDB --project ../Expenses.Model

        public DbSet<Store> Store { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Format> Format { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<ProductPurchase> ProductPurchase { get; set; }

        public ExpensesContext ()
        { }

        public ExpensesContext(DbContextOptions<ExpensesContext> options)
            :base (options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            ///Users/Aman/Library/Mobile Documents/com~apple~CloudDocs/App_Data/Expenses.db
            //../Expenses.API/App_Data/Expenses.db
            options.UseSqlite($"Data Source=../Expenses.API/App_Data/Expenses.db",
                o => o.MigrationsAssembly("Expenses.Model"))
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            //ProductBrand tiene un product, que tiene varios ProductBrands
            //modelBuilder.Entity<ProductBrand>()
            //    .HasOne(pb => pb.Product)
            //    .WithMany(p => p.ProductBrands)
            //    .OnDelete(DeleteBehavior.SetNull); //Al borrar un producto, poner a null el product de ProductBrand
            //TODO: Comprobar si es el behavior que quiero. 
            //Cascada puede ser peligroso. Vale mas poner setNull y borrarlo manualmete para evitar problemas
        }

    }
}
