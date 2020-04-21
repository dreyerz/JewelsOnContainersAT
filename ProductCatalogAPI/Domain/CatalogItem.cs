using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Domain
{
    public class CatalogItem
    {
        //What are all the things needed associated with this item - Identify the core properties
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        
        //Foreign key representation to other domains; "navigational property" that relates this domain to the other domain
        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }

        //From the Catalog Item, navigate to the catalog type through this complex property
        //Composition = putting together pieces to make a bigger object (not inheriting from the Catalogtype)
        //Addition of "virtual" keyword makes it navigational --> virtual connection and does not physically take up space; virtually there to navigate from the catalog item
        public virtual CatalogType CatalogType { get; set; }
        public virtual CatalogBrand CatalogBrand { get; set; }



    }
}
