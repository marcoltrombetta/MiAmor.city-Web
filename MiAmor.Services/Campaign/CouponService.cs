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

    public interface ICouponService : IEntityService<Coupon>
    {
        Coupon GetById(int Id);
        List<Coupon> GetByCustId(int CustId);
        PagedDataResponse<Coupon> GetCouponsByVendorId(PagedDataRequest PagedDataRequest, int VendorId);
       
    }

    public partial class CouponService : EntityService<Coupon>, ICouponService
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
        public CouponService(IContext context, IEncryption encription)
            : base(context)
        {
            _context = context;
            _Encryption = encription;
            _dbset = _context.Set<Coupon>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public Coupon GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public List<Coupon> GetByCustId(int CustId)
        {
                    
            List<Coupon> Coupon = (from c in _dbset
                                .Include(c => c.Campaign)
                                where c.CustomerId == CustId
                                select c).ToList();

            return Coupon;
        }
        public PagedDataResponse<Coupon> GetCouponsByVendorId(PagedDataRequest PagedDataRequest, int VendorId)
        {
            List<Coupon> Coupons = (from c in _dbset
                                    where c.Campaign.VendorId == VendorId
                                    select c).Skip(PagedDataRequest.RowsPerPage * (PagedDataRequest.PageNumber - 1)).Take(PagedDataRequest.RowsPerPage).AsNoTracking().ToList();
            int TotalItems = 0;
            PagedDataResponse<Coupon> PagedDataResponse = new PagedDataResponse<Coupon>(TotalItems, Coupons.Count(), Coupons);
            return PagedDataResponse;
        }
        #endregion

    }
}
