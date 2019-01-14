using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiAmor.Core;
using MiAmor.Data;
using System.Data.Entity;

using MiAmor.Services;
using System.Linq.Expressions;
using System.Data.Entity.SqlServer;

namespace MiAmor.Services
{
    public interface IProductReviewService : IEntityService<ProductReview>
    {
        ProductReview GetById(int Id);
        PagedDataResponse<ProductReview> GetFilteredProductReviews(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions);
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class ProductReviewService : EntityService<ProductReview>, IProductReviewService
    {
        #region Fields


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public ProductReviewService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<ProductReview>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public ProductReview GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public PagedDataResponse<ProductReview> GetFilteredProductReviews(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            List<ProductReview> ProductReviews;
            int TotalItems = 0;
            if (FilterElements != null)
            {

                IQueryable<ProductReview> query = _dbset;
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
                            case "ProductId":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.ProductId).Contains(filter.MyValue));
                                break;
                            case "IsApproved":
                                bool boolIsApproved;
                                if (filter.MyValue == "1")
                                {
                                    boolIsApproved = true;
                                }
                                else
                                {
                                    boolIsApproved = false;
                                }
                                query = query.Where(c => c.IsApproved == boolIsApproved);
                                break;
                            case "Title":
                                query = query.Where(c => c.Title.Contains(filter.MyValue));
                                break;
                            case "ReviewText":
                                query = query.Where(c => c.ReviewText.Contains(filter.MyValue));
                                break;
                            case "Rating":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.Rating).Contains(filter.MyValue));
                                break;
                            case "HelpfulYesTotal":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.HelpfulYesTotal).Contains(filter.MyValue));
                                break;
                            case "HelpfulNoTotal":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.HelpfulNoTotal).Contains(filter.MyValue));
                                break;
                            default:
                                ProductReviews = (from c in _dbset
                                                 select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                                break;
                        }
                    }
                }
                query = query
                     .Include(cc => cc.Customer)
                     .Include(ccc => ccc.Product)
                    .OrderBy(c => c.Id)
                    .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize);
                ProductReviews = query.ToList();
                TotalItems = query.Count();
            }
            else
            {
                ProductReviews = (from c in _dbset
                                  .Include(cc=>cc.Customer)
                                  .Include(ccc => ccc.Product)
                                 select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                TotalItems = _dbset.Count();
            }            
            
            PagedDataResponse<ProductReview> PagedDataResponse = new PagedDataResponse<ProductReview>(TotalItems, ProductReviews.Count(), ProductReviews, 0);
            return PagedDataResponse;
        }

        #endregion

    }

}
