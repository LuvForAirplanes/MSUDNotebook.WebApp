using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSUDTrack.DataModels.Models;
using MSUDTrack.Services;
using MSUDTrack.Services.DTOs;

namespace MSUDTrack.WebApp.Pages
{
    public class TodayModel : PageModel
    {
        private readonly RecordsService _recordsService;
        private readonly PeriodsService _periodsService;

        public TodayModel(RecordsService recordsService, PeriodsService periodsService)
        {
            _recordsService = recordsService;
            _periodsService = periodsService;
        }

        public TodayDTO TodaysLog { get; set; } = new TodayDTO();

        public async Task OnGetAsync()
        {
            ModelState.Clear();
            await LoadData();
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
            TodaysLog = await _recordsService.GetTodaysRecordsByCurrentChildAsync();
        }
    }
}
