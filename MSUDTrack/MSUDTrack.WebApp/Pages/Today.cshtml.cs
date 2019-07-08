using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSUDTrack.DataModels.Models;
using MSUDTrack.Services;
using MSUDTrack.Services.DTOs;

namespace MSUDTrack.WebApp.Pages
{
    [Authorize]
    public class TodayModel : PageModel
    {
        private readonly RecordsService _recordsService;
        private readonly PeriodsService _periodsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodayModel(RecordsService recordsService, PeriodsService periodsService, UserManager<ApplicationUser> userManager)
        {
            _recordsService = recordsService;
            _periodsService = periodsService;
            _userManager = userManager;
        }

        public TodayDTO TodaysLog { get; set; } = new TodayDTO();

        public async Task<IActionResult> OnGetAsync()
        {
            ModelState.Clear();
            await LoadData();

            if (TodaysLog.Child == null)
                return RedirectToPage("/Settings");

            return Page();
        }

        public async Task<IActionResult> OnPostAddRecordAsync(string periodId, string childId)
        {
            await _recordsService.CreateAsync(new Record()
            {
                Id = System.Guid.NewGuid().ToString(),
                ChildId = childId,
                PeriodId = periodId,
                Created = DateTime.Now
            });

            await LoadData();
            ModelState.Clear();
            return Page();
        }

        public async Task LoadData()
        {
            TodaysLog = await _recordsService.GetTodaysRecordsByCurrentChildAsync(await _userManager.GetUserAsync(User));
        }
    }
}
