using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiAmor;

namespace MiAmor.Core
{
    public class Vendor :  Entity<int>
    {

        private ICollection<VendorCategoryVendor> _VendorCategoryVendors;
        private List<VendorAddress> _Addresses;
        private List<VendorPicture> _VendorPictures;
        private ICollection<Campaign> _Campaigns;
        private ICollection<VendorReview> _VendorReviews;
        private List<CustomerBookmark> _VendorCustomerBookmarks;
        private List<ProductVendor> _ProductVendors;

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// Gets or sets the values indicating which employee manages this business
        public int ManagerId { get; set; }

        /// <summary>
        /// Gets or sets the SiteUrl
        /// </summary>
        public string SiteUrl { get; set; }

        /// <summary>
        /// Gets or sets the VendorContactPersonId
        /// </summary>
        public int VendorContactPersonId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show the vendor on home page
        /// </summary>
        public bool ShowOnHomePage { get; set; }

        /// <summary>
        /// Gets or sets the short description
        /// </summary>
        public string ShortDescription { get; set; }
        /// <summary>
        /// Gets or sets the full description
        /// </summary>
        public string FullDescription { get; set; }

        /// <summary>
        /// Gets or sets the admin comment
        /// </summary>
        public string AdminComment { get; set; }

        /// <summary>
        /// Gets or sets a value of used vendor template identifier
        /// </summary>
        public int VendorTemplateId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the entity is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is Confirmed
        /// </summary>
        public bool Confirmed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is Published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the meta keywords
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the meta description
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the meta title
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// Gets or sets the logo
        /// </summary>
        public string LogoImgPath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether billing is allowed to this country
        /// </summary>
        public bool AllowsBilling { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the vendor allows customer reviews
        /// </summary>
        public bool AllowCustomerReviews { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Vendor Has Branches
        /// </summary>
        public bool HasBranches { get; set; }

        /// <summary>
        /// Gets or sets the rating sum (approved reviews)
        /// </summary>
        public int ApprovedRatingSum { get; set; }
        /// <summary>
        /// Gets or sets the rating sum (not approved reviews)
        /// </summary>
        public int NotApprovedRatingSum { get; set; }
        /// <summary>
        /// Gets or sets the total rating votes (approved reviews)
        /// </summary>
        public int ApprovedTotalReviews { get; set; }
        /// <summary>
        /// Gets or sets the total rating votes (not approved reviews)
        /// </summary>
        public int NotApprovedTotalReviews { get; set; }

        public DateTime? Founded { get; set; }

        public string LegalEntity { get; set; }

        public int? Turnover { get; set; }

        public Int16? NumOfEmployees { get; set; }

        ///// <summary>
        ///// Gets or sets the VendorOpeningHoursId
 
        ///// </summary>
        //public virtual VendorContactPerson VendorContactPerson  { get; set; }
        ///// <summary>
        ///// Gets or sets the VendorCategories
        ///// </summary>
        public virtual ICollection<VendorCategoryVendor> VendorCategoryVendors
        {
            get { return _VendorCategoryVendors ?? (_VendorCategoryVendors = new List<VendorCategoryVendor>()); }
            set { _VendorCategoryVendors = value; }
        }

        ///// <summary>
        ///// Gets or sets the VendorAddresses
        ///// </summary>
        public virtual List<VendorAddress> Addresses
        {
            get { return _Addresses ?? (_Addresses = new List<VendorAddress>()); }
            set { _Addresses = value; }
        }
        public virtual VendorOpeningHours VendorOpeningHours { get; set; }

        public virtual List<VendorPicture> VendorPictures
        {
            get { return _VendorPictures ?? (_VendorPictures = new List<VendorPicture>()); }
            set { _VendorPictures = value; }
        }
        public ICollection<Campaign> Campaigns
        {
            get { return _Campaigns ?? (_Campaigns = new List<Campaign>()); }
            set { _Campaigns = value; }
        }
        public ICollection<VendorReview> VendorReviews
        {
            get { return _VendorReviews ?? (_VendorReviews = new List<VendorReview>()); }
            set { _VendorReviews = value; }
        }

        public virtual List<CustomerBookmark> VendorCustomerBookmarks
        {
            get { return _VendorCustomerBookmarks ?? (_VendorCustomerBookmarks = new List<CustomerBookmark>()); }
            set { _VendorCustomerBookmarks = value; }
        }

        public virtual List<ProductVendor> ProductVendors
        {
            get { return _ProductVendors ?? (_ProductVendors = new List<ProductVendor>()); }
            set { _ProductVendors = value; }
        }

    }
}
