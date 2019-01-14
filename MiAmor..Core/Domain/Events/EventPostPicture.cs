

namespace MiAmor.Core
{
    /// <summary>
    /// Represents a EventPost picture mapping
    /// </summary>
    public partial class EventPostPicture : Entity<int>
    {
        /// <summary>
        /// Gets or sets the EventPost identifier
        /// </summary>
        public int EventPostId { get; set; }

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
        /// Gets the EventPost
        /// </summary>
        public virtual EventPost EventPost { get; set; }
    }

}
