using System.Collections.Generic;
using System.Linq;
using CoolApp.Core.Interfaces.Data;
using CoolApp.Core.Interfaces.Service;
using CoolApp.Core.Models;

namespace CoolApp.Core.Services
{
    public partial class FriendshipService : BaseService<Friendship>, IFriendshipService
    {
		private readonly IFriendshipRepository _frinedshipRepository;

        public FriendshipService(IUnitOfWork unitOfWork, IFriendshipRepository frinedshipRepository)
			:base(unitOfWork)
		{
            Repository = _frinedshipRepository = frinedshipRepository;
		}

        public IEnumerable<Friendship> GetFriendshipsByUserId(int userId)
        {
            return Find(x => x.UserId.Equals(userId) || x.FriendId.Equals(userId));
        }

        public IEnumerable<Friendship> GetFriendshipsByUsername(string userName)
        {
            return Find(x => x.User.Username == userName || x.Friend.Username == userName);
        }
    }
}