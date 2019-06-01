using System;
using System.Collections.Generic;
using System.Text;

namespace MSUDTrack.DataModels
{
    public class BaseModel
    {
        public string Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastEdited { get; set; }
    }
}
