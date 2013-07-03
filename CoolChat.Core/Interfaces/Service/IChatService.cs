using System.Collections.Generic;
using CoolChat.Core.Models;

namespace CoolChat.Core.Interfaces.Service
{
    public partial interface IChatService : IService<Chat>
    {
		// Add extra serviceinterface methods in a partial interface
        IEnumerable<Chat> GetChats(int id);
    }
}