using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiAmor.Core;
using MiAmor.Data;
using System.Data.Entity;

namespace MiAmor.Services
{
    public interface IBlogCommentService : IEntityService<BlogComment>
    {
        BlogComment GetById(int Id);
        PagedDataResponse<BlogComment> GetFilteredBlogComments(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions);
        PagedDataResponse<BlogComment> GetCommentsByBlogPostId(PagedDataRequest PagedDataRequest, int Id);

    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class BlogCommentService : EntityService<BlogComment>, IBlogCommentService
    {
        #region Fields
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public BlogCommentService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<BlogComment>();

        }

        #endregion

        #region Methods

     
        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public BlogComment GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public PagedDataResponse<BlogComment> GetCommentsByBlogPostId(PagedDataRequest PagedDataRequest, int Id)
        {
            List<BlogComment> ListBlog = (from pb in _dbset
                                          .Include(pb => pb.Customer)
                                          where pb.BlogPostId==Id
                                          orderby pb.CreatedOnUtc
                                          select pb).Skip(PagedDataRequest.RowsPerPage * (PagedDataRequest.PageNumber - 1)).Take(PagedDataRequest.RowsPerPage).ToList();

            int TotalItems = 0;
            PagedDataResponse<BlogComment> PagedDataResponse = new PagedDataResponse<BlogComment>(TotalItems, ListBlog.Count(), ListBlog);
            return PagedDataResponse;
        }

        public PagedDataResponse<BlogComment> GetFilteredBlogComments(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            List<BlogComment> BlogComments;
            int TotalItems = 0;
            if (FilterElements != null)
            {
                IQueryable<BlogComment> query = _dbset;
                foreach (FilterKeyValue filter in FilterElements)
                {
                    if (filter.MyKey != null && filter.MyValue != null)
                    {
                        switch (filter.MyKey)
                        {
                            case "Id":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.Id).Contains(filter.MyValue));
                                break;
                            case "CustomerId":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.CustomerId).Contains(filter.MyValue));
                                break;
                            case "CommentText":
                                query = query.Where(c => c.CommentText.Contains(filter.MyValue));
                                break;
                            case "BlogPostId":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.BlogPostId).Contains(filter.MyValue));
                                break;
                            default:
                                BlogComments = (from c in _dbset
                                             select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                                break;
                        }
                    }
                }
                query = query
                    .OrderBy(c => c.Id)
                    .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize);
                BlogComments = query.ToList();
                TotalItems = query.Count();
            }
            else
            {
                BlogComments = (from c in _dbset
                             select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                TotalItems = _dbset.Count();
            }
            PagedDataResponse<BlogComment> PagedDataResponse = new PagedDataResponse<BlogComment>(TotalItems, BlogComments.Count(), BlogComments, 0);
            return PagedDataResponse;
        }

        #endregion
        
    }

}
