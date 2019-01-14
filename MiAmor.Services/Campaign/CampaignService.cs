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

    public interface ICampaignService : IEntityService<Campaign>
    {
        Campaign GetById(int Id);
        PagedDataResponse<Campaign> GetCampaignsByVendorId(PagedDataRequest PagedDataRequest, int VendorId);
        List<Campaign> GetQuickiesCampaigns();
        List<Campaign> GetCampaigns();
        PagedDataResponse<Campaign> GetAllCampaigns(PagedDataRequest PagedDataRequest);

        PagedDataResponse<Campaign> GetFilteredCampaigns(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions);
        List<Campaign> GetCampaignByPartialName(string PartialName, int NumOfRows);
    }

    public partial class CampaignService : EntityService<Campaign>, ICampaignService
    {
        #region Fields


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public CampaignService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<Campaign>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public Campaign GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

       public List<Campaign> GetQuickiesCampaigns()
        {
            return (from c in _dbset
                .Include(cp => cp.CampaignPictures)
                     orderby c.ExpirationDateTime 
                    select c).Take(3).ToList();
        }

       public List<Campaign> GetCampaigns()
       {
           return (from c in _dbset
               .Include(cp => cp.CampaignPictures)
                   orderby c.ExpirationDateTime
                   select c).ToList();
       }

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>List of Campaign</returns>
        public PagedDataResponse<Campaign> GetCampaignsByVendorId(PagedDataRequest PagedDataRequest, int VendorId)
        {
            List<Campaign> Campaigns= (from c in _dbset 
                                .Include(cp=> cp.CampaignPictures.Select(p=>p.Picture))
                                where c.VendorId==VendorId
                                orderby c.DisplayOrder
                                select c).Skip(PagedDataRequest.RowsPerPage * (PagedDataRequest.PageNumber - 1)).Take(PagedDataRequest.RowsPerPage).AsNoTracking().ToList();
            int TotalItems = 0;
            PagedDataResponse<Campaign> PagedDataResponse = new PagedDataResponse<Campaign>(TotalItems, Campaigns.Count(), Campaigns);
            return PagedDataResponse;
        }

        public PagedDataResponse<Campaign> GetAllCampaigns(PagedDataRequest PagedDataRequest)
        {
            List<Campaign> Campaigns = (from c in _dbset
                                 .Include(cp => cp.CampaignPictures.Select(p => p.Picture))
                                        orderby c.DisplayOrder
                                        select c).Skip(PagedDataRequest.RowsPerPage * (PagedDataRequest.PageNumber - 1)).Take(PagedDataRequest.RowsPerPage).AsNoTracking().ToList();
            int TotalItems = 0;
            PagedDataResponse<Campaign> PagedDataResponse = new PagedDataResponse<Campaign>(TotalItems, Campaigns.Count(), Campaigns);
            return PagedDataResponse;
        }

        public PagedDataResponse<Campaign> GetFilteredCampaigns(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            List<Campaign> Campaigns;
            int TotalItems = 0;
            if (FilterElements != null)
            {
                IQueryable<Campaign> query = _dbset;
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
                            case "VendorId":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.VendorId).Contains(filter.MyValue));
                                break;
                            case "CouponPrice":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.CouponPrice).Contains(filter.MyValue));
                                break;
                            case "EngagedInCampaign":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.EngagedInCampaign).Contains(filter.MyValue));
                                break;
                            case "SmallImgPath":
                                query = query.Where(c => c.SmallImgPath.Contains(filter.MyValue));
                                break;
                            default:
                                Campaigns = (from c in _dbset
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
                Campaigns = query.ToList();
            }
            else
            {
                Campaigns = (from c in _dbset
                                 select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                TotalItems = _dbset.Count();
            }
            PagedDataResponse<Campaign> PagedDataResponse = new PagedDataResponse<Campaign>(TotalItems, Campaigns.Count(), Campaigns, 0);
            return PagedDataResponse;
        }

        public List<Campaign> GetCampaignByPartialName(string PartialName, int NumOfRows)
        {
            List<Campaign> Campaigns = (from v in _dbset
                                        where v.Name.Contains(PartialName)
                                        select v).OrderBy(c => c.Name).Take(NumOfRows).ToList();
            return Campaigns;
        }
        #endregion

    }
}
