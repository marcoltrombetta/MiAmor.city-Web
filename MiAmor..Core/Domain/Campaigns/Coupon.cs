using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public class Coupon : Entity<int>
    {
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
        public int? CampaignPaymentId { get; set; }        
        /// <summary>
        /// Gets or sets the % the site earn from this deal
        /// </summary>
        public string CampaignDesc { get; set; }
        /// <summary>
        /// Gets or sets the real price of the campaign
        /// </summary>
        public bool? CuponWasSend { get; set; }
        /// <summary>
        /// Gets or sets the couponPrice
        /// </summary>
        public string EmailAdd { get; set; }
        /// <summary>
        /// Gets or sets the discount of the campaign
        /// </summary>
        public DateTime? EmailSendDate { get; set; }
        /// <summary>
        /// Gets or sets the price of the commition paid to the site
        /// </summary>
        public bool? IsEmailOpened { get; set; }
        /// <summary>
        /// Gets or sets the required coupons in order to execute 
        /// </summary>
        public string SmsNum { get; set; }
        /// <summary>
        /// Gets or sets the amount of each user can buy
        /// </summary>
        public bool? IsSmsOk { get; set; }
        /// <summary>
        /// Gets or sets the max coupons that can be baught
        /// </summary>
        public DateTime? SmsSendDate { get; set; }
        /// <summary>
        /// Gets or sets the status
        /// </summary>
        public string CuponNumber { get; set; }
        /// <summary>
        /// Gets or sets the amount of people baught or printed
        /// </summary>
        public Int16 CuponStatus { get; set; }
        /// <summary>
        /// Gets or sets the url of the main image
        /// </summary>
        public bool? CuponUsed { get; set; }
        /// <summary>
        /// Gets or sets the url of the small image
        /// </summary>
        public DateTime? CuponUsedDate { get; set; }
        /// <summary>
        /// Gets or sets the amount of people the vendor wishes to engage
        /// </summary>
        public string CuponUsedName { get; set; }
        /// <summary>
        /// Gets or sets weather the campaign has outside serial
        /// </summary>
        public bool IsCustomerCuponUsed { get; set; }
        /// <summary>
        /// Gets or sets the final date this campaign should run without extenssions
        /// </summary>
        public DateTime? CuponExpirationDate { get; set; }
        /// <summary>
        /// Gets or sets weather this campaign should be published on the site 
        /// </summary>
        public int ReferalAffiliateId { get; set; }
        /// <summary>
        /// Gets or sets weather affiliates should publish this campaign 
        /// </summary>
        public double? AffiliatePercent { get; set; }
        /// <summary>
        /// Gets or sets how ,uch cashback from site percent the customer should get
        /// </summary>
        public double? AffiliateSumToPay { get; set; }
        /// <summary>
        /// Gets or sets campaign parentid
        /// </summary>
        public bool? WasAffiliatePaid { get; set; }

        public DateTime AffiliatePayDate { get; set; }
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public bool? IsPresent { get; set; }

        /// Gets or sets the values indicating which employee manages this business
        public string PresentEmail { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether to show the vendor on home page
        /// </summary>
        public string PresentDesc { get; set; }
        /// <summary>
        /// Gets or sets the short description
        /// </summary>
        public bool IsSubPay { get; set; }        
        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public DateTime? SyTimeStamp { get; set; }

        public virtual Campaign Campaign { get; set; }
        
        public virtual Customer Customer { get; set; }

    }
}
