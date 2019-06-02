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
            new SelectListItem() { Text = "body & mind", Value = "body & mind" },
            new SelectListItem() { Text = "ogólnorozwojowe", Value = "ogólnorozwojowe" },
            new SelectListItem() { Text = "sztuki walki", Value = "sztuki walki" },
            new SelectListItem() { Text = "inne", Value = "inne" }
            };
        }
    }
}
