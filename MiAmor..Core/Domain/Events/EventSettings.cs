
namespace MiAmor.Core
{
    public class EventSettings : Entity<int>
    {
        /// <summary>
        /// Gets or sets a value indicating whether Event is enabled
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the page size for posts
        /// </summary>
        public int PostsPageSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether not registered user can leave comments
        /// </summary>
        public bool AllowNotRegisteredUsersToLeaveComments { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to notify about new Event comments
        /// </summary>
        public bool NotifyAboutNewEventComments { get; set; }

        /// <summary>
        /// Gets or sets a number of Event tags that appear in the tag cloud
        /// </summary>
        public int NumberOfTags { get; set; }

        /// <summary>
        /// Enable the Event RSS feed link in customers browser address bar
        /// </summary>
        public bool ShowHeaderRssUrl { get; set; }
    }
}