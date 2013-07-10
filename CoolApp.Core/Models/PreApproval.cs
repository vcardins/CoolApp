using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoolApp.Core.Models
{
    public class PreApproval : DomainObject
    {
        [Key]
        public int PreApprovalId { get; set; }

        public int Userd { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        public string PreApprovalKey { get; set; }

        public bool Approved { get; set; }

        public bool IsBlocked { get; set; }

        public DateTime ContractStartDate { get; set; }

        public DateTime ContractEndDate { get; set; }
    }
}
