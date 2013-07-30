using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoolApp.Core.Interfaces.External;

namespace CoolApp.Controllers
{
    public class PushNotificationController : Controller
    {
        private IMobileRestAPI _mobileRestAPI;

        public JsonResult Index()
        {
            _mobileRestAPI = DependencyResolver.Current.GetService<IMobileRestAPI>();

            _mobileRestAPI.SendNotification("Ticket Closing", "Maor V. requested closing of ticket #169 to Stu S.", "alert", "", "");    

            return Json(true);
        }

    }
}
