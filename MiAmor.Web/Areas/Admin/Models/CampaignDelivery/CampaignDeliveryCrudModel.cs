using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Areas.Admin.Models
{
    public class CampaignDeliveryCrudModel
    {
        public int Id { get; set; }

        public int CampaignId { get; set; }

        public string description { get; set; }

        public double deliveryPrice { get; set; }

        public int maxUnitsPerDelivery { get; set; }

        public bool active { get; set; }
    }
}