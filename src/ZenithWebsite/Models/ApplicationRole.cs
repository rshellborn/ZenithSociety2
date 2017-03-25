using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ZenithWebsite.Models
{
    public class ApplicationRole : IdentityRole
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
  
        public List<ApplicationUser> ApplicationUsers { get; set; }

    }
}
