using System.Collections.Generic;
using CoolChat.Core.Interfaces.Data;
using CoolChat.Core.Interfaces.Service;
using CoolChat.Core.Models;

namespace CoolChat.Core.Services
{
    public partial class ChatService : BaseService<Chat>, IChatService
    {
		protected IChatRepository ChatRepository;

        public ChatService(IUnitOfWork unitOfWork, IChatRepository chatRepository)
			:base(unitOfWork)
		{
            Repository = ChatRepository = chatRepository;
		}

        public IEnumerable<Chat> GetChats(int userId)
        {
            return ChatRepository.GetChats(userId);
        }
    }
}