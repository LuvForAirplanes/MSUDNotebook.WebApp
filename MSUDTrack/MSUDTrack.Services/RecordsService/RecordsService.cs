using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MSUDTrack.DataModels.Models;
using MSUDTrack.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
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

        //not currently in use
        public async Task<TodayDTO> GetTodaysRecordsByCurrentChildAsync(ApplicationUser user)
        {
            var periods = await _periodsService.ListAsync();
            var log = new TodayDTO();

            foreach (var period in periods)
            {
                log.Periods.Add(new PeriodDTO()
                {
                    Period = period,
                    Records = await GetRecordsByPeriodAsync(period.Id, user)
                });
            }

            log.Child = _childrensService.Get().Where(c => c.Id == user.ChildId).FirstOrDefault();

            return log;
        }

        public async Task<List<Record>> GetRecordsByPeriodAsync(string periodId, ApplicationUser user)
        {
            var child = _childrensService.Get().Where(c => c.Id == user.ChildId).FirstOrDefault();
            var period = await _periodsService.Get().Where(p => p.Id == periodId).FirstOrDefaultAsync();
            var records = new List<Record>();

            //I'm not sure why the where's don't work right...?
            if (Get().Count() > 0)
            {
                records = Get()
                    .Where(r => r.ChildId == child.Id)
                    .Where(r => r.PeriodId == period.Id)
                    .Where(r => r.Created.Date == user.CurrentView.Date)
                    .ToList();
            }

            return records;
        }

        public async Task<Record> CreateAsync(Record record, ApplicationUser user, bool saveNow = true)
        {
            return await base.CreateAsync(await ParseRecordAsync(record, user), saveNow);
        }

        public async Task<Record> UpdateAsync(Record record, ApplicationUser user, bool saveNow = true)
        {
            return await base.UpdateAsync(await ParseRecordAsync(record, user), record.Id, saveNow);
        }

        public async Task<Record> ParseRecordAsync(Record record, ApplicationUser user)
        {
            if (!string.IsNullOrEmpty(record.Name))
            {
                var existing = await Get().FirstOrDefaultAsync(r => r.Id == record.Id);

                if ((record.LeucineMilligrams == null || record.ProteinGrams != existing.ProteinGrams) && record.WeightGrams != null)
                {
                    var child = _childrensService.Get().Where(c => c.Id == user.ChildId).FirstOrDefault();

                    var multiple = record.WeightGrams / existing.WeightGrams;
                    var newProtein = multiple * record.ProteinGrams;
                    var newLeucine = newProtein * child.LeucineMultiple;

                    if(multiple == null)
                    {
                        record.LeucineMilligrams = record.ProteinGrams * child.LeucineMultiple;
                    } else
                    {
                        record.ProteinGrams = Math.Round(newProtein.Value, 2);
                        record.LeucineMilligrams = Math.Round(newLeucine.Value, 0);
                    }
                }
                else if ((record.ProteinGrams == null || record.LeucineMilligrams != existing.LeucineMilligrams) && record.WeightGrams != null)
                {
                    var child = _childrensService.Get().Where(c => c.Id == user.ChildId).FirstOrDefault();

                    var multiple = record.WeightGrams / existing.WeightGrams;
                    var newLeucine = multiple * record.LeucineMilligrams;
                    var newProtein = newLeucine / child.LeucineMultiple;

                    if (multiple == null)
                    {
                        record.ProteinGrams = record.LeucineMilligrams / child.LeucineMultiple;
                    }
                    else
                    {
                        record.ProteinGrams = Math.Round(newProtein.Value, 2);
                        record.LeucineMilligrams = Math.Round(newLeucine.Value, 0);
                    }
                }
                else if(record.WeightGrams != existing.WeightGrams)
                {
                    var child = _childrensService.Get().Where(c => c.Id == user.ChildId).FirstOrDefault();

                    var multiple = record.WeightGrams / existing.WeightGrams;
                    var newLeucine = multiple * record.LeucineMilligrams;
                    var newProtein = newLeucine / child.LeucineMultiple;

                    if (multiple == null)
                    {
                        record.ProteinGrams = record.LeucineMilligrams / child.LeucineMultiple;
                    }
                    else
                    {
                        record.ProteinGrams = Math.Round(newProtein.Value, 2);
                        record.LeucineMilligrams = Math.Round(newLeucine.Value, 0);
                    }
                }
                else if (record.ProteinGrams == null && record.LeucineMilligrams == null)
                {
                    throw new Exception("Protein grams or leucine milligrams must be set to save.");
                }
                else if (record.WeightGrams == null)
                {
                    throw new Exception("No weight is set.");
                }
            }

            return record;
        }
    }
}
