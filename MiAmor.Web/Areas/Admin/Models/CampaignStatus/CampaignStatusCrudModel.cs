using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Areas.Admin.Models
{
    public class CampaignStatusCrudModel
    {
        public int Id { get; set; }

        public string name { get; set; }

        public bool active { get; set; }
    }
}