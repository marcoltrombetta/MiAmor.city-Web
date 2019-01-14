using MiAmor.Core;
using System.Collections.Generic;

namespace MiAmor.Web.Models
{
    public class VendorBoxModel : Entity<int>
    {
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }
     
        /// <summary>
        /// Gets or sets the SiteUrl
        /// </summary>
        public string SiteUrl { get; set; }

        /// <summary>
        /// Gets or sets the short description
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Gets or sets the full description
        /// </summary>
        public string FullDescription { get; set; }

        /// <summary>
        /// Gets or sets the meta keywords
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the logo
        /// </summary>
        public string LogoImgPath { get; set; }

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

        public int CouponNum { get; set; }
        ///// <summary>
        ///// Gets or sets if the current customer bookmarked
        ///// </summary>
        public bool IsBookmarkedByCurrCustomer { get; set; }
        ///// <summary>
        ///// Gets or sets the VendorCategories
        ///// </summary>
        public virtual List<VendorCategoryBreadcrumbsModel> VendorCategories { get; set; }

        ///// <summary>
        ///// Gets or sets the VendorAddresses
        ///// </summary>
        public ICollection<VendorAddress> Addresses { get; set; }
        ///// <summary>
        ///// Gets or sets the VendorContactPerson 
        ///// </summary>
        //public virtual VendorContactPerson VendorContactPerson  { get; set; }

    }
}