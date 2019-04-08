using System;
using System.Collections.Generic;

namespace Gym4you.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Event> Events { get; set; }

    }
}
