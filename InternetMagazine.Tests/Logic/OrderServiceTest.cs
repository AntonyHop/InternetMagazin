﻿using InternetMagazine.PL.DTO;
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
    public class OrderServiceTest
    {
       [TestMethod]
        public void Orsers_Get_All()
        {
            MOCKUnitOfWork muof = new MOCKUnitOfWork();
            OrderService sw = new OrderService(muof);

            IEnumerable<OrderItemDTO> cats = sw.Orders();

            Assert.IsNotNull(cats);

        }

        [TestMethod]
        public void Del_Not_Exists_Order()
        {
            MOCKUnitOfWork muof = new MOCKUnitOfWork();
            OrderService sw = new OrderService(muof);

            Exception outex = null;

            try
            {
                sw.Delete(-20);
            }
            catch (Exception ex)
            {
                outex = ex;
            }

            Assert.IsNotNull(outex);
        }

        [TestMethod]
        public void Order_Service_Serch_if_user_NF()
        {
            MOCKUnitOfWork muof = new MOCKUnitOfWork();
            OrderService sw = new OrderService(muof);

            Exception outex = null;

            try
            {
                sw.getOrdersByUserId(-5);
            }
            catch (Exception ex)
            {
                outex = ex;
            }

            Assert.IsNotNull(outex);
        }

    }
}
