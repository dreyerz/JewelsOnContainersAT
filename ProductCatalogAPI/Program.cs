using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductCatalogAPI.Data;

namespace ProductCatalogAPI
{
    public class Program
    {
        //Main is the starting point for any application
        public static void Main(string[] args)
        {
            //Creates the host and calls the starter file --> sets up all the services and runs it
            //Wants to separate the build and run so that we are able to injerect and seed our data; when the application runs, we ensure that data is there
            //Asks the host to get built and calls the startup
            var host = CreateHostBuilder(args).Build();

            //Need to ensure that catalogcontext service is up and running before seeding data
            //Scope asks for how each of the services are doing
            using(var scope = host.Services.CreateScope())
            {
                //If there are 5 services getting start up, and we are only interested in one specifc service, we can call it out via scope
                //Ask the scope, provide me the name of the service provider 
                //For each services, ask for each one of the providers
                var serviceProviders = scope.ServiceProvider;

                //Out of all the service providers, we are looking for one - looking for a specific provider
                //Provider, can you tell me if the catalog context is running; i.e., can you get a reference to my context
                //Gives me the pointer to the database
                var context = serviceProviders.GetRequiredService<CatalogContext>();

                //Once it's available, we can fire the seed method, which requires the context parameter --> pointer of where the database is
                CatalogSeed.Seed(context);
            }

            //Now we have to add the run; in real production this is not needed because we will not need to seed the data
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
