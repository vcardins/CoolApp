using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using CoolChat.Core.Interfaces.Service;
using CoolChat.Models.Chats;
using Omu.ValueInjecter;

namespace CoolChat.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _userService;

        //Test Gui Communication    
        public ActionResult Index()
        {
            _userService = DependencyResolver.Current.GetService<IUserService>();
            var users = _userService.GetAll().ToList();

            List<ChatUser> chatUsers = users.Select(x => new ChatUser().InjectFrom(x)).Cast<ChatUser>().ToList();

            return View(chatUsers);
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
