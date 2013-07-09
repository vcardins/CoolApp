using System.Collections.Generic;
using CoolApp.Core.Models;

namespace CoolApp.Models.Chats
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