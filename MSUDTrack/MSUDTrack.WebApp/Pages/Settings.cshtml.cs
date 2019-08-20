using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MSUDTrack.DataModels.Models;
using MSUDTrack.Services;

namespace MSUDTrack.WebApp.Pages
{
    [Authorize]
    public class SettingsModel : PageModel
    {
        private readonly ChildrensService _childrensService;
        private readonly UserManager<ApplicationUser> userManager;

        public SettingsModel(ChildrensService childrensService, UserManager<ApplicationUser> userManager)
        {
            _childrensService = childrensService;
            this.userManager = userManager;
        }

        [BindProperty]
        public List<Child> Children { get; set; } = new List<Child>();
        [BindProperty]
        public Child NewChild { get; set; } = new Child() { Id = Guid.NewGuid().ToString(), IsActive = true, Birthday = new DateTime(2010, 1, 1), LeucineDailyCount = 500, LeucineMultiple = 100 };

        [BindProperty]
        public string ChildDeleteId { get; set; }

        public async Task OnGetAsync()
        {
            var user = await userManager.GetUserAsync(User);

            Children = await _childrensService.GetChildrenForFamilyAsync(user.FamilyId);
        }

        public async Task InitDataAsync()
        {
            var user = await userManager.GetUserAsync(User);
            ModelState.Clear();

            NewChild = new Child() { Id = Guid.NewGuid().ToString(), IsActive = true, Birthday = new DateTime(2010, 1, 1), LeucineDailyCount = 500, LeucineMultiple = 100 };
            Children = await _childrensService.GetChildrenForFamilyAsync(user.FamilyId);
        }

        public async Task OnPostAddChildAsync()
        {
            var user = await userManager.GetUserAsync(User);

            user.ChildId = NewChild.Id;
            NewChild.FamilyId = user.FamilyId;

            await _childrensService.CreateAsync(NewChild);
            await userManager.UpdateAsync(user);

            await InitDataAsync();
        }

        public async Task<IActionResult> OnPostDeleteChildAsync(string id)
        {
            var user = await userManager.GetUserAsync(User);
            var children = await _childrensService.GetChildrenForFamilyAsync(user.FamilyId);

            //if there won't be any left...
            if (children.Count == 1)
            {
                await InitDataAsync();
                return Page();
            }

            if(!string.IsNullOrEmpty(ChildDeleteId))
            {
                user.ChildId = children.FirstOrDefault(c => c.Id != id).Id;
                await userManager.UpdateAsync(user);

                await _childrensService.DeleteAsync(ChildDeleteId);

                ChildDeleteId = "";
            }
            else
            {
                ChildDeleteId = id;
            }

            await InitDataAsync();
            return Page();
        }
    }
}