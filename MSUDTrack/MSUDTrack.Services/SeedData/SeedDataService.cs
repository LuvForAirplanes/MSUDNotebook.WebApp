using System;
using System.Collections.Generic;
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

        public async Task SeedPeriodsAsync()
        {

        }
    }
}
