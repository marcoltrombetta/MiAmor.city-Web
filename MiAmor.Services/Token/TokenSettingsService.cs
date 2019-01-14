using System;
using System.Linq;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Data;

namespace MiAmor.Services
{

    public interface ITokenSettingsService : IEntityService<TokenSettings>
    {
        TokenSettings GetById(int Id);
    }

    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class TokenSettingsService : EntityService<TokenSettings>, ITokenSettingsService
    {
        #region Fields


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public TokenSettingsService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<TokenSettings>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public TokenSettings GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        #endregion

    }

}
