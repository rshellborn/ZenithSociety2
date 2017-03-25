using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ZenithWebsite.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Key]
        public int UserId { get; set; }
        public string UserNm { get; set; }
        public string RoleId { get; set; }
        public virtual ApplicationRole ApplicationRole { get; set; }

    }
}
