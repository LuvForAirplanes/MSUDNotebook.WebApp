using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSUDTrack.DataModels.Models;
using MSUDTrack.Services;
using MSUDTrack.Services.DTOs;
using MSUDTrack.WebApp.MappingProfiles;

namespace MSUDTrack.WebApp.Pages
{
    public class TodayModel : PageModel
    {
        private readonly RecordsService _recordsService;
        private readonly PeriodsService _periodsService;
        private readonly RecordEditMapper _recordEditMapper;

        public TodayModel(RecordsService recordsService, PeriodsService periodsService, RecordEditMapper recordEditMapper)
        {
            _recordsService = recordsService;
            _periodsService = periodsService;
            _recordEditMapper = recordEditMapper;
        }

        public TodayDTO TodaysLog { get; set; } = new TodayDTO();

        public async Task OnGetAsync()
        {
            var todaysLog = new TodayDTO();
            var periods = await _periodsService.ListAsync();

            foreach (var period in periods)
            {
                var recordEdits = new List<RecordEdit>();
                var records = await _recordsService.GetRecordsByPeriodAsync(period.Id);

                foreach (var record in records)
                {
                    var mappedRecord = _recordEditMapper.Map(record);
                    recordEdits.Add(mappedRecord);
                }

                todaysLog.Periods.Add(new PeriodDTO()
                {
                    Period = period,
                    Records = recordEdits
                });
            }

            TodaysLog = todaysLog;
        }
    }

    public class TodayDTO
    {
        public List<PeriodDTO> Periods { get; set; } = new List<PeriodDTO>();
    }

    public class PeriodDTO
    {
        public Period Period = new Period();

        public List<RecordEdit> Records { get; set; }
    }
}
