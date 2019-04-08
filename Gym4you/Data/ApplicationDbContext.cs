using System;
using System.Collections.Generic;
using System.Text;
using Gym4you.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gym4you.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<EventUser> EventUser { get; set; }
    }
}
