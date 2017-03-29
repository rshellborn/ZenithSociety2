using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ZenithWebsite.Models;
using System.Security.Claims;
using System.Security.Principal;

namespace ZenithWebsite.Controllers
{
    [Produces("application/json")]
    [Route("api/RoleAPI")]
    public class RoleAPIController : Controller
    {
        private readonly ZenithContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RoleAPIController(ZenithContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: api/RoleAPI

        // GET: api/EventsApi/5
        [HttpGet]
        public async Task<ICollection<IdentityUserRole<string>>> GetRole()
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleMngr = new RoleManager<IdentityRole>(roleStore);

            var roles = roleMngr.Roles.ToList();

            return roles;
        }


    }
}