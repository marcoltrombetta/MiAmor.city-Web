namespace MiAmor.Core
{
    /// <summary>
    /// Represents a Campaign attribute
    /// </summary>
    public partial class CampaignAttribute : Entity<int>
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }
    }
}
