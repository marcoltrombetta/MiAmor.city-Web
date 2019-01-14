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
    public interface ICityService : IEntityService<City>
    {
        City GetById(int Id);
        City GetNeighbourhoodByCityId(int CityId);
        List<City> GetCityByPartialName(string PartialName, int NumOfRows);
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class CityService : EntityService<City>, ICityService
    {
        #region Fields
 

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public CityService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<City>();
           
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public City GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }
        public City GetNeighbourhoodByCityId(int CityId)
        {
            City CityNeighbourhood = (from c in _dbset
                                     .Include(cn => cn.Neighbourhood)
                                     where c.Id == CityId
                                     select c).FirstOrDefault();
            return CityNeighbourhood;
        }

        public List<City> GetCityByPartialName(string PartialName, int NumOfRows)
        {
            List<City> Cities = (from v in _dbset
                                    where v.Name.Contains(PartialName)
                                    select v).OrderBy(c => c.Name).Take(NumOfRows).ToList();
            return Cities;
        }
        #endregion

    }

}
