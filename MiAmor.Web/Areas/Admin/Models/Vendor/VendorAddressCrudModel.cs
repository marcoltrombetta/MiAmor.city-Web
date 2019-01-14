using System;
using System.Collections.Generic;
using System.Linq;
using MiAmor.Core;
using System.Web;

namespace MiAmor.Web.Areas.Admin.Models
{
    public class VendorAddressCrudModel : Entity<int>
    {
        /// <summary>
        /// Gets or sets the city
        /// </summary>
        public int? cityId { get; set; }

        /// <summary>
        /// Gets or sets the city
        /// </summary>
        public int? neighbourhoodId { get; set; }

        /// <summary>
        /// Gets or sets the address 1
        /// </summary>
        public string address1 { get; set; }

        /// <summary>
        /// Gets or sets the address 2
        /// </summary>
        public string address2 { get; set; }

        /// <summary>
        /// Gets or sets the zip/postal code
        /// </summary>
        public string zipPostalCode { get; set; }

        /// <summary>
        /// Gets or sets the phone number
        /// </summary>
        public string phoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the phone number
        /// </summary>
        public string mobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the fax number
        /// </summary>
        public string faxNumber { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool deleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is active
        /// </summary>
        public bool active { get; set; }
        /// <summary>
        /// Gets or sets the Longitude
        /// </summary>
        public double? longitude { get; set; }

        /// <summary>
        /// Gets or sets the Latitude
        /// </summary>
        public double? latitude { get; set; }

        public int countryId { get; set; }
    }
}