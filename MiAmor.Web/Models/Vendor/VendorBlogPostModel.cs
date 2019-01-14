using MiAmor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Models
{
    public class VendorBlogPostModel
    {
        public int VendorId { get; set; }

        public int BlogPostId { get; set; }

        public virtual BlogPost BlogPost { get; set; }

       // public virtual Vendor Vendor { get; set; }
    }
}