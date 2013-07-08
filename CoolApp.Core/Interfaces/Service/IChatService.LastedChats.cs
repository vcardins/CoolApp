using System.Collections.Generic;
using CoolApp.Core.Models;

namespace CoolApp.Core.Interfaces.Service
{
    public partial interface IChatService
    {
        IEnumerable<Chat> GetLastedChats(string userName);
    }
}