using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gym4you.Data;
using Gym4you.Models;
using Microsoft.AspNetCore.Identity;

namespace Gym4you.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ApplicationDbContext _context;
        //private readonly UserManager<ApplicationUser> _userManager;
        public CalendarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Calendar
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Events.Include(p => p.Instructor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Calendar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(p => p.Instructor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Calendar/Create
        public IActionResult Create()
        {
            ViewData["InstructorFK"] = new SelectList(_context.Instructors, "Id", "Id");
            return View();
        }

        // POST: Calendar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Date,Amount,Type,InstructorFK")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstructorFK"] = new SelectList(_context.Instructors, "Id", "Id", @event.InstructorFK);
            return View(@event);
        }

        // GET: Calendar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["InstructorFK"] = new SelectList(_context.Instructors, "Id", "Id", @event.InstructorFK);
            return View(@event);
        }

        // POST: Calendar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Date,Amount,Type,InstructorFK")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstructorFK"] = new SelectList(_context.Instructors, "Id", "Id", @event.InstructorFK);
            return View(@event);
        }

        // GET: Calendar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(p => p.Instructor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Calendar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }


        //public async Task<IActionResult> AddUserToEvent(Event @event)
        //{
        //    IdentityUser user = await userManager.FindByNameAsync(HttpContext.User.Identity.Name);
        //    EventUser eventUser = new EventUser()
        //    {
        //        User = HttpContext.User.Identity,

        //    }
        //    _context.EventUser.Add(@event);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));

        //    ViewData["InstructorFK"] = new SelectList(_context.Instructors, "Id", "Id", @event.InstructorFK);
        //    return View(@event);
        //}

    }


}
