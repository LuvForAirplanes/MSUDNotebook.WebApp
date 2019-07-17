using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USDA_To_Tracker.Models
{
    public class USDA_Nutrient
    {
        public string Id { get; set; }

        public string NDB_Number { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public string Value_UOM { get; set; }
    }
}
