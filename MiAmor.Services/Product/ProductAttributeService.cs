using System;
using System.Linq;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Data;
using System.Collections.Generic;

namespace MiAmor.Services
{
    public interface IProductAttributeService : IEntityService<ProductAttribute>
    {
        ProductAttribute GetById(int Id);
        PagedDataResponse<ProductAttribute> GetFilteredProductAttributes(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions);
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class ProductAttributeService : EntityService<ProductAttribute>, IProductAttributeService
    {
        #region Fields


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public ProductAttributeService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<ProductAttribute>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public ProductAttribute GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        #endregion

        public PagedDataResponse<ProductAttribute> GetFilteredProductAttributes(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            List<ProductAttribute> ProductAttributes;
            int TotalItems = 0;
            if (FilterElements != null)
            {

                IQueryable<ProductAttribute> query = _dbset;
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
                            case "Description":
                                query = query.Where(c => c.Description.Contains(filter.MyValue));
                                break;
                            default:
                                ProductAttributes = (from c in _dbset
                                                 select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                                break;
                        }
                    }
                }
                query = query
                    .OrderBy(c => c.Id)
                    .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize);
                ProductAttributes = query.ToList();
                TotalItems = _dbset.Count();
            }
            else
            {
                ProductAttributes = (from c in _dbset
                                 select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                TotalItems = _dbset.Count();
            }
            PagedDataResponse<ProductAttribute> PagedDataResponse = new PagedDataResponse<ProductAttribute>(TotalItems, ProductAttributes.Count(), ProductAttributes, 0);
            return PagedDataResponse;
        }

    }

}
