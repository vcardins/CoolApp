using System.Collections.Generic;
using CoolApp.Core.Interfaces.Data;
using CoolApp.Core.Models;
using CoolApp.Infraestructure.Profiles;

namespace CoolApp.Infraestructure.Data
{
    public class ChatRepository : BaseRepository<Chat>, IChatRepository
    {
		public ChatRepository(IDatabaseFactory databaseFactory)
	        : base(databaseFactory)
	    {
	    }

        public IEnumerable<Chat> GetChats(int userId)
        {
            var comments = Find(
                x => (x.UserFromId == UserProfile.Current.UserId && x.UserToId == userId)
                     || (x.UserToId == UserProfile.Current.UserId && x.UserFromId == userId)
                );
            return comments;
        }
    }
}