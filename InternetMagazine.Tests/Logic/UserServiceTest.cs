using InternetMagazine.PL.DTO;
using InternetMagazine.PL.Services;
using InternetMagazine.Tests.MOCK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetMagazine.Tests.Logic
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void User_Get_All()
        {
            MOCKUnitOfWork muof = new MOCKUnitOfWork();
            UserService sw = new UserService(muof);

            IEnumerable<UserDTO> cats = sw.GetUsers();

            Assert.IsNotNull(cats);

        }

        [TestMethod]
        public void Del_Not_Exists_User()
        {
            MOCKUnitOfWork muof = new MOCKUnitOfWork();
            UserService sw = new UserService(muof);

            Exception outex = null;

            try
            {
                sw.RemoveUser(-10);
            }
            catch (Exception ex)
            {
                outex = ex;
            }

            Assert.IsNotNull(outex);
        }

        [TestMethod]
        public void Edit_Not_Exists_User()
        {
            MOCKUnitOfWork muof = new MOCKUnitOfWork();
            UserService sw = new UserService(muof);

            Exception outex = null;

            try
            {
                sw.UpdateUser(new UserDTO() { FirstName = "Anton" , Id = -10, LastName = "Kukushkin", Email="stargeit@gameil.com"});
            }
            catch (Exception ex)
            {
                outex = ex;
            }

            Assert.IsNotNull(outex);
        }

        [TestMethod]
        public void Delete_Not_Exists_User()
        {
            MOCKUnitOfWork muof = new MOCKUnitOfWork();
            UserService sw = new UserService(muof);

            Exception outex = null;

            try
            {
                sw.RemoveUser(-10);
            }
            catch (Exception ex)
            {
                outex = ex;
            }

            Assert.IsNotNull(outex);
        }

        [TestMethod]
        public void Name_Not_Exists_User()
        {
            MOCKUnitOfWork muof = new MOCKUnitOfWork();
            UserService sw = new UserService(muof);

            Exception outex = null;

            try
            {
                sw.getUserByName("xchxhvxkl");
            }
            catch (Exception ex)
            {
                outex = ex;
            }

            Assert.IsNotNull(outex);
        }
    }
}
