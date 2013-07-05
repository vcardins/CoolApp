using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web.Mvc;
using CoolChat.Core.Interfaces.Service;
using CoolChat.Core.Models;
using Microsoft.AspNet.SignalR;

namespace CoolChat.Hubs
{
    public class ChatHub : Hub
    {
        #region Hub Methods
        public override Task OnConnected()
        {
            var currentUser = Context.User.Identity.Name;

            JoinGroup(currentUser);

            var chatService = DependencyResolver.Current.GetService<IChatService>();

            var chats = chatService.GetLastedChats(currentUser);

            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            JoinGroup(Context.User.Identity.Name);

            return base.OnReconnected();
        }

        public override Task OnDisconnected()
        {
            LeaveGroup(Context.User.Identity.Name);

            return base.OnConnected();
        }

        private Task JoinGroup(string groupName)
        {
            return Groups.Add(Context.ConnectionId, groupName);
        }

        private Task LeaveGroup(string groupName)
        {
            return Groups.Remove(Context.ConnectionId, groupName);
        }
        #endregion

        #region ChatMethods
        public void StartChatSession()
        {
            Clients.Group(Context.User.Identity.Name).StartChatSessionServerCallback(new { success = true, username = Context.User.Identity.Name, items = new List<string>() });
        }

        public void ChatHeartBeat()
        {
        }

        public void ChatSendMessage(string to, string message)
        {
            var chatService = DependencyResolver.Current.GetService<IChatService>();

            var userSource = Context.User.Identity.Name;

            chatService.InsertNewChat(userSource, to, message);

            Clients.Group(to).ReceiveMessage(new {userSource, message });
        }
        #endregion
    }
}