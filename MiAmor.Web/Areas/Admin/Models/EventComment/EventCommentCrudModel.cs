using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Areas.Admin.Models
{
    public class EventCommentCrudModel
    {
        public int Id { get; set; }

        public int customerId { get; set; }

        /// <summary>
        /// Gets or sets the comment text
        /// </summary>
        public string commentText { get; set; }

        /// <summary>
        /// Gets or sets the Event post identifier
        /// </summary>
        public int eventPostId { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime createdOnUtc { get; set; }

    }
}