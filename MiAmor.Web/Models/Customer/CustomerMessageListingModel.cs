using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using MiAmor.Core;

namespace MiAmor.Web.Models
{
    public class CustomerMessageListingModel
    {

        public List<CustomerMessageModel> CustomerMessageModel { get; set; }
        public List<SelectListItem> AvailableSortOptions { get; set; }
        public CustomerMessagePagingModel CustomerMessagePagingModel { get; set; }
    }
}