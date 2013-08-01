using System.Web.Mvc;
using CoolApp.Core.Interfaces.External;
using CoolApp.Core.Models.Mobile;
using NUnit.Framework;

namespace CoolApp.Test.Infrastructure.Helpers
{
    [TestFixture]
    public class MobileRestAPITest
    {
        private IMobileRestAPI _mobileRestAPI;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            DependencyResolution.AppStartup.Start();
            _mobileRestAPI = DependencyResolver.Current.GetService<IMobileRestAPI>();
        }

        [TestFixtureTearDown]
        public void TestTearDown()
        {
            DependencyResolution.AppStartup.Stop();
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
