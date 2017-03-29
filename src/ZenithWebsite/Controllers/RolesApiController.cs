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
    [Route("api/RolesApi")]
    public class RolesApiController : Controller
    {
        private readonly ZenithContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesApiController(ZenithContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // GET: api/RolesApi
        [HttpGet]
        public IEnumerable<IdentityRole> GetRoles()
        {

            return _context.Roles.ToList();
        }

        // GET: api/RolesApi/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityRole role = _context.Roles.SingleOrDefault(m => m.Id == id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }
    }
}