using System;
using System.Data.Entity;
using InternetMagazine.DAL.Entities;

namespace InternetMagazine.DAL.EF
{
    public class EFContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
      

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
            context.Users.Add(new User { NickName = "Admin", Password= "338801db67c8ef85b413aecf10f2cacf", Age=21, FirstName="Anton" });//AdminRoot
            context.SaveChanges();

            context.Categories.Add(new Category { Name = "Без Ктегории" });
            context.Categories.Add(new Category { Name = "Пастилки" });
            context.Categories.Add(new Category { Name = "Спрей" });
            context.Categories.Add(new Category { Name = "Таблетки" });
            context.SaveChanges();

            context.Products.Add(new Product { Name="Доктор Мом", Desc="Просто таблетки которые пастилки",CategoryId = 2,Price=200M });
            context.Products.Add(new Product { Name = "Стрепсилс", Desc = "Просто таблетки которые пастилки", CategoryId = 2, Price = 250M });
            context.SaveChanges();

        }
    }

}
