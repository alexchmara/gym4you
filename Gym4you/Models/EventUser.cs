using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym4you.Models
{
    public class EventUser
    {
        public int Id { get; set; }
        public IdentityUser User { get; set; }
        public Event Event { get; set; }
    }
}
