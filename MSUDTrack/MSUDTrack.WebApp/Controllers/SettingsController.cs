using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MSUDTrack.DataModels.Models;
using MSUDTrack.Services;

namespace MSUDTrack.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly TrackerDbContext _context;
        private readonly ChildrensService _childrensService;
        private readonly UserManager<ApplicationUser> userManager;

        public SettingsController(TrackerDbContext context, ChildrensService childrensService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _childrensService = childrensService;
            this.userManager = userManager;
        }


        [HttpPut("dailycount/{grams}")]
        public async Task<ActionResult<int>> PutDailyCount(int grams)
        {
            var user = await userManager.GetUserAsync(User);
            var currentChild = _childrensService.Get().Where(c => c.Id == user.ChildId).FirstOrDefault();
            currentChild.LeucineDailyCount = grams;
            await _childrensService.UpdateAsync(currentChild, currentChild.Id);
            return grams;
        }

        [HttpPut("exchange/{grams}")]
        public async Task<ActionResult<int>> PutExchange(int grams)
        {
            var user = await userManager.GetUserAsync(User);
            var currentChild = _childrensService.Get().Where(c => c.Id == user.ChildId).FirstOrDefault();
            currentChild.LeucineMultiple = grams;
            await _childrensService.UpdateAsync(currentChild, currentChild.Id);
            return grams;
        }

        [Route("GetByAdminId")] /* this route becomes api/[controller]/GetByAdminId */
        public string GetByAdminId([FromQuery] int adminId)
        {
            return $"GetByAdminId: You passed in {adminId}";
        }

    }
}
