using System;
using System.Web.Mvc;
using CoolApp.Core.Interfaces.External;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoolApp.Test.Infrastructure.Helpers
{
    [TestClass]
    public class MobileRestAPITest
    {
        private IMobileRestAPI _mobileRestAPI;

        public void TestSetup()
        {
            _mobileRestAPI = DependencyResolver.Current.GetService<IMobileRestAPI>();
        }        

        [TestMethod]
        public void FormDataPostCall()
        {
            _mobileRestAPI.SendNotification("Ticket Closing", "Maor V. requested closing of ticket #169 to Stu S.", "alert", "", "");       
        }
    }
}
