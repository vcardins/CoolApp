#region credits
// ***********************************************************************
// Assembly	: TaskForceManager.Common
// Author	: Rod Johnson
// Created	: 03-23-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace CoolApp.Common.Utils
{
    #region

    #endregion

    public static class NetworkHelper
    {
        private static IPAddress LocalIPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }

        public static string LocalIPAddressString()
        {
            return LocalIPAddress().ToString();
        }
    }
}
