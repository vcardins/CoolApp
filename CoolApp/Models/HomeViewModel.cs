using System.Collections.Generic;
using System.Web.Mvc;
using CoolApp.Models.Chats;

namespace CoolApp.Models
{
    public class HomeViewModel
    {

        public ChatUser ChatUser { get; set; }
        public List<SelectListItem> ListChatUsers { get; set; }

    }
}
