

namespace MiAmor.Core
{
    /// <summary>
    /// Represents a BlogPost picture mapping
    /// </summary>
    public partial class BlogPostPicture : Entity<int>
    {
        /// <summary>
        /// Gets or sets the BlogPost identifier
        /// </summary>
        public int BlogPostId { get; set; }

        /// <summary>
        /// Gets or sets the picture identifier
        /// </summary>
        public int PictureId { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int? DisplayOrder { get; set; }

        /// <summary>
        /// Gets the picture
        /// </summary>
        public virtual Picture Picture { get; set; }

        /// <summary>
        /// Gets the BlogPost
        /// </summary>
        public virtual BlogPost BlogPost { get; set; }
    }

}
