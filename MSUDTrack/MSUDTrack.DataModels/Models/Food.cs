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
        public string ProteinGrams { get; set; }

        /// <summary>
        /// The amount of leucine this food contains, in milligrams.
        /// </summary>
        public string LeucineMilligrams { get; set; }
    }
}
