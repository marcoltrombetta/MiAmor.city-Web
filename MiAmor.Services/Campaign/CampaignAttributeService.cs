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

    public interface ICampaignAttributeService : IEntityService<CampaignAttribute>
    {
        CampaignAttribute GetById(int Id);
        PagedDataResponse<CampaignAttribute> GetFilteredCampaignAttributes(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions);
    }

    public partial class CampaignAttributeService : EntityService<CampaignAttribute>, ICampaignAttributeService
    {
        #region Fields


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public CampaignAttributeService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<CampaignAttribute>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public CampaignAttribute GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public PagedDataResponse<CampaignAttribute> GetFilteredCampaignAttributes(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            List<CampaignAttribute> CampaignAttributes;
            int TotalItems = 0;
            if (FilterElements != null)
            {
                IQueryable<CampaignAttribute> query = _dbset;
                foreach (FilterKeyValue filter in FilterElements)
                {
                    if (filter.MyKey != null && filter.MyValue != null)
                    {
                        switch (filter.MyKey)
                        {
                            case "Id":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.Id).Contains(filter.MyValue));
                                break;
                            case "Name":
                                query = query.Where(c => c.Name.Contains(filter.MyValue));
                                break;
                            case "Description":
                                query = query.Where(c => c.Description.Contains(filter.MyValue));
                                break;                          
                            default:
                                CampaignAttributes = (from c in _dbset
                                                      select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                                break;
                        }
                    }
                }
                query = query
                    .OrderBy(c => c.Id)
                    .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize);
                TotalItems = _dbset.Count();
                CampaignAttributes = query.ToList();
            }
            else
            {
                CampaignAttributes = (from c in _dbset
                                      select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                TotalItems = _dbset.Count();
            }
            PagedDataResponse<CampaignAttribute> PagedDataResponse = new PagedDataResponse<CampaignAttribute>(TotalItems, CampaignAttributes.Count(), CampaignAttributes, 0);
            return PagedDataResponse;
        }

        #endregion

    }
}
