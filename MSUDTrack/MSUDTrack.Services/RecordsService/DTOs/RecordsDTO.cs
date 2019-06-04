using MSUDTrack.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSUDTrack.Services.DTOs
{
    public class RecordsDTO
    {
        public List<PeriodDTO> Periods = new List<PeriodDTO>();
    }

    public class PeriodDTO
    {
       public  Period Period { get; set; }

        public List<Record> Records { get; set; }
            
        /// Did you get your drone?
        ///no... why? Just curious . thought maybe we could fly together sometime. That would be really cool! I actually thought about that. :)
        ///I'd really enjoy it. Gotta run to a meeting now... ok, see you.
    }
}
