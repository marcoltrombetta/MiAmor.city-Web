namespace MiAmor.Core
{
    /// <summary>
    /// Represents a product category mapping
    /// </summary>
    public partial class ProductCategoryProduct : Entity<int>
    {
        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the category identifier
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the product is featured
        /// </summary>
        public bool IsFeaturedProduct { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }
        
        /// <summary>
        /// Gets the category
        /// </summary>
        public virtual ProductCategory Category { get; set; }

        /// <summary>
        /// Gets the product
        /// </summary>
        public virtual ProductVendor Product { get; set; }

    }

}
