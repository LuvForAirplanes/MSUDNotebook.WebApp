using MSUDTrack.DataModels.Models;
using SolidMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSUDTrack.WebApp.MappingProfiles
{
    public partial class RecordEditMapper : IMapper<Record, RecordEdit>
    {
        public RecordEdit Map(Record source)
        {
            return new RecordEdit()
            {
                Id = source.Id,
                Food = source.Food
            };
        }

        public Record Map(RecordEdit source)
        {
            return new Record()
            {
                Food = source.Food,
                Id = source.Id
            };
        }

        public void Map(RecordEdit source, Record target)
        {
            target.Food = source.Food;
            target.Id = source.Id;
        }

        public void Map(Record source, RecordEdit target)
        {
            target.Id = source.Id;
            target.Food = source.Food;
        }
    }
}
