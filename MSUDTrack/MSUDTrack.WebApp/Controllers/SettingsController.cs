using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSUDTrack.Services;

namespace MSUDTrack.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly TrackerDbContext _context;
        private readonly ChildrensService _childrensService;

        public SettingsController(TrackerDbContext context, ChildrensService childrensService)
        {
            _context = context;
            _childrensService = childrensService;
        }


        [HttpPut("dailycount/{grams}")]
        public async Task<ActionResult<int>> PutDailyCount(int grams)
        {
            var currentChild = _childrensService.GetCurrentChild();
            currentChild.LeucineDailyCount = grams;
            await _childrensService.UpdateAsync(currentChild, currentChild.Id);
            return grams;
        }

        [HttpPut("exchange/{grams}")]
        public async Task<ActionResult<int>> PutExchange(int grams)
        {
            var currentChild = _childrensService.GetCurrentChild();
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
