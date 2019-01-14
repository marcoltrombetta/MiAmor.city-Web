using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Areas.Admin.Models
{
    public class ProductReviewCrudModel
    {
        public int Id { get; set; }

        public int customerId { get; set; }

        public int productId { get; set; }

        public bool isApproved { get; set; }

        public string title { get; set; }

        public string reviewText { get; set; }

        public int rating { get; set; }

        public int helpfulYesTotal { get; set; }

        public int helpfulNoTotal { get; set; }

        public CustomerCrudModel Customer { get; set; }

        public ProductCrudModel Product { get; set; }
        
    }
}