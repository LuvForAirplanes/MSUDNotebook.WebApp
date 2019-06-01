using Microsoft.AspNetCore.Identity;

namespace MSUDTrack.DataModels.Models
{
    /// <summary>
    /// System login user credentials and licensing information.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base() { }

        public ApplicationUser(string userName) : base(userName) { }
        /// <summary>
        /// The users preference of a theme for Candle.
        /// </summary>
        public string Theme { get; set; }
        ///// <summary>
        ///// Id of the <see cref="Models.Contact"/> associated with this user.
        ///// </summary>
        //public string ContactId { get; set; }
        ///// <summary>
        ///// Id of the <see cref="Models.LicenseKey"/> associated with this user.
        ///// </summary>
        //public string LicenseKeyId { get; set; }

        //public Contact Contact { get; set; }
        //public Key LicenseKey { get; set; }
    }
}
