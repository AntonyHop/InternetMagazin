using InternetMagazine.PL.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InternetMagazine.PL.Services;
using InternetMagazine.DAL.Interfaces;
using System.Collections.Generic;
using Moq;
using InternetMagazine.DAL.EF;
using System.Data.Entity;
using InternetMagazine.DAL.Entities;
using System;
using InternetMagazine.Tests.MOCK;
using InternetMagazine.PL.Interfaces;

namespace InternetMagazine.Tests.Logic
{
    [TestClass]
    public class CategoryServiceTest
    {

      
        [TestMethod]
        public void Country_Get_All()
        {
            MOCKUnitOfWork muof = new MOCKUnitOfWork();

            

            Assert.IsNotNull(cat);

        }



    }
}
