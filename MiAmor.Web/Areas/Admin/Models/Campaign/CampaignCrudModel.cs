using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Areas.Admin.Models
{
    public class CampaignCrudModel
    {
        public int Id { get; set; }

        public int vendorId { get; set; }

        public int openPersonId { get; set; }

        public double managerPercent { get; set; }

        public double sitePercent { get; set; }

        public double realPrice { get; set; }

        public double couponPrice { get; set; }

        public int discount { get; set; }

        public double partialPrice { get; set; }

        public int requiredCupons { get; set; }

        public int maxPerUser { get; set; }

        public int maxCupons { get; set; }

        public int camapignStatusId { get; set; }

        public int engagedInCampaign { get; set; }

        public string mainImgPath { get; set; }

        public string smallImgPath { get; set; }

        public double desiredSum { get; set; }

        public bool haSerial { get; set; }

        public DateTime finalEndDate { get; set; }

        public bool isForSite { get; set; }

        public bool isForAffiliate { get; set; }

        public double cashBackPercent { get; set; }

        public int parentId { get; set; }

        public string name { get; set; }

        public int managerId { get; set; }

        public bool showOnHomePage { get; set; }

        public string shortDescription { get; set; }

        public string fullDescription { get; set; }

        public string adminComment { get; set; }

        public bool active { get; set; }

        public bool confirmed { get; set; }

        public bool published { get; set; }

        public int displayOrder { get; set; }

        public string metaKeywords { get; set; }

        public string metaDescription { get; set; }

        public string metaTitle { get; set; }

        public string backgroundImgPath { get; set; }

        public bool allowsBilling { get; set; }

        public bool allowCustomerReviews { get; set; }

        public DateTime startDateTime { get; set; }

        public DateTime endDateTime { get; set; }

        public DateTime expirationDateTime { get; set; }

        public int campaignType { get; set; }
    }
}