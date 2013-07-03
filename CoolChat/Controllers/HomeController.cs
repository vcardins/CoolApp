using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using CoolChat.Models;

namespace CoolChat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(AccountModel model)
        {
            FormsAuthentication.SetAuthCookie(model.AccountName, true);
            return RedirectToAction("Chat", model);
        }

        public ActionResult Chat(AccountModel model)
        {
            return View(model);
        }

        // GET api/home
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/home/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/home
        public void Post([FromBody]string value)
        {
        }

        // PUT api/home/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/home/5
        public void Delete(int id)
        {
        }
    }
}
