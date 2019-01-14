using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Areas.Admin.Models
{
    public class EventPostCrudModel
    {
        public int Id { get; set; }

        public int languageId { get; set; }

        /// <summary>
        /// Gets or sets the Event post title
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Gets or sets the Event post title
        /// </summary>
        public string body { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Event post comments are allowed 
        /// </summary>
        public bool allowComments { get; set; }

        /// <summary>
        /// Gets or sets the total number of comments
        /// <remarks>
        /// We use this property for performance optimization (no SQL command executed)
        /// </remarks>
        /// </summary>
        public int commentCount { get; set; }

        /// <summary>
        /// Gets or sets the Event tags
        /// </summary>
        public string tags { get; set; }

        /// <summary>
        /// Gets or sets the Event post start date and time
        /// </summary>
        public DateTime startDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the Event post end date and time
        /// </summary>
        public DateTime endDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the meta keywords
        /// </summary>
        public string metaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the meta description
        /// </summary>
        public string metaDescription { get; set; }

        /// <summary>
        /// Gets or sets the meta title
        /// </summary>
        public string metaTitle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is limited/restricted to certain stores
        /// </summary>
        public bool limitedToStores { get; set; }

        /// <summary>
        /// Gets or sets the date and time of entity creation
        /// </summary>
        public DateTime createdOnUtc { get; set; }
    }
}