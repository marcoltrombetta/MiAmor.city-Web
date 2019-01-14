using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Areas.Admin.Models
{
    public class BlogPostCrudModel
    {
        public int Id { get; set; }

        public int languageId { get; set; }

        public string title { get; set; }

        public string body { get; set; }

        public bool allowComments { get; set; }

        public int commentCount { get; set; }

        public string tags { get; set; }

        public DateTime? startDateUtc { get; set; }

        public DateTime? endDateUtc { get; set; }

        public string metaKeyWords { get; set; }

        public string metaDescription { get; set; }

        public string metaTitle { get; set; }

        public bool limitedToStores { get; set; }
    }
}