using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gym4you.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public string Type { get; set; }

        public int InstructorFK { get; set; }
        [ForeignKey("InstructorFK")]
        public Instructor Instructor { get; set; }

    }
}
