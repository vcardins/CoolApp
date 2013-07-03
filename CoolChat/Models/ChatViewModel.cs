using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoolChat.Models.Chats;

namespace CoolChat.Models
{
    public class ChatViewModel
    {
        public ChatUser ChatUser { get; set; }
        public ChatUserList ListChatUsers { get; set; }

    }
}