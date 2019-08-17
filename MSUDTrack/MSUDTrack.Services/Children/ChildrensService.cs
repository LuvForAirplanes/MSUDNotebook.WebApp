using MSUDTrack.DataModels.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSUDTrack.Services
{
    public class ChildrensService : Repository<Child, string>
    {
        public ChildrensService(TrackerDbContext context) : base(context) { }

        public async Task<List<Child>> GetChildrenForFamilyAsync(string familyId)
        {
            return _context.Children.Where(c => c.FamilyId == familyId).ToList();
        }
    }
}
