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
    public interface IVendorEventPostService : IEntityService<VendorEventPost>
    {
        VendorEventPost GetById(int Id);

        PagedDataResponse<VendorEventPost> GetVendorEventPostByVendorId(PagedDataRequest PagedDataRequest, int VendorId);

    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class VendorEventPostService : EntityService<VendorEventPost>, IVendorEventPostService
    {
        #region Fields
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public VendorEventPostService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<VendorEventPost>();

        }

        #endregion

        #region Methods

     
        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public VendorEventPost GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public PagedDataResponse<VendorEventPost> GetVendorEventPostByVendorId(PagedDataRequest PagedDataRequest, int VendorId)
        {
            List<VendorEventPost> ListEvent = (from v in _dbset.Include(va => va.EventPost)
                                             .Include(v => v.EventPost.EventPostPictures.Select(av => av.Picture))
                                             where v.VendorId == VendorId
                                             orderby v.EventPost.DisplayOrder
                                             select v).Skip(PagedDataRequest.RowsPerPage * (PagedDataRequest.PageNumber - 1)).Take(PagedDataRequest.RowsPerPage).AsNoTracking().ToList();

            int TotalItems = 0;
            PagedDataResponse<VendorEventPost> PagedDataResponse = new PagedDataResponse<VendorEventPost>(TotalItems, ListEvent.Count, ListEvent);
            return PagedDataResponse;
        }

        #endregion
        
    }

}
