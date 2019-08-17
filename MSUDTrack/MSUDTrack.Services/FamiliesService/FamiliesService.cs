using MSUDTrack.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSUDTrack.Services.FamiliesService
{
    public class FamiliesService : Repository<Family, string>
    {
        public readonly TrackerDbContext context;

        public FamiliesService(TrackerDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
