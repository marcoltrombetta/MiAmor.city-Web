using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public class Campaign : Entity<int>
    {

        private IList<CampaignPicture> _CampaignPictures;

        private IList<CampaignDelivery> _CampaignDelivery;

        private IList<CampaignPayment> _CampaignPayments;

        private IList<Coupon> _Coupons;

        private IList<CampaignAttributeMapping> _CampaignAttributeMapping;

        /// <summary>
        /// Gets or sets the vendor identifier
        /// </summary>
        public int? VendorId { get; set; }
        /// <summary>
        /// Gets or sets the user who created the campaign
        /// </summary>
        public int? OpenPersonId { get; set; }
        /// <summary>
        /// Gets or sets the bruto % of the manager handling this deal
        /// </summary>
        public double? ManagerPercent { get; set; }
        /// <summary>
        /// Gets or sets the % the site earn from this deal
        /// </summary>
        public double? SitePercent { get; set; }
        /// <summary>
        /// Gets or sets the real price of the campaign
        /// </summary>
        public double? RealPrice { get; set; }
        /// <summary>
        /// Gets or sets the couponPrice
        /// </summary>
        public double? CouponPrice { get; set; }
        /// <summary>
        /// Gets or sets the discount of the campaign
        /// </summary>
        public int? Discount { get; set; }
        /// <summary>
        /// Gets or sets the price of the commition paid to the site
        /// </summary>
        public double? PartialPrice { get; set; }
        /// <summary>
        /// Gets or sets the required coupons in order to execute 
        /// </summary>
        public int? RequiredCupons { get; set; }
        /// <summary>
        /// Gets or sets the amount of each user can buy
        /// </summary>
        public int? MaxPerUser { get; set; }
        /// <summary>
        /// Gets or sets the max coupons that can be baught
        /// </summary>
        public int? MaxCupons { get; set; }
        /// <summary>
        /// Gets or sets the status
        /// </summary>
        public Int16? CamapignStatusId { get; set; }
        /// <summary>
        /// Gets or sets the amount of people baught or printed
        /// </summary>
        public int? EngagedInCampaign { get; set; }
        /// <summary>
        /// Gets or sets the url of the main image
        /// </summary>
        public string MainImgPath { get; set; }
        /// <summary>
        /// Gets or sets the url of the small image
        /// </summary>
        public string SmallImgPath { get; set; }
        /// <summary>
        /// Gets or sets the amount of people the vendor wishes to engage
        /// </summary>
        public double? DesiredSum { get; set; }
        /// <summary>
        /// Gets or sets weather the campaign has outside serial
        /// </summary>
        public bool? HaSerial { get; set; }
        /// <summary>
        /// Gets or sets the final date this campaign should run without extenssions
        /// </summary>
        public DateTime? FinalEndDate { get; set; }
        /// <summary>
        /// Gets or sets weather this campaign should be published on the site 
        /// </summary>
        public bool? IsForSite { get; set; }
        /// <summary>
        /// Gets or sets weather affiliates should publish this campaign 
        /// </summary>
        public bool? IsForAffiliate { get; set; }
        /// <summary>
        /// Gets or sets how ,uch cashback from site percent the customer should get
        /// </summary>
        public double? CashBackPercent { get; set; }
        /// <summary>
        /// Gets or sets campaign parentid
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// Gets or sets the values indicating which employee manages this business
        public int ManagerId { get; set; }
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
        /// Gets or sets a value indicating whether the entity is active
        /// </summary>
        public bool? Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is Confirmed
        /// </summary>
        public bool? Confirmed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is Published
        /// </summary>
        public bool? Published { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool? Deleted { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int? DisplayOrder { get; set; }

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
        public string BackgroundImgPath { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether billing is allowed to this country
        /// </summary>
        public bool? AllowsBilling { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the vendor allows customer reviews
        /// </summary>
        public bool? AllowCustomerReviews { get; set; }
        /// <summary>
        /// Gets or sets the rating sum (approved reviews)
        /// </summary>
        public int? ApprovedRatingSum { get; set; }
        /// <summary>
        /// Gets or sets the rating sum (not approved reviews)
        /// </summary>
        public int? NotApprovedRatingSum { get; set; }
        /// <summary>
        /// Gets or sets the total rating votes (approved reviews)
        /// </summary>
        public int? ApprovedTotalReviews { get; set; }
        /// <summary>
        /// Gets or sets the total rating votes (not approved reviews)
        /// </summary>
        public int? NotApprovedTotalReviews { get; set; }
        /// <summary>
        /// Gets or sets the StartDateTime
        /// </summary>
        public DateTime? StartDateTime { get; set; }
        /// <summary>
        /// Gets or sets the EndDateTime
        /// </summary>
        public DateTime? EndDateTime { get; set; }

        public DateTime ExpirationDateTime { get; set; }

        public int? CampaignType { get; set; }

        public virtual Vendor Vendor { get; set; }

        public virtual IList<CampaignPicture> CampaignPictures
        {
            get { return _CampaignPictures ?? (_CampaignPictures = new List<CampaignPicture>()); }
            set { _CampaignPictures = value; }
        }

        public virtual IList<CampaignDelivery> CampaignDelivery
        {
            get { return _CampaignDelivery ?? (_CampaignDelivery = new List<CampaignDelivery>()); }
            set { _CampaignDelivery = value; }
        }

        public virtual IList<Coupon> Coupon
        {
            get { return _Coupons ?? (_Coupons = new List<Coupon>()); }
            set { _Coupons = value; }
        }

       

    }
}
