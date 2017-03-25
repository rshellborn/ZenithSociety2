using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZenithWebsite.Models;
using Microsoft.AspNetCore.Authorization;

namespace ZenithWebsite.Controllers
{
    [Produces("application/json")]
    [Route("api/RolesApi")]
    public class ApplicationRoleApiController : Controller
    {
        private readonly ZenithContext _context;

        public ApplicationRoleApiController(ZenithContext context)
        {
            _context = context;
        }

        // GET: api/RolesApi
        [HttpGet]
        public IEnumerable<ApplicationRole> GetRoles()
        {
            return _context.ApplicationRoles.Include(a => a.ApplicationUsers).ToList();
        }

        // GET: api/RolesApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoles([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationRole role = await _context.ApplicationRoles.SingleOrDefaultAsync(m => m.RoleId == id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        // PUT: api/RolesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole([FromRoute] int id, [FromBody] ApplicationRole role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != role.RoleId)
            {
                return BadRequest();
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationRoleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RolesApi
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostRole([FromBody] ApplicationRole role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ApplicationRoles.Add(role);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ApplicationRoleExists(role.RoleId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetApplicationRole", new { id = role.RoleId }, role);
        }

        // DELETE: api/RolesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationRole role = await _context.ApplicationRoles.SingleOrDefaultAsync(m => m.RoleId == id);
            if (role == null)
            {
                return NotFound();
            }

            _context.ApplicationRoles.Remove(role);
            await _context.SaveChangesAsync();

            return Ok(role);
        }

        private bool ApplicationRoleExists(int id)
        {
            return _context.ApplicationRoles.Any(e => e.RoleId == id);
        }
    }
}