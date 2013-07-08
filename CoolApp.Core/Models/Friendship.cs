using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolApp.Core.Models
{
    public class Friendship : DomainObject
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Key, Column(Order = 1)]
        public int FriendId { get; set; }

        [ForeignKey("FriendId")]
        public virtual User Friend { get; set; }   
    }
}
