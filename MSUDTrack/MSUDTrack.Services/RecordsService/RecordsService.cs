﻿using Microsoft.EntityFrameworkCore;
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
        private readonly ChildrensService _childrensService;

        public RecordsService(TrackerDbContext context, PeriodsService periodsService, ChildrensService childrensService) : base(context)
        {
            _periodsService = periodsService;
            _childrensService = childrensService;
        }

        public async Task<RecordsDTO> GetTodaysRecordsByCurrentChildAsync()
        {
            var child = _childrensService.Get().Where(c => c.IsSelected).FirstOrDefault();
            var periods = await _periodsService.ListAsync();
            var report = new RecordsDTO();

            foreach (var period in periods)
            {
                var records = Get().Where(r => r.ChildId == child.Id).Where(r => r.PeriodId == period.Id).Include(r => r.Food).ToList();

                report.Periods.Add(new PeriodDTO()
                {
                    Period = period,
                    Records = records
                });
            }

            return report;
        }

        public async Task<List<Record>> GetRecordsByPeriodAsync(string periodId)
        {
            var child = _childrensService.Get().Where(c => c.IsSelected).FirstOrDefault();
            var period = await _periodsService.Get().Where(p => p.Id == periodId).FirstOrDefaultAsync();
            var records = new List<Record>();

            //I'm not sure why the where's don't work right...?
            if (Get().Count() > 0)
            {
                records = Get().Where(r => r.ChildId == child.Id).Where(r => r.PeriodId == period.Id).Include(r => r.Food).ToList();
            }

            return records;
        }
    }
}