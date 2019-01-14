using AutoMapper;
using System.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;

using MiAmor.Services;
using MiAmor.Data;
using MiAmor.Core;
using MiAmor.Web.Models;
using MiAmor.Web.Filters;

namespace MiAmor.Web.Controllers
{
    public class CustomerController : Controller
    {
        #region fields
        private ICustomerMessageTypeService _CustomerMessageTypeService;
        private ICustomerService _CustomerService;
        private ICityService _CityService;
        private ITokenService _TokenService;
        private ICustomerMessageService _CustomerMessageService;
        private ICustomerBookmarkService _CustomerBookmarkService;
        private IVendorReviewService _VendorReviewService;
        private ICampaignService _CampaignService;
        private ICouponService _CouponService;
        #endregion

        #region ctr
        public CustomerController(ICustomerService CustomerService, ITokenService TokenService, ICityService CityService,
            IVendorReviewService VendorReviewService,
            ICustomerMessageTypeService CustomerMessageTypeService,
            ICustomerMessageService CustomerMessageService, ICustomerBookmarkService CustomerBookmarkService, ICampaignService CampaignService,
            ICouponService CouponService
            )
        {
            
            _CustomerService = CustomerService;
            _TokenService = TokenService;
            _CityService = CityService;
            _CustomerMessageTypeService = CustomerMessageTypeService;
            _CustomerMessageService = CustomerMessageService;
            _CustomerBookmarkService = CustomerBookmarkService;
            _VendorReviewService = VendorReviewService;
            _CampaignService = CampaignService;
            _CouponService = CouponService;

            ViewBag.ContentId = 1;
        }

        #endregion
        #region Actions

        public ActionResult CustomerDetailsLeftbar(string CustId)
        {
            
            return PartialView("_CustomerDetailsLeftbar");
        }


        //public ActionResult CustomerContentPartial(string CustId, int ContentId)
        //{
        //    if (ContentId == 1) //Quickies
        //    {
        //      return GetCustomerQuickies(CustId);
        //    }
        //    else if (ContentId == 2)//Profile
        //    {
        //        return GetCustomerProfile();
        //    }
        //    else if (ContentId == 3) //Campains
        //    {
        //        return GetCustomerCampaigns();
        //    }
        //    else if (ContentId == 4)//Messages
        //    {
        //        return CustomerMessages(CustId);
        //    }
        //    else if (ContentId == 5) //History
        //    {
        //        return GetCustomerHistory(CustId);
        //    }
        //    else if (ContentId == 6)//Settings
        //    {
        //        return GetCustomerSettings(CustId);
        //    }
        //    else
        //    {
        //        return GetCustomerQuickies(CustId);
        //    }
        //}
        public ActionResult CustomerBookmarkList(int PageNumber)
        {
            CustomerBookmarkListingModel CustomerBookmarkListingModel = GetCustomerBookmarkUtility( 1,PageNumber,20);

            if (CustomerBookmarkListingModel == null)
            {
                CustomerBookmarkListingModel = new CustomerBookmarkListingModel();
                CustomerBookmarkListingModel.CustomerBookmark = new List<CustomerBookmark>();
            }

            return View("CustomerBookmarkList", CustomerBookmarkListingModel);
        }
        public ActionResult GetCustomerLeftbar(string CustId)
        {
            return View("CustomerDetailsLeftbar");
        }

        public ActionResult GetCustomerProfile()
        {
            CustomerProfileCities customerCities = new CustomerProfileCities();
            customerCities = CustomerProfileUtility(customerCities);
            return View("CustomerProfile", customerCities);
        }

        public ActionResult GetCustomerQuickies(string CustId)
        {
            ViewBag.CustId = CustId;
            return PartialView("_CustomerQuickies");
        }

        public ActionResult GetCustomerCampaigns()
        {
            Customer Customer = _CustomerService.GetByCookie();
            List<Coupon> Coupons=_CouponService.GetByCustId(Customer.Id);
            return View("CustomerCoupons",Coupons);
        }

