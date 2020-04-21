using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicController : ControllerBase
    {
        //First method is a get image method

        //Global variable of the class; can be accessed by the GetImage method
        private readonly IWebHostEnvironment _env;

        //Constructor asks for what we need to be injected --> the host environment; where is this service hosted? IHosting Environment
        public PicController(IWebHostEnvironment env)
        {
            //No need to know where it is, just tell me where it's hosted and computer will read off it
            //Note, the env variable is local to this constructor so how can the GetImage method access the env variable?
            //Store the env variable globally
            _env = env;
        }
        //Routes can be a "get" route or a "post route"
        [HttpGet("{id}")]
        public IActionResult GetImage(int id)
        {
            //This stores the path to the wwwroot folder path
            var webRoot = _env.WebRootPath;

            //Get to the pics folder with ring
            //Sending the direct path to client will not work because it will then try to find the path in the local box
            var path = Path.Combine($"{webRoot}/Pics/", $"Ring{id}.jpg");

            //Our goal is to send the actual image file - reading all bytes actually reads the image; image data in the form of a byte
            var buffer = System.IO.File.ReadAllBytes(path);

            //Returns the file back to the client (Controller based file); send it as a File/content from buffer and indicate the type of the file (jpeg)
            return File(buffer, "image/jpeg");
        }
    }
}