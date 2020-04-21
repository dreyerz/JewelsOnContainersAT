using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductCatalogAPI.Data;

namespace ProductCatalogAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //Configuration ==> means the jSON file         
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //This configures your microservices
        public void ConfigureServices(IServiceCollection services)
        {
            //This is where we instantiate the controllers
            services.AddControllers();

            //Switch to using Docker; this call reads from the Environment Variable in the Docker Compose YML
            //IIS = appsettingsjson; Docker = Docker Compose YML
            var server = Configuration["DatabaseServer"];
            var database = Configuration["DatabaseName"];
            var user = Configuration["DatabaseUser"];
            var password = Configuration["DatabasePassword"];
            var connectionString = $"Server={server};Database={database};User Id = {user};Password={ password}";
            services.AddDbContext<CatalogContext>(options => options.UseSqlServer(connectionString));


            //Adds DBContext into the services
            //This means throughout the service, any time there is a call for a catalog context, here is where to get it from
            //Startup creates an instance of the CatalogContext; this is adding the dependency - now will be connected to physical database
            //IISExpress DBcontext line --> services.AddDbContext<CatalogContext>(options => options.UseSqlServer(Configuration["ConnectionString"]));

            //After table is set up, call the seed class to populate the table --> pass the context
            //All of these configurations run asynchronously
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
