namespace MiAmor.Core
{
    /// <summary>
    /// Represents a blog post tag
    /// </summary>
    public partial class BlogPostTag : Entity<int>
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the tagged product count
        /// </summary>
        public int BlogPostCount { get; set; }

        /// <summary>
        /// Gets or sets the CssName
        /// </summary>
        public string CssName { get; set; }
    }
}