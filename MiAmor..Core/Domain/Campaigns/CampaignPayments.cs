using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public class CampaignPayment : Entity<int>
    {
        private IList<Coupon> _Coupons;
        private IList<CampaignDelivery> _CampaignDelivery;

        /// <summary>
        /// Gets or sets the vendor identifier
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// Gets or sets the user who created the campaign
        /// </summary>
        public int CampaignId { get; set; }

       
        /// <summary>
        /// Gets or sets the bruto % of the manager handling this deal
        /// </summary>
        public Int16? StatusId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the entity is active
        /// </summary>
        public bool? Active { get; set; }
        /// <summary>
        /// Gets or sets the % the site earn from this deal
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// Gets or sets the real price of the campaign
        /// </summary>
        public bool? WasMailSend { get; set; }
        /// <summary>
        /// Gets or sets the couponPrice
        /// </summary>
        public bool? IsSMS { get; set; }
        /// <summary>
        /// Gets or sets the discount of the campaign
        /// </summary>
        public bool? WasSMSSend { get; set; }
        /// <summary>
        /// Gets or sets the price of the commition paid to the site
        /// </summary>
        public string CardNameDesc { get; set; }
        /// <summary>
        /// Gets or sets the required coupons in order to execute 
        /// </summary>
        public string RightCCNumber { get; set; }
        /// <summary>
        /// Gets or sets the amount of each user can buy
        /// </summary>
        public string CCHolderName { get; set; }
        /// <summary>
        /// Gets or sets the max coupons that can be baught
        /// </summary>
        public string CCNumber { get; set; }
        /// <summary>
        /// Gets or sets the status
        /// </summary>
        public string CCExpirationDate { get; set; }
        /// <summary>
        /// Gets or sets the amount of people baught or printed
        /// </summary>
        public Int16? QtyCupon { get; set; }
        /// <summary>
        /// Gets or sets the url of the main image
        /// </summary>
        public decimal? TotalSum { get; set; }
        /// <summary>
        /// Gets or sets the url of the small image
        /// </summary>
        public Int16? PayNumber { get; set; }
        /// <summary>
        /// Gets or sets the amount of people the vendor wishes to engage
        /// </summary>
        public Int16? CurrencyType { get; set; }
        /// <summary>
        /// Gets or sets weather the campaign has outside serial
        /// </summary>
        public string CVV { get; set; }
        /// <summary>
        /// Gets or sets the final date this campaign should run without extenssions
        /// </summary>
        public string IdentityCard{ get; set; }
        /// <summary>
        /// Gets or sets weather this campaign should be published on the site 
        /// </summary>
        public bool? IsCardOk { get; set; }
        /// <summary>
        /// Gets or sets weather affiliates should publish this campaign 
        /// </summary>
        public string CCErrCode { get; set; }
        /// <summary>
        /// Gets or sets how ,uch cashback from site percent the customer should get
        /// </summary>
        public string DeliveryTrackId { get; set; }
        /// <summary>
        /// Gets or sets campaign parentid
        /// </summary>
        public DateTime? PurchaseDate { get; set; }

        public DateTime? PayDate { get; set; }
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public Int16? CardNameId { get; set; }

        /// Gets or sets the values indicating which employee manages this business
        public string OkNumber { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether to show the vendor on home page
        /// </summary>
        public string DealNumber { get; set; }
        /// <summary>
        /// Gets or sets the short description
        /// </summary>
        public Int16? DealType { get; set; }
        /// <summary>
        /// Gets or sets the full description
        /// </summary>
        public bool? RememberCC  { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the entity is Published
        /// </summary>
        public bool? IsSubPay { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public decimal? PersonSubPay { get; set; }
        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public DateTime? SyTimeStamp { get; set; }

        public virtual Campaign Campaign { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual IList<Coupon> Coupon
        {
            get { return _Coupons ?? (_Coupons = new List<Coupon>()); }
            set { _Coupons = value; }
        }

        public virtual IList<CampaignDelivery> CampaignDelivery
        {
            get { return _CampaignDelivery ?? (_CampaignDelivery = new List<CampaignDelivery>()); }
            set { _CampaignDelivery = value; }
        }

    }
}
