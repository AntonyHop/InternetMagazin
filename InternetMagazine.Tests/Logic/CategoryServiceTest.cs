using InternetMagazine.PL.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InternetMagazine.PL.Services;
using System.Collections.Generic;
using InternetMagazine.Tests.MOCK;
using System;

namespace InternetMagazine.Tests.Logic
{
    [TestClass]
    public class CategoryServiceTest
    {
        [TestMethod]
        public void Category_Get_All()
        {
            MOCKUnitOfWork muof = new MOCKUnitOfWork();
            CategoryService sw = new CategoryService(muof);

            IEnumerable<CategoryDTO> cats = sw.Categories();

            Assert.IsNotNull(cats);

        }

        [TestMethod]
        public void Del_Not_Exists_Category()
        {
            MOCKUnitOfWork muof = new MOCKUnitOfWork();
            CategoryService sw = new CategoryService(muof);

            Exception outex = null;

            try
            {
                sw.DeleteCategory(-10);
            }catch(Exception ex)
            {
                outex = ex;
            }

            Assert.IsNotNull(outex);
        }

        [TestMethod]
        public void Search_test()
        {
            MOCKUnitOfWork muof = new MOCKUnitOfWork();
            CategoryService sw = new CategoryService(muof);

            Exception outex = null;

            try
            {
                sw.Search("dfkgj");
            }
            catch (Exception ex)
            {
                outex = ex;
            }

            Assert.IsNotNull(outex);
        }



        [TestMethod]
        public void Del_Not_Exists_Product()
        {
            MOCKUnitOfWork muof = new MOCKUnitOfWork();
            CategoryService sw = new CategoryService(muof);

            Exception outex = null;

            try
            {
                sw.DeleteProduct(-10);

              
            }
            catch (Exception ex)
            {
                outex = ex;
            }

            Assert.IsNotNull(outex);
        }

        [TestMethod]
        public void Edit_Not_Exists_Product()
        {
            MOCKUnitOfWork muof = new MOCKUnitOfWork();
            CategoryService sw = new CategoryService(muof);

            Exception outex = null;

            try
            {
                sw.EditCategory(-10,"test");
                
            }
            catch (Exception ex)
            {
                outex = ex;
            }

            Assert.IsNotNull(outex);
        }

        [TestMethod]
        public void Get_product_Not_Exists_Product()
        {
            MOCKUnitOfWork muof = new MOCKUnitOfWork();
            CategoryService sw = new CategoryService(muof);

            Exception outex = null;

            try
            {
                sw.EditCategory(-10,"Test");

            }
            catch (Exception ex)
            {
                outex = ex;
            }

            Assert.IsNotNull(outex);
        }

        [TestMethod]
        public void Create_Product_If_name_30()
        {
            MOCKUnitOfWork muof = new MOCKUnitOfWork();
            CategoryService sw = new CategoryService(muof);
            Exception outex = null;
            string dummy = "";
            
            for(int i = 0; i < 105; i++)
            {
                dummy += "1";
            }

            try
            {
                sw.AddProduct(new ProductDTO() { Name = dummy });

            }
            catch (Exception ex)
            {
                outex = ex;
            }

            Assert.IsNotNull(outex);
        }

        [TestMethod]
        public void Get_Product_If_NOT_exists()
        {
            MOCKUnitOfWork muof = new MOCKUnitOfWork();
            CategoryService sw = new CategoryService(muof);

            Exception outex = null;

            try
            {
                sw.GetOneProduct(-30);

            }
            catch (Exception ex)
            {
                outex = ex;
            }

            Assert.IsNotNull(outex);
        }



    }
}
