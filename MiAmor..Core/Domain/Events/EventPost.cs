using System;
using System.Collections.Generic;


namespace MiAmor.Core
{
    /// <summary>
    /// Represents a Event post
    /// </summary>
    public partial class EventPost : Entity<int>
    {
        private ICollection<EventComment> _EventComments;
        private ICollection<EventPostPicture> _EventPostPicture;
        private ICollection<EventPostCategoryEventPost> _EventPostCategoryEventPost;
        private ICollection<EventPostTagEventPost> _EventPostTagPost;
        /// <summary>
        /// Gets or sets the language identifier
        /// </summary>
        public int LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the Event post title
        /// </summary>
        public string Title { get; set; }

        public string ShortDesc { get; set; }
        /// <summary>
        /// Gets or sets the Event post title
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Event post comments are allowed 
        /// </summary>
        public bool AllowComments { get; set; }

        /// <summary>
        /// Gets or sets the total number of comments
        /// <remarks>
        /// We use this property for performance optimization (no SQL command executed)
        /// </remarks>
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// Gets or sets the Event tags
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Gets or sets the Event post start date and time
        /// </summary>
        public DateTime? StartDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the Event post end date and time
        /// </summary>
        public DateTime? EndDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the meta keywords
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the meta description
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the meta title
        /// </summary>
        public string MetaTitle { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the entity is limited/restricted to certain stores
        /// </summary>
        public virtual bool LimitedToStores { get; set; }
 
        /// <summary>
        /// Gets or sets the date and time of entity creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
        /// <summary>
        /// Gets or sets Total Likes
        /// </summary>
        public int? DisplayOrder { get; set; }
        /// <summary>
        /// Gets or sets the Event Tag
        /// </summary>
        public virtual ICollection<EventPostTagEventPost> EventPostTagEventPost
        {
            get { return _EventPostTagPost ?? (_EventPostTagPost = new List<EventPostTagEventPost>()); }
            protected set { _EventPostTagPost = value; }
        }

        /// <summary>
        /// Gets or sets the Event comments
        /// </summary>
        public ICollection<EventComment> EventComments
        {
            get { return _EventComments ?? (_EventComments = new List<EventComment>()); }
            protected set { _EventComments = value; }
        }

        /// <summary>
        /// Gets or sets the Event comments
        /// </summary>
        public virtual ICollection<EventPostPicture> EventPostPictures
        {
            get { return _EventPostPicture ?? (_EventPostPicture = new List<EventPostPicture>()); }
            protected set { _EventPostPicture = value; }
        }

        public virtual ICollection<EventPostCategoryEventPost> EventPostCategoryEventPost
        {
            get { return _EventPostCategoryEventPost ?? (_EventPostCategoryEventPost = new List<EventPostCategoryEventPost>()); }
            protected set { _EventPostCategoryEventPost = value; }
        }

        /// <summary>
        /// Gets or sets the language
        /// </summary>
        public virtual Language Language { get; set; }
    }
}