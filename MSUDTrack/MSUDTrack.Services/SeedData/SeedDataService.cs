using Microsoft.EntityFrameworkCore;
using MSUDTrack.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSUDTrack.Services
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
                //Jan 1, 2000 @ 12:00 AM - 12:00 PM
                new Period { Id = Guid.NewGuid().ToString(), Created = DateTime.Now, Name = "Morning", PeriodStart = new DateTime(2000, 1, 1, 0, 0, 0 ), PeriodEnd = new DateTime(2000, 1, 1, 11, 0, 0 ) },
                //Jan 1, 2000 @ 12:00 PM - 5:00 PM
                new Period { Id = Guid.NewGuid().ToString(), Created = DateTime.Now, Name = "Afternoon", PeriodStart = new DateTime(2000, 1, 1, 11, 0, 0 ), PeriodEnd = new DateTime(2000, 1, 1, 16, 0, 0 ) },
                //Jan 1, 2000 @ 1:00 PM - 4:00 PM
                new Period { Id = Guid.NewGuid().ToString(), Created = DateTime.Now, Name = "Evening", PeriodStart = new DateTime(2000, 1, 1, 16, 0, 0 ), PeriodEnd = new DateTime(2000, 1, 1, 23, 0, 0 ) },
            };

            _trackerDbContext.Period.AddRange(records);
            _trackerDbContext.SaveChanges();
        }
    }
}
