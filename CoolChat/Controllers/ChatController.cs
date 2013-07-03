using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using CoolChat.Core.Interfaces.Service;
using CoolChat.Core.Models;
using CoolChat.Infraestructure.Profiles;
using CoolChat.Models.Chats;

namespace CoolChat.Controllers
{
    public class ChatController : Controller
    {

        protected readonly IChatService ChatService;

        public ChatController(IChatService chatService)
        {
            ChatService = chatService;
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Index(int id)
        {

            IEnumerable<Chat> chats = ChatService.GetChats(id);

            var chatsModel = new ChatsModel
            {
                Chats = chats,
                UserFromId = UserProfile.Current.UserId,
                UserToId = id
            };

            return View(chatsModel);
        }

        // GET api/home
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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

     