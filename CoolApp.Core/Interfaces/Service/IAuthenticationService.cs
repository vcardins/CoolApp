using CoolApp.Core.Interfaces.Validation;
using CoolApp.Core.Models;

namespace CoolApp.Core.Interfaces.Service
{
    public interface IAuthenticationService
    {
        IValidationContainer<User> Authenticate(string userName, string password);

        void SignIn(string username, bool isPersistant, double? offsetTimeZone);

        void SignOut();
    }
}