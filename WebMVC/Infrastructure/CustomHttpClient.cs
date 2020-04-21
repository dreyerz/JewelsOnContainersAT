using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
    public class CustomHttpClient : IHTTPClient
    {
        //.Net Core provides an HTTP Client Class -- this is an instance of a browser behind the scenes
        //Use this call to make an API call within the backend
        //Readonly = initialized at the constructor and after that it's a cosntant after - no change
        private readonly HttpClient _client;
        
        //This client will make an HTTP Client request on our behalf - we must tell the client what
        //kind of http request to make
        public CustomHttpClient()
        {
            _client = new HttpClient(); 
        }
        public async Task<string> GetStringAsync(string uri, 
            string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            //What is my request message comprised of?
            //This is the request message
            //system.net.http --> library for HttpRequestMessage  
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            if(authorizationToken !=null)
            {
                //Do Something
            }

            var response = await _client.SendAsync(requestMessage);
            
            //Content is the JSON object, read as a string
            //Async means the method will run in a secondary thread; we must wait for the response
            //Await means we can wait for the response to come back to be returned
            return await response.Content.ReadAsStringAsync();
        }

        public Task<HttpResponseMessage> PostAsync<T>(string uri, T item, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PutAsync<T>(string uri, T item, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            throw new NotImplementedException();
        }
        public Task<HttpResponseMessage> DeleteAsync(string uri, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            throw new NotImplementedException();
        }
    }
}
