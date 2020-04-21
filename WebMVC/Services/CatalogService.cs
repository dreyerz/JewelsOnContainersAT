using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Infrastructure;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class CatalogService : ICatalogService
    {
        //BaseURI will come from a configuration as an environment variable in docker container
        //Configuration will be passed by the startup injection
        private readonly string _baseUri;
        private readonly IHTTPClient _client;
        //Remember: Parameters for this constructor are the depenedency injections
        public CatalogService(IConfiguration config, IHTTPClient client)
        {
            _baseUri = $"{config["CatalogUrl"]}/api/catalog";
            _client = client;
        }

        public async Task<Catalog> GetCatalogItemsAsync(int page, int size)
        {
            //This gets the URI
            var CatalogItemsUri = ApiPaths.Catalog.GetAllCatalogItems(_baseUri, page, size);

            //Next send the URI and pass it to the custom HTTP client implementation
            //Remember, you don't want to bind directly to custom HTTP client - talk to interface
            //Don't bind to a specific implementation, loses the whole point of the interface
            //Startup will you who the implemented of httpclient is

            var datastring = await _client.GetStringAsync(CatalogItemsUri);
            
            //Controller will get a deserialized object coming back; this can be bound to display pages
            return JsonConvert.DeserializeObject<Catalog>(datastring);


        }
    }
}
