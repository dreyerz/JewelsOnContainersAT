using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Data
{
    //Purpose is to populate table with sample data -- needed for this exercise, but will not be the case for production
    //Static = one global instance for all instances to use; shared memory that need not be instantiated
    //Anything in a static class must have a static method
    public static class CatalogSeed
    {
        //Create method where we pass the context of the catalog / reference to the database where you want it to populate data
        public static void Seed(CatalogContext context)
        {
            //Before seeding the table with data, check if there are migrations available and ready to write the schema
            //Takes care of the update database command - pushes the translated code that needs to be migrated
            //Push command that checks if there are migrations needed
            context.Database.Migrate();

            //Populate the tables with data (catalog brands, types, and items) only if there is no data
            //Be sure to check if there's data existing
            //Are there any records in the table? !context --> if there are no rows in the table
            if (!context.CatalogBrands.Any())
            {
                //References catalog brands table; add range of rows via AddRange()
                //AddRange = if you need 10 brands, you need 10 rows; or call another function --> GetPreConfiguredCatalogBrands
                //GetPreConf... method below that will contain all the catalog brands you want to add in the table
                //This is NOT a delegate because you're executing the method inn place --> firing/executing the method inline
                //When you write a record into the table, you MUST save the changes, otherwise it won't be committed (unlike collections)
                context.CatalogBrands.AddRange(GetPreConfiguredCatalogBrands());
                context.SaveChanges();
            }

            if (!context.CatalogTypes.Any())
            {
                context.CatalogTypes.AddRange(GetPreconfiguredCatalogTypes());
                context.SaveChanges();
            }

            if (!context.CatalogItems.Any())
            {
                context.CatalogItems.AddRange(GetPreConfiguredCatalogTypes());
                context.SaveChanges();
            }
        
        }
        //Static class, method must return value that feeds into AddRange for CatalogBrands --> requires CatalogBrand array
        //IEnumerable = read only collection
        private static IEnumerable<CatalogBrand> GetPreConfiguredCatalogBrands()
        {
            return new List<CatalogBrand>
            {
                //Where object initialization is --> create new list and include all the brands; while creating also populating vs
                //splitting into multiple lines of code
                //These are individual brands being added to the list
                new CatalogBrand{Brand = "Tiffany & Co"},
                new CatalogBrand{Brand = "DeBeers"},
                new CatalogBrand{Brand = "Graff"}
            };
        }

        private static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
        {
            return new List<CatalogType>
            {
                new CatalogType {Type = "Engagement Ring"},
                new CatalogType {Type= "Wedding Ring"},
                new CatalogType {Type = "Fashion Ring"}
            };
        }

        private static IEnumerable<CatalogItem> GetPreConfiguredCatalogTypes()
        {
            //Items needs to know where the pictures are coming from (pictureURL)
            //CatalogtypeID is a foreign key relationship, one of the types to have a value of 2; CatalogBrand to be 3
            return new List<CatalogItem>()
            {
                //This is for demo purposes only; we would never do this in production
                new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 3, Description = "A ring that has been around for over 100 years", Name = "World Star", Price = 199.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/1" },
                new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 2, Description = "will make you world champions", Name = "White Line", Price = 88.50M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/2" },
                new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 3, Description = "You have already won gold medal", Name = "Prism White", Price = 129, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/3" },
                new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 2, Description = "Olympic runner", Name = "Foundation Hitech", Price = 12, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/4" },
                new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 1, Description = "Roslyn Red Sheet", Name = "Roslyn White", Price = 188.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/5" },
                new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 2, Description = "Lala Land", Name = "Blue Star", Price = 112, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/6" },
                new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 1, Description = "High in the sky", Name = "Roslyn Green", Price = 212, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/7" },
                new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 1, Description = "Light as carbon", Name = "Deep Purple", Price = 238.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/8" },
                new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 2, Description = "High Jumper", Name = "Antique Ring", Price = 129, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/9" },
                new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 3, Description = "Dunker", Name = "Elequent", Price = 12, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/10" },
                new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 2, Description = "All round", Name = "Inredeible", Price = 248.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/11" },
                new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 1, Description = "Pricesless", Name = "London Sky", Price = 412, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/12" },
                new CatalogItem { CatalogTypeId = 3, CatalogBrandId = 3, Description = "You ar ethe star", Name = "Elequent", Price = 123, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/13" },
                new CatalogItem { CatalogTypeId = 3, CatalogBrandId = 2, Description = "A ring popular in the 16th and 17th century in Western Europe that was used as an engagement wedding ring", Name = "London Star", Price = 218.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/14" },
                new CatalogItem { CatalogTypeId = 3, CatalogBrandId = 1, Description = "A floppy, bendable ring made out of links of metal", Name = "Paris Blues", Price = 312, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/15" }
            };
        }
    }
}
