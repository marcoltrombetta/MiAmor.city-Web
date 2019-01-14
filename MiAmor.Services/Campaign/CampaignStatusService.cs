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

    public interface ICampaignStatusService : IEntityService<CampaignStatus>
    {
        CampaignStatus GetById(int Id);
        PagedDataResponse<CampaignStatus> GetFilteredCampaignStatus(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions);       
    }

    public partial class CampaignStatusService : EntityService<CampaignStatus>, ICampaignStatusService
    {
        #region Fields


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public CampaignStatusService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<CampaignStatus>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public CampaignStatus GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public PagedDataResponse<CampaignStatus> GetFilteredCampaignStatus(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            List<CampaignStatus> CampaignStatus;
            int TotalItems = 0;
            if (FilterElements != null)
            {
                IQueryable<CampaignStatus> query = _dbset;
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
                            case "Active":
                                bool boolActive;
                                if (filter.MyValue == "1")
                                {
                                    boolActive = true;
                                }
                                else
                                {
                                    boolActive = false;
                                }
                                query = query.Where(c => c.Active == boolActive);
                                break;
                            default:
                                CampaignStatus = (from c in _dbset
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
                CampaignStatus = query.ToList();
            }
            else
            {
                CampaignStatus = (from c in _dbset
                                      select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                TotalItems = _dbset.Count();
            }
            PagedDataResponse<CampaignStatus> PagedDataResponse = new PagedDataResponse<CampaignStatus>(TotalItems, CampaignStatus.Count(), CampaignStatus, 0);
            return PagedDataResponse;
        }

        #endregion

    }
}
