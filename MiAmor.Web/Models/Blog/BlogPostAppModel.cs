using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiAmor.Core;

namespace MiAmor.Web.Models
{
    public class BlogPostAppModel
    {

        public string Title { get; set; }
       

        public DateTime? StartDateUtc { get; set; }

        public ICollection<BlogPostPicture> BlogPostPictures { get; set; }

        public string Body { get; set; }

        public ICollection<BlogComment> BlogComments { get; set; }
    }
}