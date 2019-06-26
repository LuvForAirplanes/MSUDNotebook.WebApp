using System;
using System.Collections.Generic;
using System.Text;

namespace MSUDTrack.DataModels.Models
{
    public class Record : Food
    {
        /// <summary>
        /// The Child which consumed the food.
        /// </summary>
        public Child Child { get; set; }

        public string ChildId { get; set; }

        /// <summary>
        /// The period in which the food was consumed.
        /// </summary>
        public Period Period { get; set; }

        public string PeriodId { get; set; }
    }
}
