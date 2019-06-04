using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSUDTrack.Services;
using MSUDTrack.Services.DTOs;

namespace MSUDTrack.WebApp.Pages
{
    public class TodayModel : PageModel
    {
        private readonly RecordsService _recordsService;

        public TodayModel(RecordsService recordsService)
        {
            _recordsService = recordsService;
        }

        public RecordsDTO Records { get; set; }

        public async Task OnGetAsync()
        {
            Records = await _recordsService.GetTodaysRecordsByCurrentChildAsync();
        }
    }
}
