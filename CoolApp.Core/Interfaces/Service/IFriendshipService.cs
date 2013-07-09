using System.Collections.Generic;
using CoolApp.Core.Models;

namespace CoolApp.Core.Interfaces.Service
{
    public partial interface IFriendshipService : IService<Friendship>
    {
        IEnumerable<Friendship> GetFriendshipsByUserId(int userId);

        IEnumerable<Friendship> GetFriendshipsByUsername(string userName);
    }
}