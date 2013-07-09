using System.Collections.Generic;
using CoolApp.Core.Models;

namespace CoolApp.Core.Interfaces.Service
{
    public partial interface IChatService : IService<Chat>
    {
		// Add extra serviceinterface methods in a partial interface
        IEnumerable<Chat> GetChats(int id);
    }
}