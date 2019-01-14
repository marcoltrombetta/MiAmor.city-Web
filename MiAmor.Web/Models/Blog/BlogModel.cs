using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiAmor.Core;

namespace MiAmor.Web.Models
{
    public class BlogModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public int CommentCount { get; set; }

        public string Body { get; set; }

        public string Tags { get; set; }


        public DateTime? StartDateUtc { get; set; }

        public int TotalLikes { get; set; }

        public ICollection<BlogPostPicture> BlogPostPictures { get; set; }

        public ICollection<BlogPostTagBlogPost> BlogPostTagBlogPost { get; set; }
    }
}