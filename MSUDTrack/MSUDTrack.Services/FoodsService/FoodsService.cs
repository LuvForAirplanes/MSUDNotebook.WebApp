using MSUDTrack.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSUDTrack.Services
{
    public class FoodsService : Repository<Food, string>
    {
        public FoodsService(TrackerDbContext dbContext) : base(dbContext) { }
    }
}
