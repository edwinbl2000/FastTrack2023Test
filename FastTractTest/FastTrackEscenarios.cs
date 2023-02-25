using Microsoft.VisualStudio.TestTools.UnitTesting;
using APITest;
using static System.Net.WebRequestMethods;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace FastTractTest
{
    [TestClass]
    public class FastTrackEscenarios
    {
        APISelenium selenium = new APISelenium();
        ReportFunctions RP = new ReportFunctions();

        public static IEnumerable<object[]> AdditionDataRegister
        {
            get
            {
                return new[]
                {
                    new object[] { "fastrack","https://demo.ft-crm.com/","egbarreto00000501@gmail.com","egbarreto","+356","99970876","edwin barreto"},
                    new object[] { "fastrack","https://demo.ft-crm.com/","egbarreto00000502@gmail.com","egbarreto","+356","99970876","edwin barreto"},
                };
            }
        }

        public static IEnumerable<object[]> AdditionDataLogin
        {
            get
            {
                return new[]
                {
                    new object[] { "fastrack","https://demo.ft-crm.com/", "egbarreto00000501@gmail.com" },
                    new object[] { "fastrack","https://demo.ft-crm.com/", "egbarreto00000502@gmail.com" },
                };
            }
        }

        [TestMethod]
        [DynamicData(nameof(AdditionDataRegister))]
        public void A_User_Register(string app, string url, string user, string pass, string indindicativenumberPhone, string numberPhone, string name)
        {
            int asserRegister = 0;
            string file = RP.CreteDocumentWordDinamic("A_User_Register");

            asserRegister =selenium.RegisterApp(app, user, pass, url, file, indindicativenumberPhone, numberPhone, name);

            Assert.IsTrue(asserRegister > 0);
    
        }

        [TestMethod]
        [DynamicData(nameof(AdditionDataLogin))]
        public void B_User_LoginAcces(string app, string url, string user)
        {
            Random rnd = new Random();
            int asserLogin = 0;         
            string file = RP.CreteDocumentWordDinamic("B_User_LoginAcces");

            asserLogin = selenium.LoginApps(app, user, url, file);

            Assert.IsTrue(asserLogin > 0);
        }

        [TestMethod]
        [DynamicData(nameof(AdditionDataLogin))]
        public void C_Deposit_Check_Balance(string app, string url, string user)
        {
            Random rnd = new Random();
            int asserLogin = 0;
            int asserChkBalance = 0;
            string file = RP.CreteDocumentWordDinamic("C_Deposit_Check_Balance");

            asserLogin = selenium.LoginApps(app, user, url, file);
            asserChkBalance = selenium.CheckBalance(app, file);

            Assert.IsTrue(asserLogin > 0 && asserChkBalance == 1);

        }

        [TestMethod]
        [DynamicData(nameof(AdditionDataLogin))]
        public void D_Play_Game_Update_Balance(string app, string url, string user)
        {
            Random rnd = new Random();
            int asserLogin = 0;
            int asserChkBalance = 0;
            string file = RP.CreteDocumentWordDinamic("D_Play_Game_Update_Balance");

            asserLogin = selenium.LoginApps(app, user, url, file);
            asserChkBalance = selenium.PlayGameUpdateBalance(app, file);

            Assert.IsTrue(asserLogin > 0 && asserChkBalance == 1);

        }

        [TestMethod]
        [DynamicData(nameof(AdditionDataLogin))]
        public void E_Buy_Lotery_Update_Balance(string app, string url, string user)
        {
            Random rnd = new Random();
            int asserLogin = 0;
            int asserUdpBalance = 0;
            string file = RP.CreteDocumentWordDinamic("E_Buy_Lotery_Update_Balance");

            asserLogin = selenium.LoginApps(app, user, url, file);
            asserUdpBalance = selenium.BuyLoteryUpdateBalance(app, file);

            Assert.IsTrue(asserLogin > 0 && asserUdpBalance == 1);
        }

        [TestCleanup]
        public void Clean()
        {
            selenium.Close();
            selenium.Quit();
            selenium.Dispose();
            RP.CleanProcess();

            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
            int numfiles = di.GetFiles("*.png", SearchOption.AllDirectories).Length;

            if (numfiles > 0)
            {
                foreach (FileInfo file in di.GetFiles("*.png", SearchOption.AllDirectories))
                {
                    file.Delete();
                }
            }

        }
    }
}
