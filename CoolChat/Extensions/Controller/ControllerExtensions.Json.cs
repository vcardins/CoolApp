using CoolChat.Extensions.ActionResult;
using Newtonsoft.Json;

namespace CoolChat.Extensions.Controller
{
    public static partial class ControllerExtensions
    {
        public static JsonNetResult JsonEx(this System.Web.Mvc.Controller controller, object responseBody)
        {
            return new JsonNetResult(responseBody);
        }

        public static JsonNetResult JsonEx(this System.Web.Mvc.Controller controller, object responseBody, JsonSerializerSettings settings)
        {
            return new JsonNetResult(responseBody, settings);
        } 
    }
}