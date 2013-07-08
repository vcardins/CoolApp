using CoolApp.Core.Interfaces.Data;
using CoolApp.Core.Models;

namespace CoolApp.Infraestructure.Data
{
    public partial class UserRepository : BaseRepository<User>, IUserRepository
    {
		public UserRepository(IDatabaseFactory databaseFactory)
	        : base(databaseFactory)
	    {
	    }

      
    }
}