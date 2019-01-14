using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using MiAmor.Core;

namespace MiAmor.Web.Models
{
    public class VendorSearchLisitingModel: Entity<int>
    {
        public List<VendorBoxModel> VendorBox { get; set; }
        public List<SelectListItem> AvailableSortOptions { get; set; }
        public List<VendorCategoryBreadcrumbsModel> VendorCategoryBreadcrumbs { get; set; }
        public PageList PageList { get; set; }
    }
}