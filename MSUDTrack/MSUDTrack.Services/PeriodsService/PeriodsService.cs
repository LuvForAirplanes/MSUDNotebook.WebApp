using MSUDTrack.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSUDTrack.Services
{
    public class PeriodsService : Repository<Period, string>
    {
        public PeriodsService(TrackerDbContext dbContext) : base(dbContext) { }
    }
}
