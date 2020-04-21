using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductCatalogAPI.Data;
using ProductCatalogAPI.Domain;
using ProductCatalogAPI.ViewModel;

namespace ProductCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        //Turns the paramater into a global, accessible variable
        private readonly CatalogContext _context;
        private readonly IConfiguration _config;

        //Asks the startup to ingest configuration
        public CatalogController(CatalogContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        //Picture Controller API must be called - to get the list of catalog, I need to query the CatalogItems table
        //to pull all catalog items that match a particular type and brand
        //Almost all APIs should be asynchronous because if not, no way to ensure that webIU is functioning properly; multithread way
        //Otherwise, front end could be blocked from accessing the back end
        [HttpGet]
        [Route("[action]")]
        //IACtionResult is wrapped in task vs PicController because PicController will be called by this controller whereas
        //Catalogcontroller is called by the UI (picController is not)
        //pageIndex is which page the data User wants to see the data for
        //pageSize is how many items are in per page
        //[FromQuery] attribute indicates that the parameter will come from the query
        //use of Query route means you must remove the URI route
        public async Task<IActionResult> Items(
            [FromQuery]int pageIndex = 0, 
            [FromQuery]int pageSize = 6)
        {
            //LINQ query to get the total # of records in the Items Database, similar to Select Count(*) from CatalogItems
            var itemsCount = await _context.CatalogItems.LongCountAsync();

            //context is the entity core parameter; CatalogItems is the table; then we need a LINQ query
            //Need to grab data from catalogitems just for this page index and page size
            //this call happens in 2ndary thread, but we wait for the secondary thread to finish and give back the data
            //asynchronous will happen in the secondary thread
            var items = await _context.CatalogItems
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            //After getting items, we're going to replace the pictureURL via the below method and pass along the items
            items = ChangePictureUrl(items);

            var model = new PaginatedItemsViewModel<CatalogItem>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Count = itemsCount,
                Data = items
            };
            //when making an API call, you must send back a status (200 = good, 400 = bad, 500 = internal server errors)
            return Ok(model);
        }

        //Parameter passed in is the List of CatalogItems - knows to get back another set of list of items
        private List<CatalogItem> ChangePictureUrl(List<CatalogItem> items)
        {
            //From the created collection (array of catalog items), go through each, grab the pictureURL with my current domain name
            //Replace is a method on a String
            //Strings are immutable --> replacement doesn't replace original team, so whatever you get back, you must put back in the URL
            //Replace does not override, so you are explicitly overriding it
            items.ForEach(c => c.PictureUrl = c.PictureUrl.Replace("http://externalcatalogbaseurltobereplaced", _config["ExternalCatalogBaseUrl"]));

            //Now you have the items back
            return items;
        }
    }
}