using CoolChat.Core.Interfaces.Validation;
using CoolChat.Core.Models;

namespace CoolChat.Core.Interfaces.Service
{
    public interface IAuthenticationService
    {
        IValidationContainer<User> Authenticate(string userName, string password);

        void SignIn(string username, bool isPersistant, double? offsetTimeZone);

        void SignOut();
    }
}