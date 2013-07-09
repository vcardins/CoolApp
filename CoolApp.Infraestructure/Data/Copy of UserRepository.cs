using CoolApp.Core.Interfaces.Data;
using CoolApp.Core.Models;

namespace CoolApp.Infraestructure.Data
{
    public partial class FriendshipRepository : BaseRepository<Friendship>, IFriendshipRepository
    {
		public FriendshipRepository(IDatabaseFactory databaseFactory)
	        : base(databaseFactory)
	    {
	    }

      
    }
}