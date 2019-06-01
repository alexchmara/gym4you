using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym4you.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Event> Events { get; set; }
        [NotMapped]
        public string FullName => FirstName + " " + LastName;

    }
}
