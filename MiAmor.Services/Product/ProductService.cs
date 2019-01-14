using System;
using System.Linq;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Data;
using System.Collections.Generic;

namespace MiAmor.Services
{
    public interface IProductService : IEntityService<Product>
    {
        Product GetById(int Id);
        PagedDataResponse<Product> GetProductsByCategory(PagedDataRequest PagedDataRequest, int CategoryId);
        PagedDataResponse<Product> GetFilteredProducts(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions);
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class ProductService : EntityService<Product>, IProductService
    {
        #region Fields


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public ProductService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<Product>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public Product GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public PagedDataResponse<Product> GetProductsByCategory(PagedDataRequest PagedDataRequest, int CategoryId){
            return null;
        }

        public PagedDataResponse<Product> GetFilteredProducts(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            List<Product> Products;
            int TotalItems = 0;
            if (FilterElements != null)
            {

                IQueryable<Product> query = _dbset;
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
                            case "ProductTypeId":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.ProductTypeId).Contains(filter.MyValue));
                                break;
                            case "Price":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.Price).Contains(filter.MyValue));
                                break;
                            case "ProductCost":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.ProductCost).Contains(filter.MyValue));
                                break;
                            default:
                                Products = (from c in _dbset
                                           select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                                break;
                        }
                    }
                }
                query = query
                    .OrderBy(c => c.Id)
                    .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize);
                Products = query.ToList();
                TotalItems = _dbset.Count();
            }
            else
            {
                Products = (from c in _dbset
                           select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                TotalItems = _dbset.Count();
            }
            PagedDataResponse<Product> PagedDataResponse = new PagedDataResponse<Product>(TotalItems, Products.Count(), Products, 0);
            return PagedDataResponse;
        }

        #endregion

    }

}
