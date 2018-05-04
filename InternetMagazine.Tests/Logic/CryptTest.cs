using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InternetMagazine.PL.BuisnessModels;
using System.Security.Cryptography;

namespace InternetMagazine.Tests.Logic
{
    [TestClass]
    public class CryptTest
    {
        [DataTestMethod]
        [DataRow("AdminRoot")]
        [DataRow("2285")]
        [DataRow("Hallosdlkfashdkfhalshdfhalsf")]

        public void CryptDectyptTest(string pass)
        {
            string source = pass;
            CryptLogic cl = new CryptLogic();

            String hash = cl.GetMd5Hash(source);
            bool s = cl.VerifyMd5Hash(source, hash);

            Assert.IsTrue(s, hash +" "+ source+" "+ s.ToString());

        }
    }
}
