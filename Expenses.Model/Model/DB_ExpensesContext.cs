using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Expenses.Model.Model
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
        public virtual DbSet<Label> Label { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Section> Section { get; set; }
        public virtual DbSet<Shopping> Shopping { get; set; }
        public virtual DbSet<ShoppingProduct> ShoppingProduct { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<StoreLocation> StoreLocation { get; set; }
        public virtual DbSet<TypeMeasure> TypeMeasure { get; set; }
        public virtual DbSet<TypePayment> TypePayment { get; set; }
        public virtual DbSet<TypeStore> TypeStore { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=DB_Expenses;User=sa;Password=g4st0s;Trusted_Connection=True;");
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

                entity.HasOne(d => d.IdTypePaymentNavigation)
                    .WithMany(p => p.Expense)
                    .HasForeignKey(d => d.IdTypePayment)
                    .HasConstraintName("FK_Expense_TypePayment");
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

            modelBuilder.Entity<Section>(entity =>
            {
                entity.HasKey(e => e.IdSection);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Shopping>(entity =>
            {
                entity.HasKey(e => e.IdExpense);

                entity.Property(e => e.IdExpense).ValueGeneratedNever();

                entity.HasOne(d => d.IdExpenseNavigation)
                    .WithOne(p => p.Shopping)
                    .HasForeignKey<Shopping>(d => d.IdExpense)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shopping_Expense");

                entity.HasOne(d => d.IdStoreLocationNavigation)
                    .WithMany(p => p.Shopping)
                    .HasForeignKey(d => d.IdStoreLocation)
                    .HasConstraintName("FK_Shopping_StoreLocation");
            });

            modelBuilder.Entity<ShoppingProduct>(entity =>
            {
                entity.HasKey(e => e.IdShoppingProduct);

                entity.Property(e => e.Comment).HasMaxLength(200);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdBrandNavigation)
                    .WithMany(p => p.ShoppingProduct)
                    .HasForeignKey(d => d.IdBrand)
                    .HasConstraintName("FK_ShoppingProduct_Brand");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ShoppingProduct)
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("FK_ShoppingProduct_Product");

                entity.HasOne(d => d.IdShoppingNavigation)
                    .WithMany(p => p.ShoppingProduct)
                    .HasForeignKey(d => d.IdShopping)
                    .HasConstraintName("FK_ShoppingProduct_Shopping");

                entity.HasOne(d => d.IdTypeMeasureNavigation)
                    .WithMany(p => p.ShoppingProduct)
                    .HasForeignKey(d => d.IdTypeMeasure)
                    .HasConstraintName("FK_ShoppingProduct_TypeMeasure");
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

            modelBuilder.Entity<StoreLocation>(entity =>
            {
                entity.HasKey(e => e.IdStoreLocation);

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.HasOne(d => d.IdStoreNavigation)
                    .WithMany(p => p.StoreLocation)
                    .HasForeignKey(d => d.IdStore)
                    .HasConstraintName("FK_StoreLocation_Store");
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
