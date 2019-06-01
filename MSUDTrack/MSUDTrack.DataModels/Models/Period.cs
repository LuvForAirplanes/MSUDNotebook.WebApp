using System;

namespace MSUDTrack.DataModels.Models
{
    public class Period : BaseModel
    {
        /// <summary>
        /// This period's name. (e.g. Breakfast, Lunch, Snack, Supper, Bedtime Snack)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The time at which this period begins.
        /// </summary>
        public DateTime PeriodStart { get; set; }

        /// <summary>
        /// The time at which this period ends.
        /// </summary>
        public DateTime PeriodEnd { get; set; }
    }
}
