using System.Collections.Generic;
using System.Web.Mvc;
using CoolChat.Models.Chats;

namespace CoolChat.Models
{
    public class HomeViewModel
    {

        public ChatUser ChatUser { get; set; }
        public List<SelectListItem> ListChatUsers { get; set; }

    }
}
