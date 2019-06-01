using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MSUDTrack.Services;

namespace Candle.Services
{
    public class TrackerDbContextFactory : IDesignTimeDbContextFactory<TrackerDbContext>
    {
        public TrackerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TrackerDbContext>();
            optionsBuilder.UseNpgsql("IDesignTime");

            return new TrackerDbContext(optionsBuilder.Options);
        }
    }
}
