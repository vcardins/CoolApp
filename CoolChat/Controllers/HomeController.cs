using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;

namespace CoolChat.Controllers
{
    public class HomeController : Controller
    {
        //Test Gui Communication    
        public ActionResult Index()
        {
            return View();
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
