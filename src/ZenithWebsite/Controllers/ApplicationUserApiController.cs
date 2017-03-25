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
    [Route("api/UsersApi")]
    public class ApplicationUserApiController : Controller
    {
        private readonly ZenithContext _context;

        public ApplicationUserApiController(ZenithContext context)
        {
            _context = context;
        }

        // GET: api/UsersApi
        [HttpGet]
        public IEnumerable<ApplicationUser> GetUsers()
        {
            return _context.ApplicationUsers.Include(e => e.ApplicationRole);
        }

        // GET: api/UsersApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = await _context.ApplicationUsers.SingleOrDefaultAsync(m => m.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/UsersApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExists(id))
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

        // POST: api/UsersApi
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostUser([FromBody] ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ApplicationUsers.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ApplicationUserExists(user.UserId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetApplicationUser", new { id = user.UserId }, user);
        }

        // DELETE: api/UsersApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = await _context.ApplicationUsers.SingleOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.ApplicationUsers.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool ApplicationUserExists(int id)
        {
            return _context.ApplicationUsers.Any(e => e.UserId == id);
        }
    }
}