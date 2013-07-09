using System.Web.Mvc;

namespace CoolApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
       
        private bool _IsAuthenticated;
        private string _LoggedUser;

        public ActionResult Index()
        {
            _IsAuthenticated = Request.IsAuthenticated;
            if (_IsAuthenticated)
            {
                _LoggedUser = HttpContext.User.Identity.Name;
                return View("SelectApp");
            }

            return RedirectToAction("Login", "Account");
        }
       
       
    }
}
