using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public class BlogPostTagBlogPost : Entity<int>
    {
        /// <summary>
        /// Gets or sets the BlogPost identifier
        /// </summary>
        public int BlogPostId { get; set; }

        /// <summary>
        /// Gets or sets the category identifier
        /// </summary>
        public int BlogPostTagId { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets the category
        /// </summary>
        public virtual BlogPostTag BlogPostTag { get; set; }

        /// <summary>
        /// Gets the BlogPost
        /// </summary>
        public virtual BlogPost BlogPost { get; set; }
    }
}
