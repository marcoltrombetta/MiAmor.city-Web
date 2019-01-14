namespace MiAmor.Core
{
    /// <summary>
    /// Represents a blog post tag
    /// </summary>
    public partial class EventPostTag : Entity<int>
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the tagged product count
        /// </summary>
        public int EventPostCount { get; set; }
    }
}