        public ActionResult GetCustomerQuickiesCampaigns()
        {
            List<Campaign> Campaigns = _CampaignService.GetQuickiesCampaigns();
            return PartialView("_CustomerQuickiesCampaignsList", Campaigns);
        }

        public ActionResult GetCustomerHistory(string CustId)
        {
            return PartialView("_CustomerHistory");
        }

        public ActionResult GetCustomerSettings()
        {
            return View("CustomerSettings");
        }

        public ActionResult Index()
        {
            Customer Customer= _CustomerService.GetByCookie();
            string CustId =_CustomerService.SetCustId(Customer.Id.ToString());
            ViewBag.CustId = CustId;
            return View("CustomerQuickies");
        }
       

        [HttpPost]
        public ActionResult SaveCustomerProfile(CustomerProfile Customer)
        {
            if (ModelState.IsValid)
            {
                Customer Cust = Mapper.Map<CustomerProfile, Customer>(Customer);
                 _CustomerService.Update(Cust);
            }

          return   Json(new { data = "OK" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RegisterCustomer(CustomerRegistration CustomerRegistration)
        {
             string ReturnToken="";
             ReturnedToken ReturnedToken = new ReturnedToken();
            if (ModelState.IsValid)
            {
                ReturnedToken = RegisterCustomerUtility(CustomerRegistration,true);
            }            
            return Json(new { data = ReturnedToken }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RegisterCustomerApp(CustomerRegistration CustomerRegistration)
        {
            string ReturnToken = "";
            ReturnedToken ReturnedToken = new ReturnedToken();
            if (ModelState.IsValid)
            {
                ReturnedToken = RegisterCustomerUtility(CustomerRegistration,false);
            }
            return Json(new { data = ReturnedToken }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CustomerPartial(Customer Customer, int ContentId)
        {
            if (ContentId == 1)
            {
                return PartialView("_CustomerProfile", Customer);
            }
            else if (ContentId == 3)
            {
                return PartialView("_CustomerPortfolio", Customer);
            }
            else if (ContentId == 4)
            {
                return PartialView("_CustomerEvents", Customer);
            }
            else if (ContentId == 5)
            {
                return PartialView("_CustomerBlog", Customer);
            }
            else if (ContentId == 6)
            {
                return PartialView("_CustomerContact", Customer);
            }
            else
            {
                return PartialView("_CustomerProfile", Customer);
            }
        }

        [ValidateJsonUserForgeryToken]
        [HttpPost]
        public ActionResult GetCustomerDetailsApp()
        {
            Customer Customer = _CustomerService.GetByCookie();

            CustomerDetails CustomerDetails =new CustomerDetails();
            Customer Cust = _CustomerService.GetByCustId(Customer.Id);
            CustomerDetails =
            Mapper.Map<Customer, CustomerDetails>(Cust);
          
            return Json(new { data = CustomerDetails }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCustomerQuickiesMessages(string CustId)
        {
            Customer Customer = _CustomerService.GetByCookie();
            List<CustomerMessage> CustomerMessages = _CustomerMessageService.GetQuickiesMessagesByCustId(Customer.Id);
          
           return PartialView("_CustomerQuickiesMessageList", CustomerMessages);
        }

        //[HttpPost]
        public ActionResult GetCustomerMessages(string CustId,int PageNumber)
        {
            CustomerMessageListingModel CustomerMessageListingModel = GetCustomerMessageUtility( PageNumber);
            return PartialView("_CustomerMessageList", CustomerMessageListingModel);
        }

        public ActionResult CustomerMessages(string CustId)
        {
            CustomerMessageModel CustomerMessage = new CustomerMessageModel();
            ViewBag.CustId=CustId;
            return PartialView("CustomerMessage", CustomerMessage);
        }

        public ActionResult GetCustomerBookmarks( int PageNumber=1)
        {
            Customer Customer = _CustomerService.GetByCookie();
            CustomerBookmarkListingModel CustomerBookmarkListingModel = GetCustomerBookmarkUtility(10,Customer.Id, PageNumber);

            if (CustomerBookmarkListingModel == null) {
                CustomerBookmarkListingModel = new CustomerBookmarkListingModel();
                CustomerBookmarkListingModel.CustomerBookmark = new List<CustomerBookmark>();
            }

            return View("CustomerBookmarkList", CustomerBookmarkListingModel);
        }
        public ActionResult GetCustomerQuickiesBookmarks(int PageNumber = 1)
        {
            Customer Customer = _CustomerService.GetByCookie();
            CustomerBookmarkListingModel CustomerBookmarkListingModel = GetCustomerBookmarkUtility(3,Customer.Id, PageNumber);

            if (CustomerBookmarkListingModel == null)
            {
                CustomerBookmarkListingModel = new CustomerBookmarkListingModel();
                CustomerBookmarkListingModel.CustomerBookmark = new List<CustomerBookmark>();
            }

            return PartialView("_CustomerQuickiesBookmarkList", CustomerBookmarkListingModel);
        }

        [HttpPost]
        public ActionResult LoginCustomerApp(LoginViewModel LoginViewModel)
        {
            string ReturnToken = "";          
            ReturnedToken ReturnedToken = new ReturnedToken();
            if (ModelState.IsValid)
            {
                ReturnedToken = LoginCustomerUtility(LoginViewModel,false);                            
            }           
            return Json(new { data = ReturnedToken }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoginCustomer(LoginViewModel LoginViewModel)
        {
            string ReturnToken = "";
            ReturnedToken ReturnedToken = new ReturnedToken();
            if (ModelState.IsValid)
            {
                ReturnedToken = LoginCustomerUtility(LoginViewModel,LoginViewModel.RememberMe);
            }
            return Json(new { data = ReturnedToken }, JsonRequestBehavior.AllowGet);
        }

         public ActionResult CustomerDetails(int CustNumber)
        {
            Customer Customer = _CustomerService.GetByCustId(CustNumber);
            return View(Customer);

        }
        #endregion
         #region mobile
         [ValidateJsonUserForgeryToken]
         [HttpPost]
         public ActionResult MAPP_CustomerReviewVendor(CustomerReviewVendorModel CustomerReviewVendorModel)
         {
             bool Result = CustomerReviewVendorUtility(CustomerReviewVendorModel);
            
             return Json(new { data = Result }, JsonRequestBehavior.AllowGet);
         }
        #endregion
         #region utilities
 //Get All the customer bookmarks by paging
         private CustomerBookmarkListingModel GetCustomerBookmarkUtility(int ReturnedRecords,int CustId, int PageNumber)
         {
             try
             {
                 PagedDataRequest PagedDataRequest = new PagedDataRequest(PageNumber, "", "", ReturnedRecords);

                 PagedDataResponse<CustomerBookmark> CustomerBookmarkPagedDataResponse = _CustomerBookmarkService.GetByCustId(PagedDataRequest, CustId);

                 CustomerBookmarkPagingModel CustomerBookmarkPagingModel = new CustomerBookmarkPagingModel(PageNumber, PagedDataRequest.RowsPerPage, CustomerBookmarkPagedDataResponse.total, CustId);

                 CustomerBookmarkListingModel CustomerBookmarkListingModel = new CustomerBookmarkListingModel();
                 CustomerBookmarkListingModel.CustomerBookmarkPagingModel = CustomerBookmarkPagingModel;
                 CustomerBookmarkListingModel.CustomerBookmark = CustomerBookmarkPagedDataResponse.rows;
                 //CustomerBookmarkPagingModel.CustomerMessageModel =
                 //Mapper.Map<List<CustomerBookmark>, List<CustomerMessageModel>>(CustomerMessagePagedDataResponse.rows);
                 return CustomerBookmarkListingModel;
             }
            catch (Exception e)
             {
                 int i = 0;
             }
             return null;
         }


        //Get All the customer messages by paging
         private CustomerMessageListingModel GetCustomerMessageUtility( int PageNumber)
        {

            Customer Customer = _CustomerService.GetByCookie();
            try
            {
                PagedDataRequest PagedDataRequest = new PagedDataRequest(PageNumber, "", "");

                PagedDataResponse<CustomerMessage> CustomerMessagePagedDataResponse = _CustomerMessageService.GetByCustId(PagedDataRequest, Customer.Id);

                CustomerMessagePagingModel CustomerMessagePagingModel = new CustomerMessagePagingModel(PageNumber, PagedDataRequest.RowsPerPage, CustomerMessagePagedDataResponse.total, Customer.Id);

                CustomerMessageListingModel CustomerMessageListingModel = new CustomerMessageListingModel();
                CustomerMessageListingModel.CustomerMessagePagingModel = CustomerMessagePagingModel;
                CustomerMessageListingModel.CustomerMessageModel =
                Mapper.Map<List<CustomerMessage>, List<CustomerMessageModel>>(CustomerMessagePagedDataResponse.rows);
                return CustomerMessageListingModel;
            }
            catch (Exception e)
            {
                int i = 0;
            }
            return null;
        }
        //add a review of a vendor done by customer
        private bool CustomerReviewVendorUtility(CustomerReviewVendorModel CustomerReviewVendorModel)
        {
            Customer Customer = _CustomerService.GetByCookie();

             VendorReview VendorReview = new VendorReview();
             if (Customer.Id>0)
             {
                 Customer Cust = _CustomerService.GetByCustId(Customer.Id);
                 VendorReview =
                   Mapper.Map<CustomerReviewVendorModel, VendorReview>(CustomerReviewVendorModel);
                 VendorReview.CustomerId = Cust.Id;
                 VendorReview.CreatedOnUtc = DateTime.Now;
                 _VendorReviewService.Create(VendorReview);
                 return true;
             }
             return false;
        }
         //Regoster a user from application
        //Create a token for this user
        //update customer token and return
        private ReturnedToken RegisterCustomerUtility(CustomerRegistration CustomerRegistration,bool IsCookie)
        {
            bool CustExistsByEmail = _CustomerService.IsCustExistsByEmail(CustomerRegistration.Email);
            ReturnedToken ReturnedToken = new ReturnedToken();
            if (!CustExistsByEmail)
            {
                   Customer Customer =
                  Mapper.Map<CustomerRegistration, Customer>(CustomerRegistration);

                _CustomerService.Create(Customer);
                string StrToekn = _TokenService.CreateNewTokenForCustomer(Customer,1);
                ReturnedToken.Token = StrToekn;
                ReturnedToken.CustId = _CustomerService.SetCustId(Customer.Id.ToString());
                if (IsCookie)
                {
                    _CustomerService.SetCustomerCookie(ReturnedToken.CustId);
                }          
            }
            

            return ReturnedToken;
        }

        private ReturnedToken LoginCustomerUtility(LoginViewModel LoginViewModel,bool IsCookie)
        {
            Customer Customer= _CustomerService.GetCustomerByLogIn(LoginViewModel.Email, LoginViewModel.Password);
            ReturnedToken ReturnedToken = new ReturnedToken();
            if (Customer != null)
            {
               
                string StrToekn = "";
                if (LoginViewModel.RememberMe != null)
                {                    
                        StrToekn = _TokenService.CreateNewTokenForCustomer(Customer, Convert.ToInt32(LoginViewModel.RememberMe));                    
                }                
                if (IsCookie)
                {
                    _CustomerService.SetCustomerCookie(Customer.CustId);
                }
                ReturnedToken.CustId = Customer.CustId;
                ReturnedToken.FirstName = Customer.FirstName;
                ReturnedToken.Token = StrToekn;
                return ReturnedToken;
            }
            else
            {
                return ReturnedToken;
            }
            
        }

        private CustomerProfileCities CustomerProfileUtility(CustomerProfileCities Customer)
        {
            Customer Cust = _CustomerService.GetByCookie();

            CustomerProfileCities CustomerProfileCities = new CustomerProfileCities();
            Customer UpCustomer = _CustomerService.GetByCustId(Cust.Id);
            CustomerProfileCities.Cities = _CityService.GetAll().ToList();
            CustomerProfileCities.CustomerProfile = Mapper.Map<Customer, CustomerProfile>(UpCustomer);

            return CustomerProfileCities;
        }


        #endregion
    }
}