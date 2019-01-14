using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiAmor.Core;
using MiAmor.Data;
using System.Data.Entity;
using System.Collections.Generic;

namespace MiAmor.Services
{
    public interface IVendorCategoryService : IEntityService<VendorCategory>
    {
        VendorCategory GetById(int Id);
        
        List<VendorCategory> GetParentNavigationVendors(int CategoryId, int PageId);
        List<VendorCategory> GetCategoryByParentId(int CategoryParentId);
        PagedDataResponse<VendorCategory> GetFilteredVendorCategories(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions);

    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class VendorCategoryService : EntityService<VendorCategory>, IVendorCategoryService
    {
        #region Fields
        private readonly ICacheManager _CacheManager;


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="CategoryRepository">Category repository</param>
        /// <param name="eventPublisher">Event published</param>
        public VendorCategoryService(ICacheManager CacheManager, IContext context)
            : base(context)
        {
            _CacheManager = CacheManager;
            //_context = context;
            //_dbset = _context.Set<VendorCategory>();

        }

        #endregion

        #region Constants
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : category ID
        /// </remarks>
        private const string CATEGORIES_BY_ID_KEY = "MiAmor.category.id-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : parent category ID
        /// {1} : show hidden records?
        /// {2} : current customer ID
        /// {3} : store ID
        /// </remarks>
        private const string CATEGORIES_BY_PARENT_CATEGORY_ID_KEY = "MiAmor.category.byparent-{0}-{1}-{2}-{3}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// {1} : category ID
        /// {2} : page index
        /// {3} : page size
        /// {4} : current customer ID
        /// {5} : store ID
        /// </remarks>
        private const string PRODUCTCATEGORIES_ALLBYCATEGORYID_KEY = "MiAmor.productcategory.allbycategoryid-{0}-{1}-{2}-{3}-{4}-{5}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// {1} : product ID
        /// {2} : current customer ID
        /// {3} : store ID
        /// </remarks>
        private const string PRODUCTCATEGORIES_ALLBYPRODUCTID_KEY = "MiAmor.productcategory.allbyproductid-{0}-{1}-{2}-{3}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string CATEGORIES_PATTERN_KEY = "MiAmor.category-{0}";
        /// <summary>
        /// Key pattern to clear cache for categoryParent without pageid
        /// </summary>
        private const string NAVIGATIONCATEGORIES_PATTERN_KEY = "MiAmor.navigationcategory-{0}";

        /// <summary>
        /// Key pattern to clear cache for categoryParent with pageid
        /// </summary>
        private const string NAVIGATIONCATEGORIES_PAGEID_PATTERN_KEY = "MiAmor.navigationcategorypageid-{0}";

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Category by Category identifier
        /// </summary>
        /// <param name="CategoryId">Category identifier</param>
        /// <returns>Category</returns>
        public VendorCategory GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

      
        public List<VendorCategory> GetParentNavigationVendors(int CategoryId, int PageId)
        {
             List<VendorCategory> ParentVendorCategory ;
             if (PageId == 0)
             {
                 string key = string.Format(NAVIGATIONCATEGORIES_PATTERN_KEY, CategoryId);
                 return _CacheManager.Get(key, () =>
               {
                   ParentVendorCategory = (from vc in _dbset
                                           where vc.ParentCategoryId == CategoryId
                                           orderby vc.DisplayOrder, vc.Name
                                           select vc).AsNoTracking().ToList();
                   return ParentVendorCategory;
               });
             }
             else
             {

                 string key = string.Format(NAVIGATIONCATEGORIES_PAGEID_PATTERN_KEY, CategoryId+"_"+PageId);
                 return _CacheManager.Get(key, () =>
                 {

                     ParentVendorCategory = (from vc in _dbset
                                             where vc.ParentCategoryId == CategoryId
                                             orderby vc.DisplayOrder
                                             select vc).Skip((PageId - 1) * 10).Take(10).AsNoTracking().ToList();
                     return ParentVendorCategory;
                 });
             }
        }

       public List<VendorCategory> GetCategoryByParentId(int CategoryParentId) {
           
           var aux=(from vc in _dbset
                  where vc.ParentCategoryId == CategoryParentId
                select vc).AsNoTracking().ToList();

           return aux.ToList();

        }

       public PagedDataResponse<VendorCategory> GetFilteredVendorCategories(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
       {
           List<VendorCategory> VendorCategories;
           int TotalItems = 0;
           if (FilterElements != null)
           {

               IQueryable<VendorCategory> query = _dbset;
               foreach (FilterKeyValue filter in FilterElements)
               {
                   if (filter.MyKey != null && filter.MyValue != null)
                   {
                       switch (filter.MyKey)
                       {
                           case "Id":
                               query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.Id).Contains(filter.MyValue));
                               break;
                           case "name":
                               query = query.Where(c => c.Name.Contains(filter.MyValue));
                               break;
                           case "description":
                               query = query.Where(c => c.Description.Contains(filter.MyValue));
                               break;                          
                           case "showOnHomePage":
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
                           case "metaKeywords":
                               query = query.Where(c => c.MetaKeywords.Contains(filter.MyValue));
                               break;
                           case "metaDescription":
                               query = query.Where(c => c.MetaDescription.Contains(filter.MyValue));
                               break;
                           case "metaTitle":
                               query = query.Where(c => c.MetaTitle.Contains(filter.MyValue));
                               break;
                           case "pageSize":
                               query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.PageSize).Contains(filter.MyValue));
                               break;
                           case "published":
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
                           case "cssClass":
                               query = query.Where(c => c.CssClass.Contains(filter.MyValue));
                               break;
                           case "displayOrder":
                               query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.DisplayOrder).Contains(filter.MyValue));
                               break;
                           default:
                               VendorCategories = (from c in _dbset
                                                    select c).OrderBy(c => c.Id)
                               .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                               break;
                       }
                   }
               }
               query = query
                   .OrderBy(c => c.Id)
                   .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize);
               VendorCategories = query.ToList();
               TotalItems = query.Count();
           }
           else
           {
               VendorCategories = (from c in _dbset
                                    select c).OrderBy(c => c.Id)
                               .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
               TotalItems = _dbset.Count();
           }
           PagedDataResponse<VendorCategory> PagedDataResponse = new PagedDataResponse<VendorCategory>(TotalItems, VendorCategories.Count(), VendorCategories, 0);
           return PagedDataResponse;
       }
        #endregion

    }

}
 