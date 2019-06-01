using System;
using System.Collections.Generic;
using System.Text;

namespace MSUDTrack.DataModels.Models
{
    public class Child : BaseModel
    {
        /// <summary>
        /// This Child's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// This Child's birthday.
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Whether or not this child is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Indicates whether or not this child is currently being edited.
        /// </summary>
        public bool IsSelected { get; set; }
    }
}
