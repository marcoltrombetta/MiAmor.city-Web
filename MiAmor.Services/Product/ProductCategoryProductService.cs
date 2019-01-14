using System;
using System.Linq;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Data;

namespace MiAmor.Services
{
    public interface IProductCategoryProductService : IEntityService<ProductCategoryProduct>
    {
        ProductCategoryProduct GetById(int Id);
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class ProductCategoryProductService : EntityService<ProductCategoryProduct>, IProductCategoryProductService
    {
        #region Fields


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public ProductCategoryProductService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<ProductCategoryProduct>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public ProductCategoryProduct GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        #endregion

    }

}
