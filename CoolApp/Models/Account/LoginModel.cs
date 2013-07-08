#region credits
// ***********************************************************************
// Assembly	: TaskForceManager
// Author	: Rod Johnson
// Created	: 03-09-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.ComponentModel.DataAnnotations;

namespace CoolApp.Models.Account
{
    #region

    

    #endregion

    public class LoginModel
    {
        [Required]
        [Display(Name = "Username")]
        //[Textbox(TextboxSize = TextboxSize.Medium, PlaceholderText = "Username")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        //[PasswordTextbox(TextboxSize = TextboxSize.Medium, PlaceholderText = "Password")]
        [UIHint("Password")]
        public string Password { get; set; }
        
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [ScaffoldColumn(false)]
        public string ReturnUrl { get; set; }

        public double? OffsetTimeZone { get; set; }
       
    }
}