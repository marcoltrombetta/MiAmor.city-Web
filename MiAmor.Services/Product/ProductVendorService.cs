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
    public interface IProductVendorService : IEntityService<ProductVendor>
    {
        ProductVendor GetById(int Id);
        PagedDataResponse<ProductVendor> GetProductsByCategory(PagedDataRequest PagedDataRequest, int CategoryId,int VendorId);
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class ProductVendorService : EntityService<ProductVendor>, IProductVendorService
    {
        #region Fields
        private readonly ICacheManager _CacheManager;

        #endregion
        #region Constants
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : vendor ID
        /// </remarks>
        private const string PRODUCT_INCLUDE_KEY = "MiAmor.product.id-{0}";


        #endregion
        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public ProductVendorService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<ProductVendor>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public ProductVendor GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public PagedDataResponse<ProductVendor> GetProductsByCategory(PagedDataRequest PagedDataRequest, int CategoryId, int VendorId)
        {

            List<ProductVendor> Products = new List<ProductVendor>();
            int QueryTotalCount;


            if (PagedDataRequest.SearchFor != "")
            {
                Products = (from p in _dbset
                         .Include(va => va.ProductCategories)                        
                           where (p.ProductCategories.Any(c => c.Id == CategoryId) || CategoryId == 0) &&
                           p.VendorId == VendorId &&
                           (p.Name.Contains(PagedDataRequest.SearchFor) || String.IsNullOrEmpty(PagedDataRequest.SearchFor))
                           orderby (PagedDataRequest.SortIndexName)
                           select p).Skip(PagedDataRequest.RowsPerPage * (PagedDataRequest.PageNumber - 1)).Take(PagedDataRequest.RowsPerPage).AsNoTracking().ToList();
            }
            else
            {
                var t = (from p in _dbset
                          where (p.ProductCategories.Any(c => c.CategoryId == CategoryId))  &&
                           p.VendorId == VendorId                     
                         orderby (PagedDataRequest.SortIndexName)
                         select p);
                     Products =t.Skip(PagedDataRequest.RowsPerPage * (PagedDataRequest.PageNumber - 1)).Take(PagedDataRequest.RowsPerPage).AsNoTracking().ToList();
                //check later
            }           
            int TotalItems = 0;
            PagedDataResponse<ProductVendor> PagedDataResponse = new PagedDataResponse<ProductVendor>(TotalItems, Products.Count(), Products);
            return PagedDataResponse;
        }

        #endregion

    }

}
