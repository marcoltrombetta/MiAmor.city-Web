using System;
using System.Linq;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Data;

namespace MiAmor.Services
{
    public interface IVendorAddressService : IEntityService<VendorAddress>
    {
        VendorAddress GetById(int Id);
        VendorAddress GetByVendorId(int VendorId);
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class VendorAddressService : EntityService<VendorAddress>, IVendorAddressService
    {
        #region Fields
 

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public VendorAddressService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<VendorAddress>();
           
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public VendorAddress GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public VendorAddress GetByVendorId(int VendorId)
        {
            return _dbset.FirstOrDefault(x => x.VendorId == VendorId);
        }

        #endregion

    }

}
