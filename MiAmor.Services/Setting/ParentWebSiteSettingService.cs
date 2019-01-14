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
    public interface IParentWebSiteSettingService : IEntityService<ParentWebSiteSetting>
    {
        ParentWebSiteSetting GetById(int Id);
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class ParentWebSiteSettingService : EntityService<ParentWebSiteSetting>, IParentWebSiteSettingService
    {
        #region Fields
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public ParentWebSiteSettingService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<ParentWebSiteSetting>();

        }

        #endregion

        #region Methods

     
        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public ParentWebSiteSetting GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

      

        #endregion
        
    }

}
