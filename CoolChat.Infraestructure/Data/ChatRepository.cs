using System.Collections.Generic;
using CoolChat.Core.Interfaces.Data;
using CoolChat.Core.Models;
using CoolChat.Infraestructure.Profiles;

namespace CoolChat.Infraestructure.Data
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