using Microsoft.EntityFrameworkCore;
using MSUDTrack.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSUDTrack.Services.SeedData
{
    public class SeedDataService
    {
        public readonly TrackerDbContext _trackerDbContext;

        public SeedDataService(TrackerDbContext trackerDbContext)
        {
            _trackerDbContext = trackerDbContext;
        }

        public void SeedPeriodsAsync()
        {
            _trackerDbContext.Database.Migrate();

            if (_trackerDbContext.Period.Any())
                return;

            var records = new Period[]
            {
                //Jan 1, 2000 @ 12:00 AM - 9:00 AM
                new Period { Id = Guid.NewGuid().ToString(), Created = DateTime.Now, Name = "Breakfast", PeriodStart = new DateTime(2000, 1, 1, 1, 0, 0 ), PeriodEnd = new DateTime(2000, 1, 1, 9, 0, 0 ) },
                //Jan 1, 2000 @ 9:00 AM - 1:00 PM
                new Period { Id = Guid.NewGuid().ToString(), Created = DateTime.Now, Name = "Lunch", PeriodStart = new DateTime(2000, 1, 1, 9, 0, 0 ), PeriodEnd = new DateTime(2000, 1, 1, 13, 0, 0 ) },
                //Jan 1, 2000 @ 1:00 PM - 4:00 PM
                new Period { Id = Guid.NewGuid().ToString(), Created = DateTime.Now, Name = "Snack", PeriodStart = new DateTime(2000, 1, 1, 13, 0, 0 ), PeriodEnd = new DateTime(2000, 1, 1, 16, 0, 0 ) },
                //Jan 1, 2000 @ 4:00 PM - 7:00 PM
                new Period { Id = Guid.NewGuid().ToString(), Created = DateTime.Now, Name = "Supper", PeriodStart = new DateTime(2000, 1, 1, 16, 0, 0 ), PeriodEnd = new DateTime(2000, 1, 1, 19, 0, 0 ) },
                //Jan 1, 2000 @ 7:00 PM - 12:00 AM
                new Period { Id = Guid.NewGuid().ToString(), Created = DateTime.Now, Name = "Bedtime Snack", PeriodStart = new DateTime(2000, 1, 1, 19, 0, 0 ), PeriodEnd = new DateTime(2000, 1, 1, 23, 0, 0 ) }
            };

            _trackerDbContext.Period.AddRange(records);
            _trackerDbContext.SaveChanges();
        }
    }
}
