using System;
using System.Data.Entity;
using InternetMagazine.DAL.Entities;

namespace InternetMagazine.DAL.EF
{
    public class EFContext:DbContext
    {
        public DbSet<Event> Products { get; set; }
        public DbSet<Room> Categories { get; set; }
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
            context.Users.Add(new User { NickName = "Admin", Password= "3b2077ec209a4a5d5b0d3c7d154e4cc5",Age=21, FirstName="Anton" });//AdminRoot
            context.SaveChanges();

            context.Categories.Add(new Room { Name = "Главная" });
            context.Categories.Add(new Room { Name = "Библиотека" });
            context.Categories.Add(new Room { Name = "Лаборатория" });
            context.SaveChanges();

            context.Products.Add(new Event { Name="Метро 2033", Desc= "Метро 2033 Постапокалиптический роман Обсуждение ", CategoryId = 1,Price=200M });
            context.Products.Add(new Event { Name = "Метро 2034 ", Desc = "Метро 2034 Постапокалиптический роман Обсуждение", CategoryId = 1, Price = 250M });
            context.SaveChanges();
        }
    }

}
