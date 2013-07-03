using System.Collections.Generic;
using CoolChat.Core.Models;

namespace CoolChat.Core.Interfaces.Data
{
    public partial interface IChatRepository : IRepository<Chat>
    {		
		// Add extra datainterface methods in a partial interface
        IEnumerable<Chat> GetChats(int userId);
    }
}
