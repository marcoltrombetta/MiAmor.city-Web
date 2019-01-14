using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using MiAmor.Core;

namespace MiAmor.Web.Models
{
    public class VendorReviewListingModel
    {
        public int AvgRating { get; set; }
        public List<VendorReviewModel> VendorReviewBox { get; set; }
        public List<SelectListItem> AvailableSortOptions { get; set; }
        public VendorReviewPagingModel VendorReviewPagingModel { get; set; }
    }
}