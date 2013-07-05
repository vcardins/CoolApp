using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web.Mvc;
using CoolChat.Common.Crypto;
using CoolChat.Core.Interfaces.Service;
using CoolChat.Core.Models;
using Microsoft.AspNet.SignalR;

namespace CoolChat.Hubs
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, List<string>> ConnectedUsers = new Dictionary<string, List<string>>(); 

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
            var currentUser = Context.User.Identity.Name;

            JoinGroup(currentUser);

            return base.OnReconnected();
        }

        public override Task OnDisconnected()
        {
            var currentUser = Context.User.Identity.Name;

            LeaveGroup(currentUser);

            var userService = DependencyResolver.Current.GetService<IUserService>();

            IFriendshipService frinedshipService = DependencyResolver.Current.GetService<IFriendshipService>();
            var friends = frinedshipService.GetFriendshipsByUsername(currentUser);

            var userNames = friends.Select(x => (x.User.Username == currentUser)?x.Friend.Username:x.User.Username).ToList();

            SignalOffline(userNames);

            return base.OnConnected();
        }

        private Task JoinGroup(string groupName)
        {
            var connectionId = Context.ConnectionId;
            
            InsertUserInList(groupName);

            return Groups.Add(connectionId, groupName);
        }

        private Task LeaveGroup(string groupName)
        {
            var connectionId = Context.ConnectionId;

            RemoveUserFromList(groupName);

            return Groups.Remove(connectionId, groupName);
        }

        public void RemoveGroup(string groupName)
        {
            List<string> connectionsId;
            if (ConnectedUsers.TryGetValue(groupName, out connectionsId))
            {
                foreach (var connectionId in connectionsId)
                {
                    Groups.Remove(connectionId, groupName);
                }
            }
            ConnectedUsers.Remove(groupName);
        }

        private void InsertUserInList(string groupName)
        {
            var connectionId = Context.ConnectionId;
            
            List<string> connectionsId;
            if(ConnectedUsers.TryGetValue(groupName, out connectionsId))
            {
                connectionsId.Add(connectionId);
            }
            else
            {
                connectionsId = new List<string> {connectionId};
                ConnectedUsers.Add(groupName, connectionsId);
            }
        }

        private void RemoveUserFromList(string groupName)
        {
            var connectionId = Context.ConnectionId;

            List<string> connectionsId;
            if (ConnectedUsers.TryGetValue(groupName, out connectionsId))
            {
                connectionsId.Remove(connectionId);
                if(connectionsId.Count == 0)
                {
                    ConnectedUsers.Remove(groupName);
                }
            }
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

            var hastTag = CryptoHelper.ParseHashtag(message);

            chatService.InsertNewChat(userSource, to, message);

            Clients.Group(to).ReceiveMessage(new {userSource, message });
        }

        public bool CheckConnectedUser(string userName)
        {
            return ConnectedUsers.ContainsKey(userName);
        }

        public void SignalOnline(List<string> usersNames)
        {
            var currentUser = Context.User.Identity.Name;

            foreach (var userName in usersNames)
            {
                if(CheckConnectedUser(userName))
                    Clients.Group(userName).SignalOnline(currentUser);   
            }
        }

        public void SignalOffline(List<string> usersNames)
        {
            var currentUser = Context.User.Identity.Name;

            foreach (var userName in usersNames)
            {
                if (CheckConnectedUser(userName))
                    Clients.Group(userName).SignalOffline(currentUser);
            }
        }

        public void VerifyPartnersOnline(string userName)
        {
            var currentUser = Context.User.Identity.Name;
            
            if (CheckConnectedUser(userName))
            {
                Clients.Group(currentUser).SignalOnline(userName);
                Clients.Group(userName).SignalOnline(currentUser);
            }
        }

        #endregion
    }
}