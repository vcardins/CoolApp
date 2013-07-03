using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using CoolChat.Core.Interfaces.Service;
using CoolChat.Models;
using CoolChat.Models.Chats;
using System.Linq;

namespace CoolChat.Controllers
{
    public class HomeController : Controller
    {
        protected readonly IUserService UserService;

        public HomeController(IUserService userService)
        {
            UserService = userService;
        }

        public ActionResult Index()
        {
            var users = UserService.GetAllReadOnly().Select(x => new SelectListItem{Selected = false, Text = x.FirstName, Value = x.Username}).ToList();
            var homeViewModel = new HomeViewModel {ListChatUsers = users, ChatUser = new ChatUser()};
            return View(homeViewModel);
        }

        public ActionResult Login(HomeViewModel model)
        {
            FormsAuthentication.SetAuthCookie(model.ChatUser.Username, true);
            return RedirectToAction("Chat", model);
        }

        public ActionResult Chat()
        {
            var users = UserService.GetAllReadOnly().Select(x => new ChatUser { Username = x.Username, UserId = x.UserId, PhotoFile = x.PhotoFile, DisplayName = x.DisplayName}).ToList();
            var chatUser = new ChatUser {Username = HttpContext.User.Identity.Name};
            var chatViewModel = new ChatViewModel { ListChatUsers = new ChatUserList{ChatUsers =  users}, ChatUser = chatUser };
            return View(chatViewModel);
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
