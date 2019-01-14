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

    public interface ICampaignPaymentService : IEntityService<CampaignPayment>
    {
        CampaignPayment GetById(int Id);
        PagedDataResponse<CampaignPayment> GetFilteredCampaignPayments(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions);
    }

    public partial class CampaignPaymentService : EntityService<CampaignPayment>, ICampaignPaymentService
    {
        #region Fields


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public CampaignPaymentService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<CampaignPayment>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public CampaignPayment GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public PagedDataResponse<CampaignPayment> GetFilteredCampaignPayments(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            List<CampaignPayment> CampaignPayments;
            int TotalItems = 0;
            if (FilterElements != null)
            {
                IQueryable<CampaignPayment> query = _dbset;
                foreach (FilterKeyValue filter in FilterElements)
                {
                    if (filter.MyKey != null && filter.MyValue != null)
                    {
                        switch (filter.MyKey)
                        {
                            case "Id":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.Id).Contains(filter.MyValue));
                                break;
                            case "CustomerId":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.CustomerId).Contains(filter.MyValue));
                                break;
                            case "CampaignId":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.CampaignId).Contains(filter.MyValue));
                                break;
                            case "QtyCupon":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.QtyCupon).Contains(filter.MyValue));
                                break;
                            case "TotalSum":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.TotalSum).Contains(filter.MyValue));
                                break;
                                //date
                            default:
                                CampaignPayments = (from c in _dbset
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
                CampaignPayments = query.ToList();
            }
            else
            {
                CampaignPayments = (from c in _dbset
                                      select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                TotalItems = _dbset.Count();
            }
            PagedDataResponse<CampaignPayment> PagedDataResponse = new PagedDataResponse<CampaignPayment>(TotalItems, CampaignPayments.Count(), CampaignPayments, 0);
            return PagedDataResponse;
        }

        #endregion

    }
}
