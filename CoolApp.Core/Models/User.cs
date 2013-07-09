#region credits
// ***********************************************************************
// Assembly	: TaskForceManager.Core
// Author	: Rod Johnson
// Created	: 03-23-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System;
using System.ComponentModel.DataAnnotations;
using CoolApp.Core.Enums;

namespace CoolApp.Core.Models
{
    #region

    

    #endregion

    public partial class User : DomainObject
    {

        [Key]
        public int UserId { get; set; }

        [StringLength(40)]
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [EmailAddress]
        [StringLength(120)]
        [Required]
        public string Email { get; set; }

        public Guid UserGuid { get; set; }

        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }
      
        public string PhotoFile { get; set; }
        
        public DateTime? PasswordChanged { get; set; }

        public bool IsAccountVerified { get; set; }
        public bool IsLoginAllowed { get; set; }
        public bool IsAccountClosed { get; set; }
        public DateTime? AccountClosed { get; set; }

        public DateTime? LastLogin { get; set; }
        public DateTime? LastFailedLogin { get; set; }
        public int FailedLoginCount { get; set; }

        [StringLength(100)]
        public string VerificationKey { get; set; }
        public DateTime? VerificationKeySent { get; set; }

        [Required]
        [StringLength(200)]
        public string HashedPassword { get; set; }

        internal DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }
       
        public bool IsLockedOut { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        public string DisplayName
        {
            get { return string.Format("{0} {1}.", FirstName, LastName.Substring(0, 1)); }
        }

    }
}