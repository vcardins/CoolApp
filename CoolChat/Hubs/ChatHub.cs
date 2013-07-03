using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace CoolChat.Hubs
{
    public class ChatHub : Hub
    {
        #region Hub Methods
        public override Task OnConnected()
        {
            JoinGroup(Context.User.Identity.Name);

            return base.OnConnected();
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
            Clients.Group(to).ReceiveMessage(new { userSource = Context.User.Identity.Name, message = message});
        }
        #endregion
    }
}