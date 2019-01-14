using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Areas.Admin.Models
{
    public class VendorCrudModel
    {
        public int Id { get; set; }

        public string name { get; set; }

        public int managerId { get; set; }

        public string siteUrl { get; set; }

        public int vendorContactPersonId { get; set; }

        public string shortDescription { get; set; }

        public string adminComment { get; set; }

        public string fullDescription { get; set; }

        public List<VendorAddressCrudModel> VendorAddressCrudModel { get; set; }
       
    }
}