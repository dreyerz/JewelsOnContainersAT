using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class Catalog
    {

        //When you get the deserialized GET call back as a string, it's more performant to 
        //deserialize into a object and there is a direct mapping
        //Choice to make iEnumerable into a List
        //Come up with the Catalog Item model on the web side (replicate) to say it's a list of items
        //Use object oriented typesafety concept - deserialize to the right object vs parsing
        //This is equivalent to the Paginated View Model on the API side- when JSON sends the data,
        //the names of the models don't transfer with it
        //The receiving end only keeps the structure
        public int Pagesize { get; set; }
        public int PageIndex { get; set; }
        public long Count { get; set; }
        public List<CatalogItem> Data { get; set; }
    }
}
