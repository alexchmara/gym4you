using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym4you.Services
{
    public static class TypeWorkoutService
    {
        public static IEnumerable<SelectListItem> GetTypes()
        {
            return new SelectListItem[]
            {
            new SelectListItem() { Text = "Cardio", Value = "Cardio" },
            new SelectListItem() { Text = "Fitness", Value = "Fitness" },
            new SelectListItem() { Text = "Interwał", Value = "Interwał" },
            new SelectListItem() { Text = "Anaerobowy", Value = "Anaerobowy" }
            };
        }
    }
}
