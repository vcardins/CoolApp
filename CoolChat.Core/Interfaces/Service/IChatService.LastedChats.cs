using System.Collections.Generic;
using CoolChat.Core.Models;

namespace CoolChat.Core.Interfaces.Service
{
    public partial interface IChatService
    {
        IEnumerable<Chat> GetLastedChats(string userName);
    }
}