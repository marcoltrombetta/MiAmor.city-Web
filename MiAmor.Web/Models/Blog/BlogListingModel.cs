using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using MiAmor.Core;

namespace MiAmor.Web.Models
{
    public class BlogListingModel
    {
        public List<BlogModel> Blogs { get; set; }
        public List<SelectListItem> AvailableSortOptions { get; set; }
        public BlogPagingModel BlogPagingModel { get; set; }
    }
}