using CoolChat.Core.Interfaces.Data;
using CoolChat.Core.Models;

namespace CoolChat.Infraestructure.Data
{
    public partial class FriendshipRepository : BaseRepository<Friendship>, IFriendshipRepository
    {
		public FriendshipRepository(IDatabaseFactory databaseFactory)
	        : base(databaseFactory)
	    {
	    }

      
    }
}