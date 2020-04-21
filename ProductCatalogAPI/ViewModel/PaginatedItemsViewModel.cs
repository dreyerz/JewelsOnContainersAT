using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.ViewModel
{
    //This is supposed to state all the data that the page needs
    //T entity must be a class --> meaning it must be a reference type (vs struct value type); 
    //This is how you make the class generic
    //Without the class rule, then you can pass an integer or a string
    public class PaginatedItemsViewModel<T>
        where T :class
    {
        public int PageSize { get; set; }
        //which page you're retrieving
        public int PageIndex { get; set; }
        public long Count { get; set; }

        //This means you don't know what the type will be, but the user will definte it
        public IEnumerable<T> Data { get; set; }

    }
}
