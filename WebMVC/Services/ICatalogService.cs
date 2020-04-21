using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.Services
{
    //All the calls that need to go to my Catalog
    //What are all the contracts/methods available in this interface that my controller can call
    public interface ICatalogService
    {
        //When I make a call to this service and the service sends this call to the HTTP client
        //the HTTP client will call the microservice and get the data back in string format
        //Then I will deserialize the string into Catalog format (internally containing list of items to display in UI
        Task<Catalog> GetCatalogItemsAsync(int page, int size);
    }   
}
