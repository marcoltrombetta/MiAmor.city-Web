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
    public interface IBlogPostService : IEntityService<BlogPost>
    {
        
        BlogPost GetById(int Id);
        PagedDataResponse<BlogPost> GetFilteredBlogPosts(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions);
        PagedDataResponse<BlogPost> GetBlogPostTimeLine(PagedDataRequest PagedDataRequest, int BlogPostType);
        BlogPost GetByAppPost(int Id);
        
        List<BlogPost> GetBlogPostByPartialName(string PartialName, int NumOfRows);
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class BlogPostService : EntityService<BlogPost>, IBlogPostService
    {
        #region Fields
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public BlogPostService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<BlogPost>();

        }

        #endregion

        #region Methods
     

        public BlogPost GetByAppPost(int Id)
        {
            BlogPost BlogPost = (from b in _dbset
                                 .Include(bp => bp.BlogComments)
                                 .Include(bp => bp.BlogPostPictures.Select(p=>p.Picture))
                                 select b).AsNoTracking().FirstOrDefault();
            return BlogPost;

        }
     
        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public BlogPost GetById(int Id)
        {
           return  (from pb in _dbset
                .Include(pb => pb.BlogPostPictures.Select(bp => bp.Picture))
                .Include(pb => pb.BlogPostTagBlogPost.Select(t => t.BlogPostTag))
                where pb.Id==Id
                orderby pb.CreatedOnUtc
                select pb).SingleOrDefault();

         //   return _dbset.FirstOrDefault(x => x.Id == Id);
        }

     


        public PagedDataResponse<BlogPost> GetFilteredBlogPosts(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            List<BlogPost> BlogPosts;
            int TotalItems = 0;
            if (FilterElements != null)
            {
                IQueryable<BlogPost> query = _dbset;
                foreach (FilterKeyValue filter in FilterElements)
                {
                    if (filter.MyKey != null && filter.MyValue != null)
                    {
                        switch (filter.MyKey)
                        {
                            case "Id":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.Id).Contains(filter.MyValue));
                                break;
                            case "Title":
                                query = query.Where(c => c.Title.Contains(filter.MyValue));
                                break;
                           //add date filters
                            case "AllowComments":
                                bool boolAllowComments;
                                if (filter.MyValue == "1")
                                {
                                    boolAllowComments = true;
                                }
                                else
                                {
                                    boolAllowComments = false;
                                }
                                query = query.Where(c => c.AllowComments == boolAllowComments);
                                break;
                            default:
                                BlogPosts = (from c in _dbset
                                                        select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                                break;
                        }
                    }
                }
                query = query
                    .OrderBy(c => c.Id)
                    .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize);
                BlogPosts = query.ToList();
                TotalItems = query.Count();
            }
            else
            {
                BlogPosts = (from c in _dbset
                                        select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                TotalItems = _dbset.Count();
            }
            PagedDataResponse<BlogPost> PagedDataResponse = new PagedDataResponse<BlogPost>(TotalItems, BlogPosts.Count(), BlogPosts, 0);
            return PagedDataResponse;
        }
        public PagedDataResponse<BlogPost> GetBlogPostTimeLine(PagedDataRequest PagedDataRequest, int BlogPostType)
        {
            List<BlogPost> ListBlog = (from pb in _dbset
                                       .Include(pb => pb.BlogPostPictures.Select(bp => bp.Picture))
                                       where pb.BlogPostType==BlogPostType
                                       orderby pb.CreatedOnUtc
                                       select pb).Skip(PagedDataRequest.RowsPerPage * (PagedDataRequest.PageNumber - 1)).Take(PagedDataRequest.RowsPerPage).AsNoTracking().ToList();
            
            int TotalItems = 0;
            PagedDataResponse<BlogPost> PagedDataResponse = new PagedDataResponse<BlogPost>(TotalItems, ListBlog.Count(), ListBlog);
            return PagedDataResponse;
        }


        public List<BlogPost> GetBlogPostByPartialName(string PartialName, int NumOfRows)
        {
            List<BlogPost> BlogPosts = (from v in _dbset
                                        where v.Title.Contains(PartialName)
                                        select v).OrderBy(c => c.Title).Take(NumOfRows).ToList();
            return BlogPosts;
        }

        #endregion
        
    }

}
