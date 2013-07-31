using System.Collections.Generic;
using CoolApp.Core.Models.Mobile;

namespace CoolApp.Core.Interfaces.External
{
    public interface IMobileRestAPI
    {
        void SendNotification(MobileNotification mobileNotification);
    }
}