using System;
using System.Linq;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Data;
using System.Collections.Generic;

namespace MiAmor.Services
{
    public interface IBlogPostTagBlogPostService : IEntityService<BlogPostTag>
    {
        BlogPostTag GetById(int Id);

    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class BlogPostTagBlogPostService : EntityService<BlogPostTag>, IBlogPostTagBlogPostService
    {
        #region Fields
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public BlogPostTagBlogPostService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<BlogPostTag>();

        }

        #endregion

        #region Methods

     
        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public BlogPostTag GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        #endregion
        
    }

}
