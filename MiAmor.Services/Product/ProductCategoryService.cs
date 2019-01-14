using System;
using System.Linq;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Data;
using System.Collections.Generic;

namespace MiAmor.Services
{
    public interface IProductCategoryService : IEntityService<ProductCategory>
    {
        ProductCategory GetById(int Id);
        PagedDataResponse<ProductCategory> GetFilteredProductCategories(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions);
        List<ProductCategory> GetParentCategoryByPartialName(string PartialName, int NumOfRows);
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class ProductCategoryService : EntityService<ProductCategory>, IProductCategoryService
    {
        #region Fields


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public ProductCategoryService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<ProductCategory>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public ProductCategory GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public PagedDataResponse<ProductCategory> GetFilteredProductCategories(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            List<ProductCategory> ProductCategories;
            int TotalItems = 0;
            if (FilterElements != null)
            {

                IQueryable<ProductCategory> query = _dbset;
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
                            case "ParentCategoryId":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.ParentCategoryId).Contains(filter.MyValue));
                                break;
                            case "ShowOnHomePage":
                                bool boolShowOnHomePage;
                                if (filter.MyValue == "1")
                                {
                                    boolShowOnHomePage = true;
                                }
                                else
                                {
                                    boolShowOnHomePage = false;
                                }
                                query = query.Where(c => c.ShowOnHomePage == boolShowOnHomePage);
                                break;
                            case "MetaKeywords":
                                query = query.Where(c => c.MetaKeywords.Contains(filter.MyValue));
                                break;
                            case "MetaDescription":
                                query = query.Where(c => c.MetaDescription.Contains(filter.MyValue));
                                break;
                            case "MetaTitle":
                                query = query.Where(c => c.MetaTitle.Contains(filter.MyValue));
                                break;
                            case "PageSize":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.PageSize).Contains(filter.MyValue));
                                break;
                            case "Published":
                                bool boolPublished;
                                if (filter.MyValue == "1")
                                {
                                    boolPublished = true;
                                }
                                else
                                {
                                    boolPublished = false;
                                }
                                query = query.Where(c => c.Published == boolPublished);
                                break;
                            case "DisplayOrder":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.DisplayOrder).Contains(filter.MyValue));
                                break;
                            default:
                                ProductCategories = (from c in _dbset
                                                     select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                                break;
                        }
                    }
                }
                query = query
                    .OrderBy(c => c.Id)
                    .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize);
                ProductCategories = query.ToList();
                TotalItems = query.Count();
            }
            else
            {
                ProductCategories = (from c in _dbset
                                     select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                TotalItems = _dbset.Count();
            }
            PagedDataResponse<ProductCategory> PagedDataResponse = new PagedDataResponse<ProductCategory>(TotalItems, ProductCategories.Count(), ProductCategories, 0);
            return PagedDataResponse;
        }

        public List<ProductCategory> GetParentCategoryByPartialName(string PartialName, int NumOfRows)
        {
            List<ProductCategory> ProductCategories = (from v in _dbset
                                 where v.Name.Contains(PartialName)
                                 select v).OrderBy(c => c.Name).Take(NumOfRows).ToList();
            return ProductCategories;
        }

        #endregion

    }

}
