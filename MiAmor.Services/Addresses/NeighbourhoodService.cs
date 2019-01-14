using System;
using System.Linq;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Data;
using System.Collections.Generic;

namespace MiAmor.Services
{
    public interface INeighbourhoodService : IEntityService<Neighbourhood>
    {
        Neighbourhood GetById(int Id);
        List<Neighbourhood> GetByCityId(int CityId);
        List<Neighbourhood> GetNeighbourhoodByPartialName(string PartialName, int CityId, int NumOfRows);

        List<Neighbourhood> GetByCityName(String CityName);
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class NeighbourhoodService : EntityService<Neighbourhood>, INeighbourhoodService
    {
        #region Fields
 

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public NeighbourhoodService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<Neighbourhood>();
           
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public Neighbourhood GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public List<Neighbourhood> GetByCityId(int CityId)
        {
            List<Neighbourhood> Neighbourhoods = (from n in _dbset
                                                      where n.CityId== CityId
                                                      orderby n.DisplayOrder
                                                      select n).ToList();
            return Neighbourhoods;
        }

        public List<Neighbourhood> GetByCityName(String CityName)
        {
            List<Neighbourhood> Neighbourhoods = (from n in _dbset
                                                  where n.City.Name == CityName
                                                  orderby n.DisplayOrder
                                                  select n).ToList();
            return Neighbourhoods;
        }


        public List<Neighbourhood> GetNeighbourhoodByPartialName(string PartialName, int CityId, int NumOfRows)
        {
            List<Neighbourhood> Neighbourhoods = (from v in _dbset
                                 where v.Name.Contains(PartialName)
                                 where v.CityId == CityId
                                 select v).OrderBy(c => c.Name).Take(NumOfRows).ToList();
            return Neighbourhoods;
        }

        #endregion

    }

}
