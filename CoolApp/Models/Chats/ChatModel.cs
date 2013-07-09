using System.ComponentModel.DataAnnotations;

namespace CoolApp.Models.Chats
{
    public class ChatModel
    {
        public int ChatId { get; set; }

        [ScaffoldColumn(false)]
        public string FirstName { get; set; }

        [ScaffoldColumn(false)]
        public string DisplayName { get; set; }

        //[Textbox(IsMultiline = true, PlaceholderText = "Type your message here...")]
        public string Message { get; set; }

        [ScaffoldColumn(false)]
        public string Created { get; set; }

        public string PhotoFile { get; set; }
      
    }
}