using MSUDTrack.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSUDTrack.Services.DTOs
{
    public class ReturnRecord : Record
    {
        public int LuecineCount { get; set; }

        public int LeucineLeft { get; set; }
    }
}
