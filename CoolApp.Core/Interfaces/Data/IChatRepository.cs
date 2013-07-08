using System.Collections.Generic;
using CoolApp.Core.Models;

namespace CoolApp.Core.Interfaces.Data
{
    public partial interface IChatRepository : IRepository<Chat>
    {		
		// Add extra datainterface methods in a partial interface
        IEnumerable<Chat> GetChats(int userId);
    }
}
