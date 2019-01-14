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

    public interface ICustomerMessageService : IEntityService<CustomerMessage>
    {
        CustomerMessage GetById(int Id);
        PagedDataResponse<CustomerMessage> GetByCustId(PagedDataRequest PagedDataRequest, int CustId);
        List<CustomerMessage> GetQuickiesMessagesByCustId(int CustId);
    }

    public partial class CustomerMessageService : EntityService<CustomerMessage>, ICustomerMessageService
    {
        #region Fields
        private IEncryption _Encryption;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public CustomerMessageService(IContext context, IEncryption Encryption)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<CustomerMessage>();
            _Encryption = Encryption;

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public CustomerMessage GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }
        public List<CustomerMessage> GetQuickiesMessagesByCustId(int CustId) {
            return (from c in _dbset
                               .Include(m => m.CustomerMessageType)
                               .Include(m => m.Vendor)
                               where c.CustomerId == CustId
                               orderby c.VendorId
                               select c).Take(3).ToList();
        }
        
        public PagedDataResponse<CustomerMessage> GetByCustId(PagedDataRequest PagedDataRequest, int CustId)
        {
            List<CustomerMessage> CustomerMessage = new List<CustomerMessage>();
            int QueryTotalCount;

            if (PagedDataRequest.SearchFor != "")
            {
                CustomerMessage = (from c in _dbset
                                .Include(m => m.CustomerMessageType)
                                .Include(m => m.Vendor)
                                where c.CustomerId == CustId
                                orderby c.VendorId
                                select c).Skip(PagedDataRequest.RowsPerPage * (PagedDataRequest.PageNumber - 1)).Take(PagedDataRequest.RowsPerPage).AsNoTracking().ToList();
            }
            else
            {
                CustomerMessage = (from c in _dbset
                                .Include(m => m.CustomerMessageType)
                                .Include(m => m.Vendor)
                                where c.CustomerId == CustId
                                 orderby c.VendorId
                                select c).Skip(PagedDataRequest.RowsPerPage * (PagedDataRequest.PageNumber - 1)).Take(PagedDataRequest.RowsPerPage).AsNoTracking().ToList();
                //check later
            }
            int TotalItems = 0;
            PagedDataResponse<CustomerMessage> PagedDataResponse = new PagedDataResponse<CustomerMessage>(TotalItems, CustomerMessage.Count(), CustomerMessage);
            return PagedDataResponse;

        }
        
        #endregion

    }
}
