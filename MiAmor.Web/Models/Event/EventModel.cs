using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiAmor.Core;
using MiAmor.Core.Domain;

namespace MiAmor.Web.Models
{
    public class EventModel
    {
        public int ParentWebSiteSettingId { get; set; }

        public int EventId { get; set; }

        public EventPost EventPost { get; set; }

        public ParentWebSiteSetting ParentWebSiteSetting { get; set; }

    }
}