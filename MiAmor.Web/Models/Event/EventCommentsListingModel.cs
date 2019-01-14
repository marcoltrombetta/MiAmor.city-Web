﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using MiAmor.Core;

namespace MiAmor.Web.Models
{
    public class EventCommentsListingModel
    {
        public List<EventComment> EventComments { get; set; }
        public List<SelectListItem> AvailableSortOptions { get; set; }
        public EventPagingModel EventPagingModel { get; set; }
    }
}