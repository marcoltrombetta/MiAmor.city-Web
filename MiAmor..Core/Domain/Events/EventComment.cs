using System;


namespace MiAmor.Core
{
    /// <summary>
    /// Represents a Event comment
    /// </summary>
    public partial class EventComment : Entity<int>
    {
        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the comment text
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// Gets or sets the Event post identifier
        /// </summary>
        public int EventPostId { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the customer
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the Event post
        /// </summary>
        public virtual EventPost EventPost { get; set; }
    }
}