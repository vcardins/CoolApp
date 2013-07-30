using System.Collections.Generic;

namespace CoolApp.Core.Interfaces.External
{
    public interface IMobileRestAPI
    {
        object MobileRestCall(string urlAction, object bodyObject, string method);
        void SendNotification(string title, string text, string channel, string clientId, string devideToken);
    }
}