using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Expenses.Core.ApplicationService;
using Expenses.Core.ApplicationService.ServicesImpl;
using Expenses.Core.DomainService;
using Expenses.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;

namespace Expenses.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Aquí se debe añadir la configuración del log que estará definido en service extensions
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductBrandRepository, ProductBrandRepository>();
            services.AddScoped<IProductBrandService, ProductBrandService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Esto lo hace solo el web api pero lo pongo como ejemplo para utilizar luego unit test
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IStoreService>();
        }
   
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //De momento lo comentamos para las pruebas
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
