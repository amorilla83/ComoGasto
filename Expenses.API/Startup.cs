using AutoMapper;
using Expenses.API.Extensions;
using Expenses.Core;
using Expenses.Core.ApplicationService;
using Expenses.Core.ApplicationService.ServicesImpl;
using Expenses.Core.DomainService;
using Expenses.Infrastructure.Data;
using Expenses.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using NLog;
using System;
using System.IO;

namespace Expenses.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            /*services.AddDbContext<ExpensesContext>(
                options => options.UseInMemoryDatabase("Expenses")
                );*/
            services.ConfigureCors();
            services.ConfigureLoggerService();
            services.ConfigureSqlLiteContext(Configuration);
            services.ConfigureRepositories();
            services.ConfigureDomainServices();

            //services.AddDbContext<ExpensesContext>(options =>
            //    options.UseSqlite($"Data Source=../Expenses.API/App_Data/Expenses.db",
            //    o => o.MigrationsAssembly("Expenses.Model")));


            //Evitamos los bucles en las referencias entre objetos
            //services.AddMvc().AddJsonOptions(options => {
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //});

            services.AddControllers()
                .ConfigureApiBehaviorOptions( options =>
                {
                    //Add custom error response factory when ModelState is invalid
                    //options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.
                })
                    .AddNewtonsoftJson(x =>
                    {
                        x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ComoGasto", Version = "v1" });
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            services.AddMemoryCache();
            services.AddAutoMapper(typeof(Startup));
            //Esto lo hace solo el web api pero lo pongo como ejemplo para utilizar luego unit test
            //var serviceProvider = services.BuildServiceProvider();
            //var service = serviceProvider.GetRequiredService<IStoreService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //Configuramos la base de datos InMemory
                //using (var scope = app.ApplicationServices.CreateScope())
                //{
                //    var context = scope.ServiceProvider.GetService<ExpensesContext>();
                //    DBSeed.Seed(context);
                //}
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Como Gasto v1"));
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
