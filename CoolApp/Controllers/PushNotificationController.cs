using System.Web.Mvc;
using CoolApp.Core.Interfaces.External;
using CoolApp.Core.Models.Mobile;
using CoolApp.Infrastructure.Configuration;

namespace CoolApp.Controllers
{
    public class PushNotificationController : Controller
    {
        private IMobileRestAPI _mobileRestAPI;

        public JsonResult Index()
        {
            _mobileRestAPI = DependencyResolver.Current.GetService<IMobileRestAPI>();
            
            var statusConfig = AppConfig.Instance.Notifications.Statuses["Completed"];

            var result = _mobileRestAPI.SendNotification(new MobileNotification
            {
                Title = "Ticket Closing",
                Text = "Maor V. requested closing of ticket #169 to Stu S.",
                Channel = "alert",
                Icon = statusConfig.Icon,
                Vibrate = statusConfig.Vibrate,
                Sound = statusConfig.Sound
            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
