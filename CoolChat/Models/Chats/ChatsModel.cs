using System.Collections.Generic;
using CoolChat.Core.Models;

namespace CoolChat.Models.Chats
{
    public class ChatsModel
    {
        
        public int ChatId { get; set; }

        public int UserFromId { get; set; }

        public int UserToId { get; set; }

        public IEnumerable<Chat> Chats { get; set; }

        public string Message { get; set; }

    }
}