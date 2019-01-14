using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Areas.Admin.Models
{
    public class BlogCommentCrudModel
    {
        public int Id { get; set; }

        public int customerId { get; set; }

        public string commentText { get; set; }

        public int blogPostId { get; set; }

        public DateTime createdOnUtc { get; set; }
    }
}