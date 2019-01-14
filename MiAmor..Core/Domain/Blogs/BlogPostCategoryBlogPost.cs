using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public class BlogPostCategoryBlogPost : Entity<int>
    {
        /// <summary>
        /// Gets or sets the BlogPost identifier
        /// </summary>
        public int BlogPostId { get; set; }

        /// <summary>
        /// Gets or sets the category identifier
        /// </summary>
        public int CategoryId { get; set; }
       
        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets the category
        /// </summary>
        public virtual VendorCategory Category { get; set; }

        /// <summary>
        /// Gets the BlogPost
        /// </summary>
        public virtual BlogPost BlogPost { get; set; }
    }
}
