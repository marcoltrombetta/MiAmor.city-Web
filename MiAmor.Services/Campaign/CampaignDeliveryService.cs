
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiAmor.Core;
using MiAmor.Data;
using System.Data.Entity;


namespace MiAmor.Services
{

    public interface ICampaignDeliveryService : IEntityService<CampaignDelivery>
    {
        CampaignDelivery GetById(int Id);
        PagedDataResponse<CampaignDelivery> GetFilteredCampaignDeliveries(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions);
    }

    public partial class CampaignDeliveryService : EntityService<CampaignDelivery>, ICampaignDeliveryService
    {
        #region Fields


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public CampaignDeliveryService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<CampaignDelivery>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public CampaignDelivery GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public PagedDataResponse<CampaignDelivery> GetFilteredCampaignDeliveries(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            List<CampaignDelivery> CampaignDeliveries;
            int TotalItems = 0;
            if (FilterElements != null)
            {
                IQueryable<CampaignDelivery> query = _dbset;
                foreach (FilterKeyValue filter in FilterElements)
                {
                    if (filter.MyKey != null && filter.MyValue != null)
                    {
                        switch (filter.MyKey)
                        {
                            case "Id":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.Id).Contains(filter.MyValue));
                                break;
                            /*case "CampignId":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.CampignId).Contains(filter.MyValue));
                                break;*/
                            case "Description":
                                query = query.Where(c => c.Description.Contains(filter.MyValue));
                                break;
                            case "DeliveryPrice":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.DeliveryPrice).Contains(filter.MyValue));
                                break;
                            case "MaxUnitsPerDelivery":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.MaxUnitsPerDelivery).Contains(filter.MyValue));
                                break;
                            case "Active":
                               bool boolActive;
                                if (filter.MyValue=="1")
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
                                CampaignDeliveries = (from c in _dbset
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
                CampaignDeliveries = query.ToList();
            }
            else
            {
                CampaignDeliveries = (from c in _dbset
                             select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                TotalItems = _dbset.Count();
            }
            PagedDataResponse<CampaignDelivery> PagedDataResponse = new PagedDataResponse<CampaignDelivery>(TotalItems, CampaignDeliveries.Count(), CampaignDeliveries, 0);
            return PagedDataResponse;
        }

        #endregion

    }
}
