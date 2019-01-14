using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Areas.Admin.Models
{
    public class VendorCategoryCountCrudModel
    {
        public int Id { get; set; }

        public string name { get; set; }      

        public int vendorCategoryId { get; set; }

        public int countVendors { get; set; }

        public string cssClass { get; set; }

        public int displayOrder { get; set; }
    }
}