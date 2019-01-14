using System;
using System.Linq;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Data;


namespace MiAmor.Services
{

    public interface ICustomerMessageTypeService : IEntityService<CustomerMessageType>
    {
        CustomerMessageType GetById(int Id);
    }

    public partial class CustomerMessageTypeService : EntityService<CustomerMessageType>, ICustomerMessageTypeService
    {
        #region Fields


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public CustomerMessageTypeService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<CustomerMessageType>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public CustomerMessageType GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        #endregion

    }
}
