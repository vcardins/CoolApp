using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CoolChat.Core.Interfaces.Service;
using CoolChat.Infraestructure.Profiles;
using CoolChat.Models;
using CoolChat.Models.Chats;
using Omu.ValueInjecter;

namespace CoolChat.Controllers
{
    public class ChatController : Controller
    {
        private IFriendshipService _frinedshipService;
        protected readonly IChatService ChatService;

        public ChatController(IChatService chatService)
        {
            ChatService = chatService;
        }

        public ActionResult Index()
        {
            return View(GetChatModel());
        }

        private IEnumerable<ChatUser> GetChatUsers()
        {
            _frinedshipService = DependencyResolver.Current.GetService<IFriendshipService>();
            var friends = _frinedshipService.GetFriendshipsByUserId(UserProfile.Current.UserId);

            var chatUsers = friends.Select(x => new ChatUser().InjectFrom((x.UserId == UserProfile.Current.UserId)?x.Friend:x.User)).Cast<ChatUser>().ToList();
            return chatUsers;
        }

        private ChatViewModel GetChatModel()
        {
            var chatUser = new ChatUser();
            chatUser.InjectFrom(UserProfile.Current);

            var chatViewModel = new ChatViewModel
            {
                ListChatUsers = new ChatUserList { ChatUsers = GetChatUsers() },
                ChatUser = chatUser
            };
            return chatViewModel;
        }


        // GET api/home/5
        public JsonResult StartSession()
        {
            return Json(new { success = true, username = User.Identity.Name, items = new List<string>() }, JsonRequestBehavior.AllowGet);
        }

        // GET api/home/5
        public JsonResult BoxSession(string chatbox)
        {
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // GET api/home/5
        public JsonResult Close ()
        {
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // GET api/home/5
        public JsonResult HeartBeat()
        {
            return Json(new { success = true, items = new List<string>() }, JsonRequestBehavior.AllowGet);
        }

        // GET api/home/5
        public JsonResult SendMessage()
        {
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

    }
}

     