using CoolChat.Core.Models;

namespace CoolChat.Core.Interfaces.Service
{
    public partial interface IUserService : IService<User>
    {
		// Add extra serviceinterface methods in a partial interface
        User GetByUsername(string name);
    }
}