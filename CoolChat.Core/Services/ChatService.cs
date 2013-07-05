using System;
using System.Collections.Generic;
using System.Web.Mvc;
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

        public bool InsertNewChat(string userFrom, string userTo, string message)
        {
            try
            {
                var userService = DependencyResolver.Current.GetService<IUserService>();
                var sourceUser = userService.GetByUsername(userFrom);
                var targetUser = userService.GetByUsername(userTo);
                var container = SaveOrUpdate(new Chat { UserFromId = sourceUser.UserId, UserToId = targetUser.UserId, Message = message });
                return container.IsValid;
                
            }catch(Exception ex)
            {
                
            }
            return false;
        }

        public IEnumerable<Chat> GetChats(int userId)
        {
            return ChatRepository.GetChats(userId);
        }

        public IEnumerable<Chat> GetLastedChats(string userName)
        {
            return ChatRepository.Find(x => x.UserTo.Username == userName || x.UserFrom.Username == userName, 10);
        } 
    }
}