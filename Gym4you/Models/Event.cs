using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gym4you.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int InstructorFK { get; set; }
        [ForeignKey("InstructorFK")]
        public Instructor Instructor { get; set; }

    }
}
