using System;
using System.Data.Entity;
using InternetMagazine.DAL.Entities;

namespace InternetMagazine.DAL.EF
{
    public class EFContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public EFContext(string connection) : base(connection)
        {   
        }

        static EFContext()
        {
            Database.SetInitializer<EFContext>(new DataInitalizer());
        }
    }

    public class DataInitalizer : DropCreateDatabaseIfModelChanges<EFContext> {
        protected override void Seed(EFContext context)
        {
            context.Categories.Add(new Category { Name = "Без Ктегории" });
            context.Categories.Add(new Category { Name = "Фантастика" });

            context.Products.Add(new Product { Name="Метро 2033", Desc="Постапокалиптический роман",CategoryId = 2,Price=200M });
            context.Products.Add(new Product { Name = "Метро 2034", Desc = "Постапокалиптический роман", CategoryId = 2, Price = 250M });
            context.SaveChanges();

        }
    }

}
