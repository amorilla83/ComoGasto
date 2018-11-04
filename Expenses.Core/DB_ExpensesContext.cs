using System;
using Expenses.Model.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Expenses.Core
{
    public partial class DB_ExpensesContext : DbContext
    {
        public DB_ExpensesContext()
        {
        }

        public DB_ExpensesContext(DbContextOptions<DB_ExpensesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Expense> Expense { get; set; }
        public virtual DbSet<ExpenseItem> ExpenseItem { get; set; }
        public virtual DbSet<Favourite> Favourite { get; set; }
        public virtual DbSet<HistoPrice> HistoPrice { get; set; }
        public virtual DbSet<Label> Label { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductBrand> ProductBrand { get; set; }
        public virtual DbSet<Section> Section { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<TypeMeasure> TypeMeasure { get; set; }
        public virtual DbSet<TypePayment> TypePayment { get; set; }
        public virtual DbSet<TypeStore> TypeStore { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=DB_Expenses;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasKey(e => e.IdBrand);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.HasKey(e => e.IdExpense);

                entity.Property(e => e.Concept).HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Details).HasMaxLength(200);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.IdSectionNavigation)
                    .WithMany(p => p.Expense)
                    .HasForeignKey(d => d.IdSection)
                    .HasConstraintName("FK_Expense_Section");

                entity.HasOne(d => d.IdStoreNavigation)
                    .WithMany(p => p.Expense)
                    .HasForeignKey(d => d.IdStore)
                    .HasConstraintName("FK_Expense_Store");

                entity.HasOne(d => d.IdTypePaymentNavigation)
                    .WithMany(p => p.Expense)
                    .HasForeignKey(d => d.IdTypePayment)
                    .HasConstraintName("FK_Expense_TypePayment");
            });

            modelBuilder.Entity<ExpenseItem>(entity =>
            {
                entity.HasKey(e => e.IdExpenseItem);

                entity.Property(e => e.IdExpenseItem).ValueGeneratedNever();

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Quantity).HasMaxLength(50);

                entity.HasOne(d => d.IdExpenseNavigation)
                    .WithMany(p => p.ExpenseItem)
                    .HasForeignKey(d => d.IdExpense)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpenseItem_Expense");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ExpenseItem)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpenseItem_Product");

                entity.HasOne(d => d.IdTypeMeasureNavigation)
                    .WithMany(p => p.ExpenseItem)
                    .HasForeignKey(d => d.IdTypeMeasure)
                    .HasConstraintName("FK_ExpenseItem_TypeMeasure");
            });

            modelBuilder.Entity<Favourite>(entity =>
            {
                entity.HasKey(e => e.IdFavourite);

                entity.HasOne(d => d.IdProductBrandNavigation)
                    .WithMany(p => p.Favourite)
                    .HasForeignKey(d => d.IdProductBrand)
                    .HasConstraintName("FK_Favourite_ProductBrand");

                entity.HasOne(d => d.IdStoreNavigation)
                    .WithMany(p => p.Favourite)
                    .HasForeignKey(d => d.IdStore)
                    .HasConstraintName("FK_Favourite_Store");
            });

            modelBuilder.Entity<HistoPrice>(entity =>
            {
                entity.HasKey(e => e.IdHistoPrice);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.IdProductBrandNavigation)
                    .WithMany(p => p.HistoPrice)
                    .HasForeignKey(d => d.IdProductBrand)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HistoPrice_ProductBrand");

                entity.HasOne(d => d.IdStoreNavigation)
                    .WithMany(p => p.HistoPrice)
                    .HasForeignKey(d => d.IdStore)
                    .HasConstraintName("FK_HistoPrice_Store");
            });

            modelBuilder.Entity<Label>(entity =>
            {
                entity.HasKey(e => e.IdLabel);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdSectionNavigation)
                    .WithMany(p => p.Label)
                    .HasForeignKey(d => d.IdSection)
                    .HasConstraintName("FK_Label_Section");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct);

                entity.Property(e => e.Detail).HasMaxLength(200);

                entity.Property(e => e.Image).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.IdLabelNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.IdLabel)
                    .HasConstraintName("FK_Product_Label");
            });

            modelBuilder.Entity<ProductBrand>(entity =>
            {
                entity.HasKey(e => e.IdProductBrand);

                entity.Property(e => e.Quantity).HasMaxLength(50);

                entity.HasOne(d => d.IdBrandNavigation)
                    .WithMany(p => p.ProductBrand)
                    .HasForeignKey(d => d.IdBrand)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductBrand_Brand");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ProductBrand)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductBrand_Product");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.HasKey(e => e.IdSection);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.IdStore);

                entity.Property(e => e.Logo).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdTypeStoreNavigation)
                    .WithMany(p => p.Store)
                    .HasForeignKey(d => d.IdTypeStore)
                    .HasConstraintName("FK_Store_TypeStore");
            });

            modelBuilder.Entity<TypeMeasure>(entity =>
            {
                entity.HasKey(e => e.IdTypeMeasure);

                entity.Property(e => e.Description).HasMaxLength(50);
            });

            modelBuilder.Entity<TypePayment>(entity =>
            {
                entity.HasKey(e => e.IdTypePayment);

                entity.Property(e => e.Description).HasMaxLength(50);
            });

            modelBuilder.Entity<TypeStore>(entity =>
            {
                entity.HasKey(e => e.IdTypeStore);

                entity.Property(e => e.Name).HasMaxLength(50);
            });
        }
    }
}
