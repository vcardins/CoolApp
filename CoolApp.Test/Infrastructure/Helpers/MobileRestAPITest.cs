using System.Web.Mvc;
using CoolApp.Core.Interfaces.External;
using CoolApp.Core.Models.Mobile;
using NUnit.Framework;
using CoolApp.Infrastructure.Configuration;

namespace CoolApp.Test.Infrastructure.Helpers
{
    [TestFixture]
    public class MobileRestAPITest
    {
        private IMobileRestAPI _mobileRestAPI;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            _mobileRestAPI = DependencyResolver.Current.GetService<IMobileRestAPI>();
        }        

        [Test]
        public void FormDataPostCall()
        {
            _mobileRestAPI = DependencyResolver.Current.GetService<IMobileRestAPI>();
            var result = _mobileRestAPI.SendNotification(new MobileNotification
            {
                Title = "Ticket Closing",
                Text = "Maor V. requested closing of ticket #169 to Stu S.",
                Channel = "alert",
                Icon = "",
                Vibrate = true,
                Sound = "default"
            });     
        }
    }
}
