using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Data
{
    //Special class --> I am a DbContext class, I am going to provide instructions to Entity Framework Core when it's about to set the database
    //The instructions for the database configuration/setup will go here

    public class CatalogContext : DbContext
    {
        //Write the code assuming that the location will be given to you at deployment time -- through the Constructor
        //In the CatalogContext Constructor, you must ask for what you need - I need the database context information; location?
        //Info will be passed via Dependency Context Option --> from Entity Framework Core
        //When someone constructs the class, they will pass what kind of database you're using and where is the connection string
        //Whichever class requires dependency will accept the dependency as a constructor option (receiving end)
        // :base --> :@ the class level means inheritance of another class
        //:base --> :@ the constructor level; means when I receive the catalog options, I will call my base class' constructor (DbContext)
        //and pass the same options; Needed because when you inherit, you do NOT get constructors --> you get methods and properties
        //Whenever you call a constructor, an object is being created --> as part of this constructor, I can trigger base constructor
        //: can only be used at the constructor level  --> cannot be used for any other method
        //: can only be used at the constructor level  --> cannot be used for any other method
        //Passing to the base class makes (the creation of tables) it happen
        //Takes care of "Where" for the Entity framework
        public CatalogContext(DbContextOptions options)
            : base(options)
        {

        }

        //DbSet is a name for a db set or db table; makes a database table of type catalog brand
        //This is going to be a database table of type CatalogBrand --> DbContext indicates that it's an instruction for entity framework
        //3 Tables of the 3 domains --> takes care of "What" for the entity framework
        //These are table references
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<CatalogItem> CatalogItems { get; set; }

        //Entity Framework is like a pipeline and provides hooks into every layer - if you don't like the default instruction, plug into the hook and change
        //The changes = do it this way for me
        //Part of Inheritance property --> inheriting from a class can be configured for specific methods, whereas everything else carries forward
        //Use "Override" in inheritance to override the parents' behavior -- this is the hook; a way to change the parent behavior
        //When you're about to create tables, modelbuilder will create the table --> this instructs the modelbuilder how to create the tables
        //Override can also allow you to do what the parent does plus more
        //Lambda statements will allow you to write another method inline; since you won't use that method anywhere else
        //Let "e" by this table that's getting built (CatalogBrand) followed by instructions
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Define what the modelbuilder will do
            modelBuilder.Entity<CatalogBrand>(e =>
            {
                //When convert to the table, give it a name --> "Catalog Brands", the 1st instruction
                e.ToTable("CatalogBrands");

                //2nd instruction: Pick property to provide instructions --> you know e is your catalogbrand type, so all the properties are there
                //HiLo --> read notes; good practice to name the constraint so that you can change/delete the specific constraint later  
                e.Property(b => b.ID)
                .IsRequired()
                .UseHiLo("catalog_brand_hilo");

                //
                e.Property(b => b.Brand)
                .IsRequired()
                .HasMaxLength(100);
            });

            modelBuilder.Entity<CatalogType>(e =>
            {
                e.ToTable("CatalogTypes");
                e.Property(t => t.ID)
                .IsRequired()
                .UseHiLo("catalog_type_hilo");
                e.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(100);
            });

            modelBuilder.Entity<CatalogItem>(e =>
            {
                //"Catalog" is the name of the database (for Entity Framework to name the table) but internally, in code, it's CatalogItems
                e.ToTable("Catalog");
                e.Property(c => c.ID)
                .IsRequired()
                .UseHiLo("catalog_hilo");
                e.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
                e.Property(c => c.Price)
                .IsRequired();

                //Assigns the foreign key relationshp - CatalogItem has one relationship to Catalogtype, 
                //but Catalogtype has many relationships to CatalogItems
                //e variable is the perspective of the CatalogItem
                //c.Catalogtype is NOT referring to the property, it defines the relationship with the table first
                //This entity has one relationship with the Catalogtype table; which in turn has a 1:many to the table
                //.HasForeignKey 
                e.HasOne(c => c.CatalogType)
                .WithMany()
                .HasForeignKey(c => c.CatalogTypeId);

                e.HasOne(c => c.CatalogBrand)
                .WithMany()
                .HasForeignKey(c => c.CatalogBrandId);
            });
        }
    }
}
