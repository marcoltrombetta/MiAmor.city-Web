using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using MiAmor.Core;

namespace MiAmor.Web.Models
{
    public class VendorCampaignListingModel
    {
        public List<CampaignModel> Campaigns { get; set; }
        public List<SelectListItem> AvailableSortOptions { get; set; }
        public VendorCampaignPagingModel VendorCampaignPagingModel { get; set; }
        public PageList PageList { get; set; }
    }
}
