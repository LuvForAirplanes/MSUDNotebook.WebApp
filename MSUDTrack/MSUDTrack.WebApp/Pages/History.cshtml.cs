using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSUDTrack.DataModels.Models;

namespace MSUDTrack.WebApp.Pages
{
    [Authorize]
    public class HistoryModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HistoryModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string date)
        {
            if(date != null)
            {
                var user = await _userManager.GetUserAsync(User);
                user.CurrentView = DateTime.Parse(date);
                user.CurrentViewSet = DateTime.Now;
                await _userManager.UpdateAsync(user);

                return RedirectToPage("/Today");
            }
            else
            {
                return Page();
            }
        }
    }
}