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
        private readonly FoodsService _foodsService;

        public TodayModel(RecordsService recordsService, PeriodsService periodsService, FoodsService foodsService)
        {
            _recordsService = recordsService;
            _periodsService = periodsService;
            _foodsService = foodsService;
        }

        public TodayDTO TodaysLog { get; set; } = new TodayDTO();

        public List<Food> Foods { get; set; }

        public async Task OnGetAsync()
        {
            TodaysLog = await _recordsService.GetTodaysRecordsByCurrentChildAsync();
            Foods = await _foodsService.ListAsync();
        }
    }
}
