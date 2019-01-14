

namespace MiAmor.Core
{
    /// <summary>
    /// Represents a product attribute
    /// </summary>
    public partial class ProductAttribute : Entity<int>
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
