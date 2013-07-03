using CoolChat.Core.Interfaces.Data;
using CoolChat.Core.Models;

namespace CoolChat.Infraestructure.Data
{
    public partial class UserRepository : BaseRepository<User>, IUserRepository
    {
		public UserRepository(IDatabaseFactory databaseFactory)
	        : base(databaseFactory)
	    {
	    }

      
    }
}