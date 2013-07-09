using System.Collections.Generic;
using CoolApp.Core.Models;

namespace CoolApp.Core.Interfaces.Service
{
    public partial interface IChatService
    {
        bool InsertNewChat(string userFrom, string userTo, string message);
    }
}