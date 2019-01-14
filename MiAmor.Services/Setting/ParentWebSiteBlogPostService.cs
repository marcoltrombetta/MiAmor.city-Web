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
    public interface IParentWebSiteBlogPostService : IEntityService<ParentWebSiteBlogPost>
    {
        List<BlogPost> GetLastestBlogPostByCategoryId(int CategoryId,int Count);
        List<BlogPost> GetLastestBlogPost(int count);
        ParentWebSiteBlogPost GetById(int Id);
        PagedDataResponse<BlogPost> GetBlogTimeLine(PagedDataRequest PagedDataRequest, Int16 BlogPostType);
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class ParentWebSiteBlogPostService : EntityService<ParentWebSiteBlogPost>, IParentWebSiteBlogPostService
    {
        #region Fields
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public ParentWebSiteBlogPostService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<ParentWebSiteBlogPost>();

        }

        #endregion

        #region Methods

        public List<BlogPost> GetLastestBlogPostByCategoryId(int CategoryId,int Count=2)
        {
            List<ParentWebSiteBlogPost> ListEvent = (from pb in _dbset
                                        .Include(p => p.BlogPost)
                                        .Include(pp => pp.BlogPost.BlogPostPictures.Select(p => p.Picture))
                                        .Include(pp=>pp.BlogPost.BlogPostCategoryBlogPost )
                                                     where pb.BlogPost.BlogPostType == 1 && 
                                                     pb.BlogPost.BlogPostCategoryBlogPost.Any(bcp=>bcp.CategoryId ==CategoryId)
                                                     orderby pb.BlogPost.StartDateUtc descending 
                                                     select pb).Take(Count).AsNoTracking().ToList();
            List<BlogPost> ListBlogPost = ListEvent.Select(l => l.BlogPost).ToList();


            return ListBlogPost;
        }

        public List<BlogPost> GetLastestBlogPost(int Count)
        {
           

            List<ParentWebSiteBlogPost> ListEvent = (from pb in _dbset
                                         .Include(p => p.BlogPost)
                                         .Include(pp => pp.BlogPost.BlogPostPictures.Select(p => p.Picture))
                                                     where pb.BlogPost.BlogPostType == 1
                                                     orderby pb.BlogPost.StartDateUtc descending
                                                     select pb).Take(Count).AsNoTracking().ToList();
            List<BlogPost> ListBlogPost = ListEvent.Select(l => l.BlogPost).ToList();


            return ListBlogPost;
        }
     
        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public ParentWebSiteBlogPost GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public PagedDataResponse<BlogPost> GetBlogTimeLine(PagedDataRequest PagedDataRequest,Int16 BlogPostType)
        {
            List<ParentWebSiteBlogPost> ListEvent = (from pb in _dbset
                                        .Include(p=>p.BlogPost)
                                        .Include(pp => pp.BlogPost.BlogPostPictures.Select(p => p.Picture))
                                                     where pb.BlogPost.BlogPostType == BlogPostType 
                                                     orderby pb.BlogPost.DisplayOrder
                                         select pb).Skip(PagedDataRequest.RowsPerPage * (PagedDataRequest.PageNumber - 1)).Take(PagedDataRequest.RowsPerPage).AsNoTracking().ToList();
            List < BlogPost> ListBlogPost= ListEvent.Select(l => l.BlogPost).ToList();

            int TotalItems = 0;
            PagedDataResponse<BlogPost> PagedDataResponse = new PagedDataResponse<BlogPost>(TotalItems, ListEvent.Count(), ListBlogPost);
            return PagedDataResponse;
        }


        #endregion
        
    }

}
