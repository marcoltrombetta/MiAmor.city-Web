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
    public interface IVendorCategoryCountService : IEntityService<VendorCategoryCount>
    {
        VendorCategoryCount GetById(int Id);
        VendorCategoryCount GetByCategoryId(int Id);
        List<VendorCategoryCount> GeAllParentCategoryCount();
        PagedDataResponse<VendorCategoryCount> GetFilteredVendorCategoryCounts(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions);

    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class VendorCategoryCountService : EntityService<VendorCategoryCount>, IVendorCategoryCountService
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
        public VendorCategoryCountService(ICacheManager CacheManager, IContext context)
            : base(context)
        {
            _CacheManager = CacheManager;

        }

        #endregion

        #region Constants
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : vendor ID
        /// </remarks>
        private const string VENDORSCATEGORYCOUNT_BY_ID_KEY = "MiAmor.vendorcategorycount-{0}";
        #endregion

        #region Methods

        /// <summary>
        /// Gets an Category by Category identifier
        /// </summary>
        /// <param name="CategoryId">Category identifier</param>
        /// <returns>Category</returns>
        public VendorCategoryCount GetById(int Id)
        {            
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }
        public VendorCategoryCount GetByCategoryId(int Id)
        {
            string key = string.Format(VENDORSCATEGORYCOUNT_BY_ID_KEY, Id);
            return _CacheManager.Get(key, () =>
            {
               return  _dbset.FirstOrDefault(x => x.VendorCategoryId == Id);
            });
        }
        public List<VendorCategoryCount> GeAllParentCategoryCount()
        {
            List<VendorCategoryCount> ListVendorCategoryCount=(from vc in _dbset
                    where vc.ParentCategoryId == null || vc.ParentCategoryId == 0
                    select vc).ToList();
            
            return ListVendorCategoryCount;
        }

        public PagedDataResponse<VendorCategoryCount> GetFilteredVendorCategoryCounts(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            List<VendorCategoryCount> VendorCategoryCounts;
            int TotalItems = 0;
            if (FilterElements != null)
            {
                IQueryable<VendorCategoryCount> query = _dbset;
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
                            case "vendorCategoryId":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.VendorCategoryId).Contains(filter.MyValue));
                                break;
                            case "countVendors":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.CountVendors).Contains(filter.MyValue));
                                break;
                            case "cssClass":
                                query = query.Where(c => c.CssClass.Contains(filter.MyValue));
                                break;
                            default:
                                VendorCategoryCounts = (from c in _dbset
                                                    select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                                break;
                        }
                    }
                }
                query = query
                    .OrderBy(c => c.Id)
                    .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize);
                VendorCategoryCounts = query.ToList();
                TotalItems = query.Count();
            }
            else
            {
                VendorCategoryCounts = (from c in _dbset
                                    select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                TotalItems = _dbset.Count();
            }
            PagedDataResponse<VendorCategoryCount> PagedDataResponse = new PagedDataResponse<VendorCategoryCount>(TotalItems, VendorCategoryCounts.Count(), VendorCategoryCounts, 0);
            return PagedDataResponse;
        }
        #endregion

    }

}
 