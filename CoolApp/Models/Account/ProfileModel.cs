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

using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CoolChat.Models.Account
{
    #region

    

    #endregion

    public partial class ProfileModel
    {
        public ProfileModel()
        {
            PersonalInfo = new PersonalInfoModel();
            AddressInfo = new AddressModel();
        }

        public PersonalInfoModel PersonalInfo { get; set; }

        public AddressModel AddressInfo { get; set; }

        public class PersonalInfoModel
        {

            [ScaffoldColumn(false)]
            [HiddenInput(DisplayValue = false)]
            public string Username { get; set; }

            [Required]
            [Textbox(TextboxSize = TextboxSize.Large), Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last name")]
            public string LastName { get; set; }

            [Required]
            [EmailTextbox]
            [Display(Name = "Email")]
            public virtual String Email { get; set; }

            [Required]
            [Textbox(TextboxSize = TextboxSize.Small), Display(Name = "Phone Number")]
            [PhoneNumberTextbox]
            public string PhoneNumber { get; set; }

            [Display(Name = "Photo")]
            public string PhotoFile { get; set; }

        }
    }
}