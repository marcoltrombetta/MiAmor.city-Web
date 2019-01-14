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
    public interface IVendorService : IEntityService<Vendor>
    {
        Vendor GetById(int Id);
        PagedDataResponse<Vendor> GetVendorsPagingByCategory(PagedDataRequest PagedDataRequest, int CategoryId);

        PagedDataResponse<Vendor> GetVendorsPagingByCategoryAndGeo(PagedDataRequest PagedDataRequest, int CategoryId, double Lat, double Lng);
        List<Vendor> GetVendorsForAddress();
        Vendor GetBoxDetailsById(int Id);
        Vendor GetVendorHomePage(int Id);
        List<Vendor> GetVendorByPartialName(string PartialName, int NumOfRows);
        PagedDataResponse<Vendor> GetFilteredVendors(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions);
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class VendorService : EntityService<Vendor>, IVendorService
    {
        #region Fields
        private readonly ICacheManager _CacheManager;
      

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public VendorService(ICacheManager CacheManager, IContext context)
            : base(context)
        {
            _CacheManager = CacheManager;
            //_context = context;
            //_dbset = _context.Set<Vendor>();

        }

        #endregion

        #region Constants
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : vendor ID
        /// </remarks>
        private const string VENDORS_BY_ID_KEY = "MiAmor.vendor.id-{0}";             
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string VENDOR_INCLUDE_KEY = "MiAmor.vendorincludeAddress-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string NAVIGATIONCATEGORIES_PATTERN_KEY = "MiAmor.navigationcategory-{0}";
        /// <summary>
        /// Key pattern to vendor index page+sidebar
        /// </summary>
        private const string VENDORS_BY_ID_KEY_HomePage = "MiAmor.vendorHomePage.id-{0}";

        #endregion
        #region Methods
        /// <summary>
        /// Gets an list of vendors by text name
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
       public List<Vendor> GetVendorByPartialName(string PartialName, int NumOfRows)
        {
            List<Vendor> Vendors = (from v in _dbset
                                    where v.Name.Contains(PartialName)
                                    select v).OrderBy(c => c.Name).Take(NumOfRows).ToList();
            return Vendors;
        }
        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public Vendor GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }
        /// <summary>
        /// Gets the vendor details for display
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public Vendor GetBoxDetailsById(int Id)
        {
            Vendor Vendor = (from v in _dbset.Include(va => va.Addresses.Select(a => a.Cities))
                       .Include(va => va.Addresses.Select(a => a.Neighbourhood))
                       .Include(va => va.Campaigns.Select(a => a.CampaignPictures.Select(cp=>cp.Picture)))
                                    where v.Id == Id
                                    select v).FirstOrDefault();
            return Vendor;
        }

        public IList<Vendor> GetAllVendorssDisplayedOnHomePage()
        {
            var query = from v in _dbset
                        orderby v.DisplayOrder, v.Name
                        where v.Published &&
                        !v.Deleted &&
                        v.ShowOnHomePage
                        select v;
            var vendors = query.ToList();
            return vendors;
        }

        public List<Vendor> GetVendorsForAddress()
        {

            List<Vendor> Vendors = (from v in _dbset.Include(va => va.Addresses.Select(a => a.Cities.Neighbourhood))
                         .Include(va => va.Addresses.Select(a => a.Neighbourhood))
                         .Include(va => va.VendorCategoryVendors.Select(vc => vc.VendorCategory))
                                    where v.ManagerId == 0
                                    select v).Take(100).ToList();
                 Vendors = (from v in Vendors
                            orderby Guid.NewGuid()
                            select v).Take(30).ToList();
                 foreach (Vendor vendor in Vendors)
                 {
                     vendor.ManagerId = 1;
                 }
                 _context.SaveChanges();
                 return Vendors;          
        }

        public Vendor GetVendorHomePage(int Id)
        {
            
             string key = string.Format(VENDORS_BY_ID_KEY_HomePage, Id);
            return _CacheManager.Get(key, () =>
            {
           Vendor Vendor = (from v in _dbset.Include(va => va.Addresses.Select(a => a.Cities.Neighbourhood))
                        .Include(va => va.Addresses.Select(a => a.Country))
                        .Include(va => va.VendorCategoryVendors.Select(vc => vc.VendorCategory))
                        .Include(va => va.VendorOpeningHours)
                        .Include(va => va.Addresses)
                        .Include(vp => vp.VendorPictures.Select(p=>p.Picture))
                                    where v.Id == Id
                                    select v).AsNoTracking().SingleOrDefault();
           return Vendor;
            });
        }
        public PagedDataResponse<Vendor> GetVendorsPagingByCategory(PagedDataRequest PagedDataRequest, int CategoryId)
        {
          
            List<Vendor> Vendors = new List<Vendor>();
            int QueryTotalCount ;
          
           
            if (PagedDataRequest.SearchFor != "")
            {
                Vendors = (from v in _dbset.Include(va => va.Addresses.Select(a => a.Cities))
                         .Include(va => va.Addresses.Select(a => a.Neighbourhood))
                         .Include(va => va.VendorCategoryVendors.Select(vc => vc.VendorCategory))
                         .Include(va => va.Campaigns)
                           where (v.VendorCategoryVendors.Select(vc => vc.VendorCategory).Any(c => c.Id == CategoryId) || CategoryId == 0) &&
                         (v.Name.Contains(PagedDataRequest.SearchFor) )
                         orderby (PagedDataRequest.SortIndexName)
                         select v).Skip(PagedDataRequest.RowsPerPage * (PagedDataRequest.PageNumber - 1)).Take(PagedDataRequest.RowsPerPage).AsNoTracking().ToList();
            }
            else {
               Vendors = (from v in _dbset.Include(va => va.Addresses.Select(a => a.Cities))
                         .Include(va => va.Addresses.Select(a => a.Neighbourhood))
                         .Include(va => va.VendorCategoryVendors.Select(vc => vc.VendorCategory))
                         .Include(va => va.Campaigns)
                          where (v.VendorCategoryVendors.Select(vc => vc.VendorCategory).Any(c => c.Id == CategoryId))
                         orderby (PagedDataRequest.SortIndexName)
                         select v).Skip(PagedDataRequest.RowsPerPage * (PagedDataRequest.PageNumber - 1)).Take(PagedDataRequest.RowsPerPage).AsNoTracking().ToList();
                //check later
            }
           
            int TotalItems = 0;
            PagedDataResponse<Vendor> PagedDataResponse = new PagedDataResponse<Vendor>(TotalItems, Vendors.Count(), Vendors);
            return PagedDataResponse;
        }

        //gets all the vendors in a range of distance by latitude and longtitude
        public PagedDataResponse<Vendor> GetVendorsPagingByCategoryAndGeo(PagedDataRequest PagedDataRequest, int CategoryId, double Lat, double Lng)
        {
            List<Vendor> Vendors = new List<Vendor>();

            double maxlat = 0;
            double minlat = 0;
            double maxlng = 0;
            double minlng = 0;



            maxlat = Lat + 1 / 69.17;
            minlat = Lat - (maxlat - Lat);
            maxlng = Lng + 1 / (Math.Cos(minlat * Math.PI / 180) * 69.17);
            minlng = Lng - (maxlng - Lng);

            if (PagedDataRequest.SearchFor != "")
            {
                Vendors = (from v in _dbset.Include(va => va.Addresses.Select(a => a.Cities))
                             .Include(va => va.Addresses.Select(a => a.Neighbourhood))
                            .Include(va => va.VendorCategoryVendors.Select(vc => vc.VendorCategory))
                            .Include(va => va.Campaigns)
                           where (v.VendorCategoryVendors.Select(vc => vc.VendorCategory).Any(c => c.Id == CategoryId) || CategoryId == 0) &&
                           (v.Addresses.Any(a => a.Latitude >= minlat && a.Latitude <= maxlat &&
                           a.Longitude >= minlng && a.Longitude <= maxlng)) &&
                           (v.Name.Contains(PagedDataRequest.SearchFor) || String.IsNullOrEmpty(PagedDataRequest.SearchFor))
                           orderby (PagedDataRequest.SortIndexName)
                           select v).Skip(PagedDataRequest.RowsPerPage * (PagedDataRequest.PageNumber - 1)).Take(PagedDataRequest.RowsPerPage).AsNoTracking().ToList();
            }
            else {
                if (CategoryId == 0)
                {
                    Vendors = (from v in _dbset.Include(va => va.Addresses.Select(a => a.Cities))
                              .Include(va => va.Addresses.Select(a => a.Neighbourhood))
                              .Include(va => va.VendorCategoryVendors.Select(vc => vc.VendorCategory))
                              .Include(va => va.Campaigns)
                               where (v.Addresses.Any(a => a.Latitude >= minlat && a.Latitude <= maxlat &&
                               a.Longitude >= minlng && a.Longitude <= maxlng)) &&
                               (v.Name.Contains(PagedDataRequest.SearchFor) || String.IsNullOrEmpty(PagedDataRequest.SearchFor))
                               orderby (PagedDataRequest.SortIndexName)
                               select v).Skip(PagedDataRequest.RowsPerPage * (PagedDataRequest.PageNumber - 1)).Take(PagedDataRequest.RowsPerPage).AsNoTracking().ToList();
                }
                else
                {

                    Vendors = (from v in _dbset.Include(va => va.Addresses.Select(a => a.Cities))
                                     .Include(va => va.Addresses.Select(a => a.Neighbourhood))
                                     .Include(va => va.VendorCategoryVendors.Select(vc => vc.VendorCategory))
                                      .Include(va => va.Campaigns)
                               where (v.VendorCategoryVendors.Select(vc => vc.VendorCategory).Any(c => c.Id == CategoryId) || CategoryId == 0) &&
                               (v.Addresses.Any(a => a.Latitude >= minlat && a.Latitude <= maxlat &&
                               a.Longitude >= minlng && a.Longitude <= maxlng))
                               orderby (PagedDataRequest.SortIndexName)
                               select v).Skip(PagedDataRequest.RowsPerPage * (PagedDataRequest.PageNumber - 1)).Take(PagedDataRequest.RowsPerPage).AsNoTracking().ToList();
                }
            }
            //var kaka = from z in _dbset
            //           where z.Latitude >= minlat
            //           && z.Latitude <= maxlat
            //           && z.Longitude >= minlng
            //           && z.Longitude <= maxlng
            //           select z;

            int TotalItems = 0;
            PagedDataResponse<Vendor> PagedDataResponse = new PagedDataResponse<Vendor>(TotalItems, Vendors.Count(), Vendors.ToList());
            return PagedDataResponse;
        }

        public PagedDataResponse<Vendor> GetFilteredVendors(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            List<Vendor> Vendors;
            int TotalItems = 0;
            if (FilterElements != null)
            {
                IQueryable<Vendor> query = _dbset.Include(va => va.Addresses.Select(a => a.Neighbourhood))
                                            .Include(va => va.Addresses.Select(a => a.Cities));
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
                            case "Addresses[0].Cities.Name":
                                query = query.Where(c => c.Addresses.Any(ad => ad.Cities.Name.Contains(filter.MyValue)));
                                break;   
                            case "Addresses[0].PhoneNumber":
                                query = query.Where(c => c.Addresses.Any(ad => ad.PhoneNumber.Contains(filter.MyValue)));
                                break;                                
                            case "SiteUrl":
                                query = query.Where(c => c.SiteUrl.Contains(filter.MyValue));
                                break;
                            case "VendorContactPersonId":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.VendorContactPersonId).Contains(filter.MyValue));
                                break;
                            case "Addresses[0].MobileNumber":
                                query = query.Where(c => c.Addresses.Any(ad => ad.MobileNumber.Contains(filter.MyValue)));
                                break;
                            case "Addresses[0].MainEmail":
                                query = query.Where(c => c.Addresses.Any(ad => ad.MainEmail.Contains(filter.MyValue)));
                                break;   
                            default:
                                Vendors = (from c in _dbset
                                           .Include(va => va.Addresses.Select(a => a.Cities))
                                             select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                                break;
                        }
                    }
                }
                query = query                   
                    .OrderBy(c => c.Id)
                    .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize);
                Vendors = query.ToList();
                TotalItems = query.Count();
            }
            else
            {
                Vendors = (from c in _dbset
                           .Include(va => va.Addresses.Select(a => a.Neighbourhood))
                             .Include(va => va.Addresses.Select(a => a.Cities))
                              .Include(va => va.VendorCategoryVendors.Select(vc => vc.VendorCategory))
                             select c).OrderBy(c => c.Id)
                             .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).AsNoTracking().ToList(); 
                TotalItems = _dbset.Count();
            }
            PagedDataResponse<Vendor> PagedDataResponse = new PagedDataResponse<Vendor>(TotalItems, Vendors.Count(), Vendors, 0);
            return PagedDataResponse;
        }
        #endregion

    }

}
