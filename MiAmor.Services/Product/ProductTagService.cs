using System;
using System.Linq;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Data;
using System.Collections.Generic;

namespace MiAmor.Services
{
    public interface IProductTagService : IEntityService<ProductTag>
    {
        ProductTag GetById(int Id);
        PagedDataResponse<ProductTag> GetFilteredProductTags(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions);
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class ProductTagService : EntityService<ProductTag>, IProductTagService
    {
        #region Fields


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public ProductTagService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<ProductTag>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public ProductTag GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public PagedDataResponse<ProductTag> GetFilteredProductTags(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            List<ProductTag> ProductTags;
            int TotalItems = 0;
            if (FilterElements != null)
            {
                IQueryable<ProductTag> query = _dbset;
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
                            default:
                                ProductTags = (from c in _dbset
                                                     select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                                break;
                        }
                    }
                }
                query = query
                    .OrderBy(c => c.Id)
                    .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize);
                ProductTags = query.ToList();
                TotalItems = query.Count();
            }
            else
            {
                ProductTags = (from c in _dbset
                                     select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                TotalItems = _dbset.Count();
            }
            PagedDataResponse<ProductTag> PagedDataResponse = new PagedDataResponse<ProductTag>(TotalItems, ProductTags.Count(), ProductTags, 0);
            return PagedDataResponse;
        }

        #endregion

    }

}
