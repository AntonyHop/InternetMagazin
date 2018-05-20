using InternetMagazine.DAL.Interfaces;
using System;
using System.Collections.Generic;
using InternetMagazine.DAL.Entities;
using Moq;

namespace InternetMagazine.Tests.MOCK
{
    class MOCKUnitOfWork : IUnitOfWork
    {
       
        private IGenericRepository<Category> _categories;
        private IGenericRepository<Product> _products;
        private IGenericRepository<User> _users;
        private IGenericRepository<Order> _orders;

        public List<Category> cat = new List<Category>();
        public List<Order> or = new List<Order>();

        public List<User> us = new List<User>();
        public List<Product> pr = new List<Product>();


        public MOCKUnitOfWork()
        {
            cat.Add(new Category() { Id = 1, Name = "Test1" });
            cat.Add(new Category() { Id = 2, Name = "Test2" });

            or.Add(new Order() { Id = 1, Price = 5, Count = 1 });
            or.Add(new Order() { Id = 2, Price = 200, Count=10 });

            us.Add(new User { NickName = "Admin1", Password = "3b2077ec209a4a5d5b0d3c7d154e4cc5", Age = 21, FirstName = "Anton" });
            us.Add(new User { NickName = "Admin2", Password = "3b2077ec209a4a5d5b0d3c7d154e4cc5", Age = 21, FirstName = "Anton" });

            pr.Add(new Product { Name = "Метро 2033", Desc = "Постапокалиптический роман", CategoryId = 2, Price = 200M });
            pr.Add(new Product { Name = "Метро 2034", Desc = "Постапокалиптический роман", CategoryId = 2, Price = 250M });
        }

        public IGenericRepository<Category> Categories
        {
            get
            {
                if (_categories == null)
                {
                    var stub = new Mock<IGenericRepository<Category>>();
                    stub.Setup(ld => ld.Get()).Returns(cat);

                    _categories = stub.Object;

                  
                }
                return _categories;
            }
        }

        public IGenericRepository<Product> Products
        {
            get
            {
                if (_products == null)
                {
                    var stub = new Mock<IGenericRepository<Product>>();
                    stub.Setup(ld => ld.Get()).Returns(pr);

                    _products = stub.Object;

                }
                return _products;
            }



        }

        public IGenericRepository<User> Users
        {
            get
            {
                if (_users == null)
                {
                    var stub = new Mock<IGenericRepository<User>>();
                    stub.Setup(ld => ld.Get()).Returns(us);

                    _users = stub.Object;

                }
                return _users;
            }
        }

        public IGenericRepository<Order> Orders
        {
            get
            {
                if (_orders == null)
                {
                    var stub = new Mock<IGenericRepository<Order>>();
                    stub.Setup(ld => ld.Get()).Returns(or);

                    _orders = stub.Object;

                }
                return _orders;
            }
        }



        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
