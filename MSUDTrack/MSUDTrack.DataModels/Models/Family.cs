using System;
using System.Collections.Generic;
using System.Text;

namespace MSUDTrack.DataModels.Models
{
    public class Family : BaseModel
    {
        /// <summary>
        /// This family's last name. (e.g. Martin, or Stauffer)
        /// </summary>
        public string LastName { get; set; }

        public string KnownAs { get
            {
                return "The " + LastName + "'s";
            }
        }

        public List<Child> Children { get; set; }
    }
}
