using System;
using System.Linq;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Data;


/// this template is for creating regions and structure for all services
/// should be deleted at the end
namespace MiAmor.Services
{
    class ServiceTemplate : EntityService<VendorAddress>, IVendorAddressService
    {
          #region Fields
 

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public ServiceTemplate(IContext context)
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

        #endregion

    }
}
