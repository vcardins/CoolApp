using System.Linq;
using CoolApp.Core.Interfaces.Data;
using CoolApp.Core.Interfaces.Service;
using CoolApp.Core.Models;

namespace CoolApp.Core.Services
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