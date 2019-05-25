using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym4you.Models.ViewModels
{
    public class CalendarViewModel
    {
        public IEnumerable<Event> Events { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int PrevMonth { get; set; }
        public int PrevYear { get; set; }
        public int NextMonth { get; set; }
        public int NextYear { get; set; }
    }
}
