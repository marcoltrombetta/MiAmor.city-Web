using System;
using System.Linq;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Data;
using System.Collections.Generic;

namespace MiAmor.Services
{
    public interface IBlogPostTagService : IEntityService<BlogPostTag>
    {
        BlogPostTag GetById(int Id);
        PagedDataResponse<BlogPostTag> GetFilteredBlogPostTags(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions);
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class BlogPostTagService : EntityService<BlogPostTag>, IBlogPostTagService
    {
        #region Fields
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public BlogPostTagService(IContext context)
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

        public PagedDataResponse<BlogPostTag> GetFilteredBlogPostTags(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            List<BlogPostTag> BlogPostTags;
            int TotalItems = 0;
            if (FilterElements != null)
            {
                IQueryable<BlogPostTag> query = _dbset;
                foreach (FilterKeyValue filter in FilterElements)
                {
                    if (filter.MyKey != null && filter.MyValue != null)
                    {
                        switch (filter.MyKey)
                        {
                            case "Id":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.Id).Contains(filter.MyValue));
                                break;
                            case "Name":
                                query = query.Where(c => c.Name.Contains(filter.MyValue));
                                break;
                            case "BlogPostCount":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.BlogPostCount).Contains(filter.MyValue));
                                break;
                            case "CssName":
                                query = query.Where(c => c.CssName.Contains(filter.MyValue));
                                break;
                            default:
                                BlogPostTags = (from c in _dbset
                                             select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                                break;
                        }
                    }
                }
                query = query
                    .OrderBy(c => c.Id)
                    .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize);
                BlogPostTags = query.ToList();
                TotalItems = query.Count();
            }
            else
            {
                BlogPostTags = (from c in _dbset
                             select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                TotalItems = _dbset.Count();
            }
            PagedDataResponse<BlogPostTag> PagedDataResponse = new PagedDataResponse<BlogPostTag>(TotalItems, BlogPostTags.Count(), BlogPostTags, 0);
            return PagedDataResponse;
        }

        #endregion
        
    }

}
