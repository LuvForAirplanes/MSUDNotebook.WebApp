using MSUDTrack.DataModels.Models;
using MSUDTrack.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSUDTrack.Services
{
    public class RecordsService : Repository<Record, string>
    {
        private readonly PeriodsService _periodsService;

        public RecordsService(TrackerDbContext context, PeriodsService periodsService) : base(context)
        {
            _periodsService = periodsService;
        }

        public async Task<RecordsDTO> GetTodaysRecordsByChildAsync(string childId)
        {
            var periods = await _periodsService.ListAsync();
            var report = new RecordsDTO();

            foreach (var period in periods)
            {
                var records = Get().Where(r => r.ChildId == childId).Where(r => r.PeriodId == period.Id).ToList();

                report.Periods.Add(new PeriodDTO()
                {
                    Period = period,
                    Records = records
                });
            }

            return report;
        }
    }
}
