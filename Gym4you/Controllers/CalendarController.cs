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
using Microsoft.AspNetCore.Authorization;
using Gym4you.Models.ViewModels;
using System.Net;
using System.Net.Mime;

namespace Gym4you.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public CalendarController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Calendar
        public async Task<IActionResult> Index(int? month, int? year)
        {
            month = month ?? DateTime.Now.Month;
            year = year ?? DateTime.Now.Year;
            DateTime dateTime = new DateTime(year ?? DateTime.Now.Year, month ?? DateTime.Now.Month, 1);
            var applicationDbContext = await _context.Events.Include(p => p.Instructor).Where(p => p.Date.Month == (month ?? DateTime.Now.Month) && p.Date.Year == (year ?? DateTime.Now.Year)).ToListAsync();


            foreach (var item in applicationDbContext)
            {
                item.Amount = item.Amount - _context.EventUser.Where(p => p.Event.Id == item.Id).Count();
            }
            CalendarViewModel calendarViewModel = new CalendarViewModel()
            {
                Events = applicationDbContext,
                Month = month ?? DateTime.Now.Month,
                Year = year ?? DateTime.Now.Year,
                PrevMonth = dateTime.AddMonths(-1).Month,
                PrevYear = dateTime.AddMonths(-1).Year,
                NextMonth = dateTime.AddMonths(1).Month,
                NextYear = dateTime.AddMonths(1).Year
            };
            return View(calendarViewModel);
        }


        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToEvent([FromBody] Event eventId)
        {
            IdentityUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            Event eventObject = await _context.FindAsync<Event>(eventId.Id);
            int allParticipants = await _context.EventUser.Where(p => p.Event.Id == eventId.Id).CountAsync();
            bool isExitsUserInEvent = await _context.EventUser.AnyAsync(p => p.Event.Id == eventId.Id && p.User.Id == user.Id);
            if (isExitsUserInEvent)
            {
                return Json(new { success = false, responseText = "You are already registered in this event" });

            }

            if (eventObject.Amount < allParticipants + 1)
            {

                return Json(new { success = false, responseText = "You can't sign up, because there are already enough participants for these event." });
            }

            EventUser eventUser = new EventUser()
            {
                User = user,
                Event = eventObject
            };

            _context.EventUser.Add(eventUser);
            await _context.SaveChangesAsync();
            return Json(new { success = true });

        }

        private IActionResult Json(object p, object allowGet)
        {
            throw new NotImplementedException();
        }
    }

}
