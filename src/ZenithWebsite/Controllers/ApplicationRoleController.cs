using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZenithWebsite.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ZenithWebsite.Controllers
{
    public class ApplicationRoleController : Controller
    {
        private readonly ZenithContext _context;

        public ApplicationRoleController(ZenithContext context)
        {
            _context = context;
        }

        // GET: ApplicationRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicationRoles.ToListAsync());
        }

        // GET: ApplicationRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicationRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId, RoleName,Description")] ApplicationRole role)
        {
            if (ModelState.IsValid)
            {
                _context.Add(role);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: ApplicationRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.ApplicationRoles.SingleOrDefaultAsync(m => m.RoleId == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: ApplicationRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await _context.ApplicationRoles.SingleOrDefaultAsync(m => m.RoleId == id);
            _context.ApplicationRoles.Remove(role);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ApplicationRoleExists(int id)
        {
            return _context.ApplicationRoles.Any(e => e.RoleId == id);
        }
    }
}
