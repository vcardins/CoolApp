using System.Collections.Generic;
using CoolChat.Core.Models;

namespace CoolChat.Core.Interfaces.Service
{
    public partial interface IChatService
    {
        bool InsertNewChat(string userFrom, string userTo, string message);
    }
}