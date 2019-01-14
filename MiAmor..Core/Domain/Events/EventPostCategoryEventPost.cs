using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public class EventPostCategoryEventPost : Entity<int>
    {
        /// <summary>
        /// Gets or sets the EventPost identifier
        /// </summary>
        public int EventPostId { get; set; }

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
        /// Gets the EventPost
        /// </summary>
        public virtual EventPost EventPost { get; set; }
    }
}
