using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
    //Infrastructure is meant to be one class for all services for reusability
    public class ApiPaths
    {
        //Class within a class - here are all the endpoints within my catalog
        //Another class (within the class) will be for Cart APIs and Order API, etc.
        public static class Catalog
        {
            //Contains the URLs - Product API is the end point, within this one of the endpoints
            //is PicController; Catalog is using the Pic - no need to expose
            //Catalog controller has one endpoint - Items
            //Service APIPaths, can you tell me the path/endpoint to get all catalog items?
            //Goal is to return a URI that tells you where the endpoint is located
            //int take = how many to display
            public static string GetAllCatalogItems(string baseUri, int page, int take)
            {
                //Expects the baseURI to have a slash at the end
                //APIpath's job is to give you an endpoint to fire back
                return $"{baseUri}items?pageIndex={page}&pageSize={take}";
            }
        }
    }
}
