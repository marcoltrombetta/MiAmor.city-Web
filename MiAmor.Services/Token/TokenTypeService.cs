using System;
using System.Linq;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Data;

namespace MiAmor.Services
{
    public interface ITokenTypeService : IEntityService<TokenType>
    {
        TokenType GetById(int Id);
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class TokenTypeService : EntityService<TokenType>, ITokenTypeService
    {
        #region Fields


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public TokenTypeService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<TokenType>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public TokenType GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        #endregion

    }

}
