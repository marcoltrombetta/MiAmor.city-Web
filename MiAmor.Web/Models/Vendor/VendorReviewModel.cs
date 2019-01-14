using MiAmor.Core;
using System;
using System.Collections.Generic;

namespace MiAmor.Web.Models
{
    public class VendorReviewModel : Entity<int>
    {     
        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int VendorId { get; set; }      

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the review text
        /// </summary>
        public string ReviewText { get; set; }

        /// <summary>
        /// Review rating
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Review helpful votes total
        /// </summary>
        public int HelpfulYesTotal { get; set; }

        /// <summary>
        /// Review not helpful votes total
        /// </summary>
        public int HelpfulNoTotal { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the product
        /// </summary>
        public virtual Customer Customer { get; set; }

    }
}