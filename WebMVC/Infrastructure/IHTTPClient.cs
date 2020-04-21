using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
    //HTTPClient defines how communication happens between service and microservice world
    //Come up with a contract for HTTP Client to go do
    //GET = asking to give the data; POST = server must create new data; PUT = edit/change data
    //HTTPClient must be written in a way to support all these services (Get, Post, Delete, Put)
    //All interfaces are by default public
    public interface IHTTPClient
    {
        //Client that will make HTTP requests on user behalf
        //This is a get request - JSON is a string
        //Wrap the return in a TASK which is a thread
        //uri is required, other two parameters are optional
        Task<string> GetStringAsync(string uri, string authorizationToken = null, string authorizationMethod = "Bearer");


        //Supports any post request from the user --> allows users to post a new product; client can get the info provided from the user and write it into the database
        //Post request - Client sends data
        //HTTPResponse = response from system of the status - call was good, internal server error, not found, etc. Response method gives you the status of whether it was good or badf
        //To be used as a general service --> Post call can write any data (order, cart, catalog, etc.) --> call should be written in generics so it can be used by all
        //Based on the type of service calls you, you can accept the data. This is a TYPE definition.
        //Parameter includes data of type T; caller will tell this method what type of data they are going to post and that's the data to be passed to the endpoint
        Task<HttpResponseMessage> PostAsync<T>(string uri, T item, string authorizationToken = null, string authorizationMethod = "Bearer");

        //Put method signature is the same, but the method implementation is slightly different

        Task<HttpResponseMessage> PutAsync<T>(string uri, T item, string authorizationToken = null, string authorizationMethod = "Bearer");

        Task<HttpResponseMessage> DeleteAsync(string uri, string authorizationToken = null, string authorizationMethod = "Bearer");
    }
}
