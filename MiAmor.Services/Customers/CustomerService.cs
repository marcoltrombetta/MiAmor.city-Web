using System;
using System.Web;
using System.Web.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiAmor.Core;
using MiAmor.Data;
using System.Data.Entity;

using MiAmor.Services;
using System.Linq.Expressions;
using System.Data.Entity.SqlServer;
//using MiAmor.Web.Models;

namespace MiAmor.Services
{
    public interface ICustomerService : IEntityService<Customer>
    {
        void SetCustomerCookie(string CustId);
        Customer GetByCookie();
        Customer GetCustomerByEmail(string Email);
        Customer GetById(int Id);
        bool IsCustExistsByEmail(string Email);
        Customer GetCustomerByLogIn(string Email, string password);
        Customer GetByTokenId(int TokenId);
        Customer GetByCustId(int CustId);
        Customer GetByCustStringId(string CustId);
        string SetCustId(string CustId);
        PagedDataResponse<Customer> GetFilteredCustomers(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions);
        List<Customer> GetCustomerByPartialName(string PartialName, int NumOfRows);
    }

    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class CustomerService : EntityService<Customer>, ICustomerService
    {
        #region Fields
        private IEncryption _Encryption;
        private readonly string CookieName = "userInfo";
        
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public CustomerService(IContext context, IEncryption Encryption)
            : base(context)
        {
            
            _Encryption = Encryption;
            _context = context;
            _dbset = _context.Set<Customer>();            

        }

        #endregion

        #region Methods
        public Customer GetByCustStringId(string CustId)
        {
            int Id = Convert.ToInt32(_Encryption.DecryptStrLink(CustId));
            Customer Customer = (from c in _dbset
                                 where c.Id == Id
                                 select c).FirstOrDefault();

            return Customer;

        }
        public Customer GetCustomerByEmail(string Email)
        {
            Customer Customer = (from c in _dbset
                                 where c.Email == Email
                                 select c).FirstOrDefault();
            return Customer;
        }
       public void SetCustomerCookie(string CustId)
        {
            HttpCookie aCookie;
            string cookieName;
            HttpContext.Current.Response.Cookies.Clear();
            int limit = HttpContext.Current.Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                cookieName = HttpContext.Current.Request.Cookies[i].Name;     
                aCookie = new HttpCookie(cookieName);
                aCookie.Value = null;
                aCookie.Expires = DateTime.Now.AddYears(-1);
                HttpContext.Current.Response.Cookies.Add(aCookie);              
            }
           
            var cookie = HttpContext.Current.Response.Cookies[CookieName];            
            if(cookie == null)
            {
                cookie = new HttpCookie(CookieName, CustId);
             }
            else
            {
                cookie.Expires = DateTime.Now.AddDays(-100);
                HttpContext.Current.Response.Cookies.Add(cookie);
                //HttpContext.Current.Session.Clear();
            }
            cookie = new HttpCookie(CookieName, CustId);
            cookie.Expires = DateTime.Now.AddDays(365);
            cookie.Value = CustId;
            cookie.HttpOnly = true;           
            cookie.Secure = false;
            HttpContext.Current.Response.Cookies.Add(cookie);

        }
        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public Customer GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public bool IsCustExistsByEmail(string Email)
        {
            Customer Customer= _dbset.FirstOrDefault(x => x.Email == Email);
            if(Customer ==null)
                return false;
            else
                return true;
        }

        public Customer GetCustomerByLogIn(string Email, string Password)
        {
            Customer Customer= _dbset.FirstOrDefault(x => x.Email == Email && x.Password == Password);

            Customer.CustId = _Encryption.EncryptStrLink(Customer.Id.ToString());            
            return Customer;
        }
        public Customer GetByCustId(int CustId)
        {
            Customer Customer = (from c in _dbset
                                 where c.Id == CustId
                                 select c).FirstOrDefault();
           
            return Customer;
        }

        public Customer GetByCookie()
        {
            //string CustId = HttpContext.Current.Request.Cookies[CookieName].Value;
            //int Id = Convert.ToInt32(_Encryption.DecryptStrLink(CustId));
            //fuck u 
            Customer Customer = (from c in _dbset
                                 where c.Id == 86
                                 select c).FirstOrDefault();

            return Customer;
        }

        public string SetCustId(string CustId)
        {

            string RetCustId = _Encryption.EncryptStrLink(CustId );
            return RetCustId;
        }

        public Customer GetByTokenId(int TokenId)
        {
            Customer Customer = (from c in _dbset
                                 where c.TokenId == TokenId
                                 select c).FirstOrDefault();
           
            return Customer;
        }

        public PagedDataResponse<Customer> GetFilteredCustomers(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        { 
          List<Customer> Customers;
            int TotalItems =0;
            if (FilterElements != null)
            {

                IQueryable<Customer> query = _dbset;
                foreach (FilterKeyValue filter in FilterElements)
                {
                    if (filter.MyKey != null && filter.MyValue != null)
                    {
                        switch (filter.MyKey)
                        {
                            case "Id":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.Id).Contains(filter.MyValue));
                                break;
                            case "FirstName":
                                query = query.Where(c => c.FirstName.Contains(filter.MyValue));
                                break;
                            case "LastName":
                                query = query.Where(c => c.LastName.Contains(filter.MyValue));
                                break;
                            case "Email":
                                query = query.Where(c => c.Email.Contains(filter.MyValue));
                                break;
                            case "CustomerType":
                                int CustomerTypeId = Convert.ToInt16(filter.MyValue);
                                query = query.Where(c => c.CustomerTypeId == CustomerTypeId);
                                break;
                            case "Gender":
                                // bool boolGender = Convert.ToBoolean(filter.Value);
                                bool boolGender;
                                if (filter.MyValue=="1")
                                {
                                    boolGender = true;
                                }
                                else
                                {
                                    boolGender = false;
                                }
                                query = query.Where(c => c.Gender == boolGender);
                                break;
                            case "Phone":
                                query = query.Where(c => c.Phone.Contains(filter.MyValue));
                                break;
                            case "City":
                                query = query.Where(c => c.City.Name.Contains(filter.MyValue));
                                break;
                            case "Age":
                                //string StringAge = Convert.ToString((double)c.Age);
                                //query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.DateOfBirth).Contains(filter.Value));
                                break;

                            default:
                                Customers = (from c in _dbset
                                             .Include(ct=>ct.CustomerType)
                                             .Include(ct => ct.City)
                                             select c).OrderBy(c => c.FirstName)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                                break;
                        }

                    }
                }
                query = query
                    .Include(ct => ct.CustomerType)
                    .Include(ct => ct.City)
                    .OrderBy(c => c.FirstName)
                    .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize);
                Customers = query.ToList();
                TotalItems = query.Count();
            }
            else
            {
                Customers = (from c in _dbset
                              .Include(ct => ct.CustomerType)
                              .Include(ct => ct.City)
                             select c).OrderBy(c => c.FirstName)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                TotalItems = _dbset.Count();
                                  
            }
            PagedDataResponse<Customer> PagedDataResponse = new PagedDataResponse<Customer>(TotalItems, Customers.Count(), Customers, 0);
            return PagedDataResponse;
        }

        public List<Customer> GetCustomerByPartialName(string PartialName, int NumOfRows)
        {
            List<Customer> Customers = (from v in _dbset
                                 where v.FirstName.Contains(PartialName)
                                        select v).OrderBy(c => c.FirstName).Take(NumOfRows).ToList();
            return Customers;
        }
              
        #endregion

    }

}
