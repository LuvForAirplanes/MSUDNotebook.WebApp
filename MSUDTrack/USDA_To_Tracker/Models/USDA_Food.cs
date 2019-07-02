using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USDA_To_Tracker.Models
{
    public class USDA_Food
    {
        public string Id { get; set; }

        public string NDB_Number { get; set; }

        public string Name { get; set; }

        public string UPC { get; set; }
    }
}
