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

    public interface ICampaignAttributeMappingService : IEntityService<CampaignAttributeMapping>
    {
        CampaignAttributeMapping GetById(int Id);
       
    }

    public partial class CampaignAttributeMappingService : EntityService<CampaignAttributeMapping>, ICampaignAttributeMappingService
    {
        #region Fields


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public CampaignAttributeMappingService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<CampaignAttributeMapping>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public CampaignAttributeMapping GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        #endregion

    }
}
