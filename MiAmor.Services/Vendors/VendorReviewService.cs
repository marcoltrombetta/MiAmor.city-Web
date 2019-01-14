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
    public interface IVendorReviewService : IEntityService<VendorReview>
    {
        VendorReview GetById(int Id);
        PagedDataResponse<VendorReview> GetVendorsReviewPagingByVenodrId(PagedDataRequest PagedDataRequest, int VendorId);
        int GetAvgReview(int VendorId);

    }

    public partial class VendorReviewService : EntityService<VendorReview>, IVendorReviewService
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
        public VendorReviewService(ICacheManager CacheManager, IContext context)
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
        private const string VENDORSREVIEW_BY_ID_KEY = "MiAmor.vendor.id-{0}";             
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string VENDORREVIEW_INCLUDE_KEY = "MiAmor.vendorincludeAddress-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>

        #endregion
        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public VendorReview  GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public PagedDataResponse<VendorReview> GetVendorsReviewPagingByVenodrId(PagedDataRequest PagedDataRequest, int VendorId)
        {
            try
            {
                List<VendorReview> VendorReview = new List<VendorReview>();
                int QueryTotalCount;

                if (PagedDataRequest.SearchFor != "")
                {
                    VendorReview = (from v in _dbset
                                    .Include(vc => vc.Customer)
                                    where (v.VendorId == VendorId) &&
                                    ((v.ReviewText.Contains(PagedDataRequest.SearchFor) || (v.Title.Contains(PagedDataRequest.SearchFor))))
                                    orderby (PagedDataRequest.SortIndexName)
                                    select v).Skip(PagedDataRequest.RowsPerPage * (PagedDataRequest.PageNumber - 1)).Take(PagedDataRequest.RowsPerPage).AsNoTracking().ToList();
                }
                else
                {
                    VendorReview = (from v in _dbset
                                    .Include(vc => vc.Customer)
                                    where (v.VendorId == VendorId)
                                    orderby (PagedDataRequest.SortIndexName)
                                    select v).Skip(PagedDataRequest.RowsPerPage * (PagedDataRequest.PageNumber - 1)).Take(PagedDataRequest.RowsPerPage).AsNoTracking().ToList();
                    //check later
                }
                int TotalItems = 0;
                PagedDataResponse<VendorReview> PagedDataResponse = new PagedDataResponse<VendorReview>(TotalItems, VendorReview.Count(), VendorReview);
                return PagedDataResponse;
            }
            catch (Exception e)
            {
                int i = 0;
            }
            return null;
        }

        public int GetAvgReview(int VendorId)
        {
            List<VendorReview> ListVendorReview =(from vr in _dbset
                                 where vr.VendorId==VendorId
                                select vr).ToList();
            if (ListVendorReview.Count() == 0)
                return 0;
            int sumR=0;
            foreach(VendorReview VR in ListVendorReview)
            {
                sumR+=VR.Rating;

            }
            return Convert.ToInt32( Math.Ceiling(Convert.ToDouble( sumR / ListVendorReview.Count())));
        }
     
        #endregion

    }

    }