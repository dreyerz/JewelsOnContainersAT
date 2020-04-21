using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class CatalogController : Controller
    {
        //This gets the dependency injection and is stored in a local variable for use
        private readonly ICatalogService _service;
        public CatalogController(ICatalogService service)
        {
            _service = service;
        }
        //Default index action comes from index.js used before
        //Page is what is sent back to the client
        //Depending on controller you're calling from (action, index), it locates sa page in the views folder
        //I will look for a folder that matches the controller name
        //No folder = no view
        //Within folder, it will look for html page called index
        public async Task<IActionResult> Index(int page)
        {
            //Make the call to the service
            var itemsOnPage = 10;

            var catalog = await _service.GetCatalogItemsAsync(page, itemsOnPage);
            return View(catalog);
        }
    }
}