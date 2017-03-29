using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZenithWebsite.Models.UserRolesViewModel
{
    public class EditUserRoleViewModel
    {
        [Display(Name = "Username")]
        public string Username { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Roles")]
        public ICollection<String> Roles { get; set; }

        [Required]
        [Display(Name = "Select Role")]
        public string SelectedRole { get; set; }
    }

}