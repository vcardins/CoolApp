using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace CoolApp.Core.Models
{
    [DataContract]
    public class Chat : DomainObject
    {
        [Key]
        public int ChatId { get; set; }

        public int UserFromId { get; set; }

        [ForeignKey("UserFromId")]
        public virtual User UserFrom { get; set; }

        public int UserToId { get; set; }

        [ForeignKey("UserToId")]
        public virtual User UserTo { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }

        public bool IsBlocked { get; set; }

        public string BlockingReason { get; set; }
    }
}
