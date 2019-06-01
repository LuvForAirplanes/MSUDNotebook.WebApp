using MSUDTrack.DataModels.Models;

namespace MSUDTrack.Services.Children
{
    public class ChildrensService : Repository<Child, string>
    {
        public ChildrensService(TrackerDbContext context) : base(context) { }
    }
}
