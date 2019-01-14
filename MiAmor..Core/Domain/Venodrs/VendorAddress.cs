using System;


namespace MiAmor.Core
{
    public partial class VendorAddress : Entity<int>
    {
      
        /// <summary>
        /// Gets or sets the country identifier
        /// </summary>
        public int? CountryId { get; set; }

        /// <summary>
        /// Gets or sets the state/province identifier
        /// </summary>
        public int? StateProvinceId { get; set; }      
        
        /// <summary>
        /// Gets or sets the city
        /// </summary>
        public int? CityId { get; set; }

        /// <summary>
        /// Gets or sets the city
        /// </summary>
        public int? NeighbourhoodId { get; set; }

        /// <summary>
        /// Gets or sets the address 1
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address 2
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the zip/postal code
        /// </summary>
        public string ZipPostalCode { get; set; }

        /// <summary>
        /// Gets or sets the phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the phone number
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the fax number
        /// </summary>
        public string FaxNumber { get; set; }
        /// <summary>
        /// Gets or sets the fax number
        /// </summary>
        public string MainEmail { get; set; }
        /// <summary>
        /// Gets or sets the custom attributes (see "AddressAttribute" entity for more info)
        /// </summary>
        public string CustomAttributes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the vendorId
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        /// Gets or sets the VendorAddressType
        /// </summary>
        public int VendorAddressTypeId { get; set; }

        /// <summary>
        /// Gets or sets the Longitude
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the Latitude
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the GoogleNeighbourhood
        /// </summary>
        public string LocalityName { get; set; }

    
            /// <summary>
        /// Gets or sets the country
        /// </summary>
        public virtual Country Country { get; set; }

        /// <summary>
        /// Gets or sets the state/province
        /// </summary>
        public virtual StateProvince StateProvince { get; set; }

        /// <summary>
        /// Gets or sets the City
        /// </summary>
        public virtual City Cities { get; set; }

        /// <summary>
        /// Gets or sets the Neighbourhood
        /// </summary>
        public virtual Neighbourhood Neighbourhood { get; set; }

        /// <summary>
        /// Gets or sets the VendorAddressType 
        /// </summary>
        public virtual VendorAddressType VendorAddressType { get; set; }
      
    }
}
