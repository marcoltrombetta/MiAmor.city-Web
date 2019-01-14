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

    public interface ICustomerBookmarkService : IEntityService<CustomerBookmark>
    {
        CustomerBookmark GetById(int Id);
        PagedDataResponse<CustomerBookmark> GetByCustId(PagedDataRequest PagedDataRequest, int CustId);
        bool GetIsBookmarkedByCustIdVendorId(int CustId,int VendorId);

        CustomerBookmark GetByCustIdVendorId(int CustId, int VendorId);

    }

    public partial class CustomerBookmarkService : EntityService<CustomerBookmark>, ICustomerBookmarkService
    {
        #region Fields
        private IEncryption _Encryption;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public CustomerBookmarkService(IContext context, IEncryption Encryption)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<CustomerBookmark>();
            _Encryption = Encryption;
        }

        #endregion

        #region Methods

        public CustomerBookmark GetByCustIdVendorId(int CustId, int VendorId)
        {           
            return (from c in _dbset
                    where c.CustomerId == CustId && c.VendorId == VendorId
                    orderby c.VendorId
                    select c).FirstOrDefault();
        }
        
        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
       public bool GetIsBookmarkedByCustIdVendorId(int CustId,int VendorId)
        {
            return (from c in _dbset
                    where c.CustomerId == CustId && c.VendorId == VendorId
                    orderby c.VendorId
                    select c).ToList().Count > 0;

        }
        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public CustomerBookmark GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public PagedDataResponse<CustomerBookmark> GetByCustId(PagedDataRequest PagedDataRequest, int CustId)
        {
           
            List<CustomerBookmark> CustomerBookmark = new List<CustomerBookmark>();
            int QueryTotalCount;

            if (PagedDataRequest.SearchFor != "")
            {
                CustomerBookmark = (from c in _dbset
                                .Include(m => m.Vendor)
                                   where c.CustomerId == CustId
                                    orderby c.VendorId
                                   select c).Skip(PagedDataRequest.RowsPerPage * (PagedDataRequest.PageNumber - 1)).Take(PagedDataRequest.RowsPerPage).AsNoTracking().ToList();
            }
            else
            {
                CustomerBookmark = (from c in _dbset
                                    .Include(m => m.Vendor.Addresses)
                                    .Include(m => m.Vendor.VendorCategoryVendors.Select(vc => vc.VendorCategory))
                                   where c.CustomerId == CustId
                                   orderby c.VendorId
                                    select c).Skip(PagedDataRequest.RowsPerPage * (PagedDataRequest.PageNumber - 1)).Take(PagedDataRequest.RowsPerPage).AsNoTracking().ToList();
                //check later
            }
            int TotalItems = 0;
            PagedDataResponse<CustomerBookmark> PagedDataResponse = new PagedDataResponse<CustomerBookmark>(TotalItems, CustomerBookmark.Count(), CustomerBookmark);
            return PagedDataResponse;

        }
        #endregion

    }
}
