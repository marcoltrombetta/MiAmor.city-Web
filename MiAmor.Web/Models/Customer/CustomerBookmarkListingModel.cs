using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using MiAmor.Core;

namespace MiAmor.Web.Models
{
    public class CustomerBookmarkListingModel
    {
        public List<CustomerBookmark> CustomerBookmark { get; set; }
        public List<SelectListItem> AvailableSortOptions { get; set; }
        public CustomerBookmarkPagingModel CustomerBookmarkPagingModel { get; set; }
    }
}