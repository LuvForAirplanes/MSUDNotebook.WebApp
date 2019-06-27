using System;
using System.Collections.Generic;
using System.Text;

namespace MSUDTrack.DataModels.Models
{
    public class Record : BaseModel
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
        /// <summary>
        /// This food's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The amount of protein this food contains, in grams.
        /// </summary>
        public int? ProteinGrams { get; set; }

        /// <summary>
        /// The amount of leucine this food contains, in milligrams.
        /// </summary>
        public int? LeucineMilligrams { get; set; }

        /// <summary>
        /// The weight of the food consumed, in grams.
        /// </summary>
        public int? WeightGrams { get; set; }
    }
}
