using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gym4you.Data;
using Gym4you.Models;

namespace Gym4you.Controllers
{
    public class EventUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EventUsers
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventUsers = await _context.EventUser.Include(p=>p.Event.Instructor).Include(p=>p.User).Where(p=>p.Event.Id == id).ToListAsync();
            if (eventUsers == null)
            {
                return NotFound();
            }

            return View(eventUsers);
        }

        // GET: EventUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventUser = await _context.EventUser.Include(p => p.Event.Instructor).Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventUser == null)
            {
                return NotFound();
            }

            return View(eventUser);
        }

        // GET: EventUsers/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: EventUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id")] EventUser eventUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(eventUser);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(eventUser);
        //}

        //// GET: EventUsers/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var eventUser = await _context.EventUser.Include(p=>p.User)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (eventUser == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(eventUser);
        //}

        // POST: EventUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventUser = await _context.EventUser.Include(p=>p.Event).Where(p=>p.Id == id).FirstOrDefaultAsync();
         
            var eventDetails = await _context.Events.FindAsync(eventUser.Event.Id);
            eventDetails.Amount = eventDetails.Amount + 1;

            _context.EventUser.Remove(eventUser);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = id });
        }

        private bool EventUserExists(int id)
        {
            return _context.EventUser.Any(e => e.Id == id);
        }
    }
}
