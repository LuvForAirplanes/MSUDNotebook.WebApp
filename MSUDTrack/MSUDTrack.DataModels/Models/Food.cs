using System;
using System.Collections.Generic;
using System.Text;

namespace MSUDTrack.DataModels.Models
{
    public class Food : BaseModel
    {
        /// <summary>
        /// This food's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The amount of protein this food contains, in grams.
        /// </summary>
        public double? ProteinGrams { get; set; }

        /// <summary>
        /// The amount of leucine this food contains, in milligrams.
        /// </summary>
        public double? LeucineMilligrams { get; set; }

        /// <summary>
        /// The weight of the food consumed, in grams.
        /// </summary>
        public double? WeightGrams { get; set; }

        /// <summary>
        /// The Manufacturer of this food.
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// The last time this food was used.
        /// </summary>
        public DateTime LastUsed { get; set; }

        /// <summary>
        /// How many times this food was used.
        /// </summary>
        public int TimesUsed { get; set; }
    }
}
