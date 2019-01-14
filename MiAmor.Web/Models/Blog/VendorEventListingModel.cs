using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using MiAmor.Core;

namespace MiAmor.Web.Models
{
    public class VendorEventListingModel
    {
        public List<VendorEventPost> Events { get; set; }
        public List<SelectListItem> AvailableSortOptions { get; set; }
        public VendorEventPagingModel VendorEventPagingModel { get; set; }
    }
}