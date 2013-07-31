using System.Web.Mvc;
using CoolApp.Core.Interfaces.External;
using CoolApp.Core.Models.Mobile;

namespace CoolApp.Controllers
{
    public class PushNotificationController : Controller
    {
        private IMobileRestAPI _mobileRestAPI;

        public JsonResult Index()
        {
            _mobileRestAPI = DependencyResolver.Current.GetService<IMobileRestAPI>();

            _mobileRestAPI.SendNotification(new MobileNotification
            {
                Title = "Ticket Closing",
                Text = "Maor V. requested closing of ticket #169 to Stu S.",
                Channel = "alert"
            });    

            return Json(true);
        }

    }
}
