using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Areas.Admin.Models
{
    public class ManufacturerCrudModel
    {
        public int Id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string metaKeywords { get; set; }

        public string metaDescription { get; set; }

        public string metaTitle { get; set; }

        public int pageSize { get; set; }

        public bool published { get; set; }

        public int displayOrder { get; set; }


    }
}