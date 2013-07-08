
namespace CoolApp.Models.Chats
{
    public class ChatUser
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string DisplayName { get; set; }
       
        public string PhotoFile { get; set; }

        public bool IsOnline { get; set; }
      
    }
}