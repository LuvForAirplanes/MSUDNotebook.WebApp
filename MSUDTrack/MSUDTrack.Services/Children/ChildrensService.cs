using MSUDTrack.DataModels.Models;

namespace MSUDTrack.Services
{
    public class ChildrensService : Repository<Child, string>
    {
        public ChildrensService(TrackerDbContext context) : base(context) { }
    }
}
