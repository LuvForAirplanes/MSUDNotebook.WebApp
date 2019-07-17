using MSUDTrack.DataModels.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MSUDTrack.Services
{
    public class ChildrensService : Repository<Child, string>
    {
        public ChildrensService(TrackerDbContext context) : base(context) { }

        public Child GetCurrentChild()
        {
            return Get().FirstOrDefault(c => c.IsSelected);
        }
    }
}
