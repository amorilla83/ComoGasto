using System;
using Expenses.Contracts;
using Expenses.Core;
using Expenses.Core.ApplicationService;
using Expenses.Core.ApplicationService.ServicesImpl;
using Expenses.Core.DomainService;
using Expenses.Infrastructure.Data;
using Expenses.Infrastructure.Data.Repository;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Expenses.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            //Necesitamos el cors para poder utilizar Angular
            //Se podrían hacer limitaciones en el origen, métodos o cabeceras
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureLoggerService (this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureSqlLiteContext (this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("SQLLite");
            services.AddDbContext<ExpensesContext> (options =>
                options.UseSqlite(connectionString,
                o => o.MigrationsAssembly("Expenses.Model")));
        }

        public static void ConfigureRepositories (this IServiceCollection services)
        {
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IFormatRepository, FormatRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IProductDetailsRepository, ProductDetailsRepository>();
            services.AddScoped<IProductPurchaseRepository, ProductPurchaseRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void ConfigureDomainServices (this IServiceCollection services)
        {
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IFormatService, FormatService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IProductDetailsService, ProductDetailsService>();
        }
    }
}
