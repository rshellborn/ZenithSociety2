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
    [Route("api/EventsApi")]
    public class EventsApiController : Controller
    {
        private readonly ZenithContext _context;

        public EventsApiController(ZenithContext context)
        {
            _context = context;
        }

        // GET: api/EventsApi/All -> Eventspage
        [HttpGet("{all}")]
        public IEnumerable<Event> GetEvents([FromRoute] string all)
        {
            var @event = _context.Events.Include(e => e.Activity);
            var empty = _context.Events.Take(0);

            if (all == "all")
            {
                return @event;
            }

            return empty;
        }

        // GET: api/EventsApi -> Homepage
        [HttpGet]
        public IEnumerable<Event> GetEvents()
        {
            var @event = _context.Events.Include(e => e.Activity).OrderBy(e => e.EventFrom).ToList();

            List<Event> week = new List<Event>();

            DateTime today = DateTime.Today;
            DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            startOfWeek = startOfWeek.AddDays(1.0); // Monday
            DateTime endOfWeek = startOfWeek.AddDays(7); // Sunday

            foreach (var item in @event)
            {
                if (item.EventFrom >= startOfWeek && item.EventTo < endOfWeek) // Monday to Sunday
                {
                    if (item.IsActive == true)
                    {
                        week.Add(item);
                    }
                }
            }

            return week;
        }

        // GET: api/EventsApi/All -> Eventspage


        // GET: api/EventsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Event @event = await _context.Events.Include(e => e.Activity).SingleOrDefaultAsync(m => m.EventId == id);

            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        // PUT: api/EventsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent([FromRoute] int id, [FromBody] Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @event.EventId)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/EventsApi
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostEvent([FromBody] Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Events.Add(@event);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EventExists(@event.EventId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEvent", new { id = @event.EventId }, @event);
        }

        // DELETE: api/EventsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Event @event = await _context.Events.SingleOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return Ok(@event);
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}