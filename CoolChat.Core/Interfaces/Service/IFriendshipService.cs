using System.Collections.Generic;
using CoolChat.Core.Models;

namespace CoolChat.Core.Interfaces.Service
{
    public partial interface IFriendshipService : IService<Friendship>
    {
        IEnumerable<Friendship> GetFriendshipsByUserId(int userId);

        IEnumerable<Friendship> GetFriendshipsByUsername(string userName);
    }
}