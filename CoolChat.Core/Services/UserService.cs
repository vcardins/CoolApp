using System.Linq;
using CoolChat.Core.Interfaces.Data;
using CoolChat.Core.Interfaces.Service;
using CoolChat.Core.Models;

namespace CoolChat.Core.Services
{
    public partial class UserService : BaseService<User>, IUserService
    {
		private readonly IUserRepository _userRepository;
		
		public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
			:base(unitOfWork)
		{
            Repository = _userRepository = userRepository;
		}


        public User GetByUsername(string username)
        {
            var user = _userRepository.GetAll().SingleOrDefault(x => x.Username == username);
            return user;
        }
    }
}