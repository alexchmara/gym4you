using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Gym4you.Areas.Identity.Pages.Account.Manage
{
    public class UsersInformationModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<UsersInformationModel> _logger;

        public UsersInformationModel(
            UserManager<IdentityUser> userManager,
            ILogger<UsersInformationModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        [TempData]
        public int CountUsers { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var users = await _userManager.GetUsersInRoleAsync("User");
            if (users != null)
            {
                CountUsers = users.Count;
            }

            return Page();
        }
    }
}