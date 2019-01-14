using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using MiAmor.Core;

namespace MiAmor.Web.Models
{
    public class VendorBlogListingModel
    {
        public List<VendorBlogPost> Blogs { get; set; }
        public List<SelectListItem> AvailableSortOptions { get; set; }
        public VendorBlogPagingModel VendorBlogPagingModel { get; set; }

        public PageList PageList { get; set; }

    }
}