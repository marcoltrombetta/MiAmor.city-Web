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
    public interface IVendorBlogPostService : IEntityService<VendorBlogPost>
    {
        VendorBlogPost GetById(int Id);
        PagedDataResponse<VendorBlogPost> GetVendorBlogPostByVendorId(PagedDataRequest PagedDataRequest, int VendorId, Int16 BlogPostType);

        PagedDataResponse<VendorBlogPost> GetAllVendorBlogPost(PagedDataRequest PagedDataRequest, Int16 BlogPostType);

    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class VendorBlogPostService : EntityService<VendorBlogPost>, IVendorBlogPostService
    {
        #region Fields
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public VendorBlogPostService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<VendorBlogPost>();

        }

        #endregion

        #region Methods

     
        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public VendorBlogPost GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public PagedDataResponse<VendorBlogPost> GetVendorBlogPostByVendorId(PagedDataRequest PagedDataRequest, int VendorId, Int16 BlogPostType=1)
        {
            List<VendorBlogPost> ListBlog = (from v in _dbset.Include(va => va.BlogPost)
                                                    .Include(v => v.BlogPost.BlogPostPictures.Select(av => av.Picture))
                                                     where v.VendorId == VendorId && v.BlogPost.BlogPostType == BlogPostType
                                                     orderby v.BlogPost.DisplayOrder
                                                     select v).Skip(PagedDataRequest.RowsPerPage * (PagedDataRequest.PageNumber - 1)).Take(PagedDataRequest.RowsPerPage).AsNoTracking().ToList();

            int TotalItems = 0;
            PagedDataResponse<VendorBlogPost> PagedDataResponse = new PagedDataResponse<VendorBlogPost>(TotalItems,ListBlog.Count, ListBlog);
            return PagedDataResponse;
        }

        public PagedDataResponse<VendorBlogPost> GetAllVendorBlogPost(PagedDataRequest PagedDataRequest, Int16 BlogPostType = 1)
        {
            List<VendorBlogPost> ListBlog = (from v in _dbset.Include(va => va.BlogPost)
                                                    .Include(v => v.BlogPost.BlogPostPictures.Select(av => av.Picture))
                                             where v.BlogPost.BlogPostType == BlogPostType
                                             orderby v.BlogPost.DisplayOrder
                                             select v).Skip(PagedDataRequest.RowsPerPage * (PagedDataRequest.PageNumber - 1)).Take(PagedDataRequest.RowsPerPage).AsNoTracking().ToList();

            int TotalItems = 0;
            PagedDataResponse<VendorBlogPost> PagedDataResponse = new PagedDataResponse<VendorBlogPost>(TotalItems, ListBlog.Count, ListBlog);
            return PagedDataResponse;
        }
        #endregion
        
    }

}
