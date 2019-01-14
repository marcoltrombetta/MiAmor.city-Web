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
    public interface IPriceListService : IEntityService<PriceList>
    {
        PriceList GetById(int Id);
        PagedDataResponse<PriceList> GetFilteredPriceLists(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions);
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class PriceListService : EntityService<PriceList>, IPriceListService
    {
        #region Fields
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public PriceListService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<PriceList>();

        }

        #endregion

        #region Methods


        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public PriceList GetById(int Id)
        {
            return (from pb in _dbset
                     .Include(pb => pb.PriceListVendorCategory)
                     .Include(pb => pb.PriceListItem)
                    where pb.Id == Id
                    orderby pb.Id
                    select pb).SingleOrDefault();

            //return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public PagedDataResponse<PriceList> GetFilteredPriceLists(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            List<PriceList> PriceLists;
            int TotalItems = 0;
            if (FilterElements != null)
            {

                IQueryable<PriceList> query = _dbset;
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
                                PriceLists = (from c in _dbset
                                              select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                                break;
                        }
                    }
                }
                query = query
                    .OrderBy(c => c.Id)
                    .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize);
                PriceLists = query.ToList();
                TotalItems = query.Count();
            }
            else
            {
                PriceLists = (from c in _dbset
                              select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                TotalItems = _dbset.Count();
            }
            PagedDataResponse<PriceList> PagedDataResponse = new PagedDataResponse<PriceList>(TotalItems, PriceLists.Count(), PriceLists, 0);
            return PagedDataResponse;
        }

        #endregion

    }

}
