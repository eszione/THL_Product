using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Product.Repositories.Implementations;
using Product.Repositories.Interfaces;
using Product.Services.Implementations;
using Product.Services.Interfaces;
using Product.Types.Constants;
using Product.Types.Models;
using Product.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace Product.API
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
            services.AddControllers();

            services.AddDbContext<ProductContext>(options => options.UseInMemoryDatabase("ProductDb"));

            services.AddTransient(typeof(IRepository<,,>), typeof(Repository<,,>));

            services.AddTransient(typeof(IProductRepository), typeof(ProductRepository));

            services.AddTransient(typeof(IProductService), typeof(ProductService));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            GenerateInMemoryData(app);
        }

        private void GenerateInMemoryData(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ProductContext>();
                var products = JsonReader.GetFromFile<IEnumerable<ProductRecord>>(FolderNames.MOCK, FileNames.PRODUCT_FILE).Result;
                if (products?.Any() ?? false)
                {
                    foreach(var product in products)
                    {
                        context.Products.Add(product);
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
