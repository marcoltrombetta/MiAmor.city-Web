using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Areas.Admin.Models
{
    public class CampaignPaymentsCrudModel
    {
        public int Id { get; set; }

        public int customerId { get; set; }
        /// <summary>
        /// Gets or sets the user who created the campaign
        /// </summary>
        public int campaignId { get; set; }


        /// <summary>
        /// Gets or sets the bruto % of the manager handling this deal
        /// </summary>
        public Int16 statusId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the entity is active
        /// </summary>
        public bool active { get; set; }
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
        public bool isSMS { get; set; }
        /// <summary>
        /// Gets or sets the discount of the campaign
        /// </summary>
        public bool wasSMSSend { get; set; }
        /// <summary>
        /// Gets or sets the price of the commition paid to the site
        /// </summary>
        public string cardNameDesc { get; set; }
        /// <summary>
        /// Gets or sets the required coupons in order to execute 
        /// </summary>
        public string rightCCNumber { get; set; }
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
        public Int16 qtyCupon { get; set; }
        /// <summary>
        /// Gets or sets the url of the main image
        /// </summary>
        public decimal totalSum { get; set; }
        /// <summary>
        /// Gets or sets the url of the small image
        /// </summary>
        public Int16 payNumber { get; set; }
        /// <summary>
        /// Gets or sets the amount of people the vendor wishes to engage
        /// </summary>
        public Int16 currencyType { get; set; }
        /// <summary>
        /// Gets or sets weather the campaign has outside serial
        /// </summary>
        public string CVV { get; set; }
        /// <summary>
        /// Gets or sets the final date this campaign should run without extenssions
        /// </summary>
        public string identityCard { get; set; }
        /// <summary>
        /// Gets or sets weather this campaign should be published on the site 
        /// </summary>
        public bool isCardOk { get; set; }
        /// <summary>
        /// Gets or sets weather affiliates should publish this campaign 
        /// </summary>
        public string CCErrCode { get; set; }
        /// <summary>
        /// Gets or sets how ,uch cashback from site percent the customer should get
        /// </summary>
        public string deliveryTrackId { get; set; }
        /// <summary>
        /// Gets or sets campaign parentid
        /// </summary>
        public DateTime purchaseDate { get; set; }

        public DateTime payDate { get; set; }
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public Int16 cardNameId { get; set; }

        /// Gets or sets the values indicating which employee manages this business
        public string okNumber { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether to show the vendor on home page
        /// </summary>
        public string dealNumber { get; set; }
        /// <summary>
        /// Gets or sets the short description
        /// </summary>
        public Int16 dealType { get; set; }
        /// <summary>
        /// Gets or sets the full description
        /// </summary>
        public bool rememberCC { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the entity is Published
        /// </summary>
        public bool isSubPay { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public decimal personSubPay { get; set; }
        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public DateTime syTimeStamp { get; set; }
    }
}