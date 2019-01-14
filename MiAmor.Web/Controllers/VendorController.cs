using AutoMapper;

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
using Newtonsoft.Json;


namespace MiAmor.Web.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
        #region fields
        IProductVendorService _VendorProductService;

        IVendorService _VendorService;
        IVendorReviewService _VendorReviewService;
        IVendorCategoryService _VendorCategoryService;
        IVendorCategoryCountService _VendorCategoryCountService;
        private readonly ICacheManager _CacheManager;
        IVendorAddressService _VendorAddressService;
        IVendorOpeningHoursService _VendorOpeningHoursService;
        ICustomerBookmarkService _CustomerBookmarkService;
        ICustomerService _CustomerService;
        ICampaignService _CampaignService;
        MiAmorContext db = new MiAmorContext();

        IVendorBlogPostService _VendorBlogPostService;
        IVendorEventPostService _VendorEventPostService;

        #endregion

        #region ctr
        public VendorController(IVendorService VendorService, IVendorCategoryService VendorCategoryService, IVendorCategoryCountService VendorCategoryCountService,
             ICacheManager CacheManager, IVendorReviewService VendorReviewService, IVendorAddressService VendorAddressService, IProductVendorService VendorProductService, IVendorOpeningHoursService VendorOpeningHoursService,
              ICustomerBookmarkService CustomerBookmarkService,IVendorBlogPostService VendorBlogPostService,IVendorEventPostService VendorEventPostService,
            ICustomerService CustomerService,
            ICampaignService CampaignService)
        {
            _VendorProductService = VendorProductService;
            _CampaignService = CampaignService;
            _VendorAddressService = VendorAddressService;
            _CacheManager = CacheManager;
            _VendorService = VendorService;
            _VendorCategoryService = VendorCategoryService;
            _VendorCategoryCountService = VendorCategoryCountService;
            _VendorOpeningHoursService = VendorOpeningHoursService;
            _CustomerBookmarkService = CustomerBookmarkService;
            _CustomerService = CustomerService;
            _VendorReviewService = VendorReviewService;
            _VendorBlogPostService = VendorBlogPostService;
            _VendorEventPostService = VendorEventPostService;
        }

        #endregion

        #region Constants
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : vendor ID
        /// </remarks>
        private const string VENDORVREADCRUMBS_BY_ID_KEY = "MiAmor.vendorbreadcrumbsbyid-{0}";
        #endregion

        #region Actions

        public ActionResult GetVendorListingModel(int CategoryId, int PageNumber)
        {
           SearchListingModel SearchListingModel=GetVendorSearchListingModel(CategoryId,"",PageNumber);

           return PartialView("_VendorListing", SearchListingModel.VendorSearchLisitingModel);
        }

        public ActionResult GetBlogVendorListingModel(int CategoryId, int PageNumber)
        {
           SearchListingModel SearchListingModel=GetVendorSearchListingModel(CategoryId,"",PageNumber);

           return PartialView("_VendorBlogListing", SearchListingModel.VendorBlogListingModel);
        }

        public ActionResult GetCampaignVendorListingModel(int CategoryId, int PageNumber)
        {
            SearchListingModel SearchListingModel = GetVendorSearchListingModel(CategoryId, "", PageNumber);

            return PartialView("_VendorCuponListing", SearchListingModel.VendorCampaignListingModel);
        }

        //show listing of all the vendors, should add paging abileties  
        public ActionResult GetVendorSidebarDetails(int VendorId)
        {
            Vendor Vendor = _VendorService.GetVendorHomePage(VendorId);
            ViewBag.VendorId = VendorId;

            return View("_VendorCompanySidebarDetails", Vendor);
        }
        public ActionResult Index(int Id, int ContentId=1)
        {
            Vendor Vendor = _VendorService.GetVendorHomePage(Id);
            ViewBag.ContentId = ContentId;
            ViewBag.VendorId = Id;
            return View("VendorCompanyProfile",Vendor);
        }


        public JsonResult IsOpenVendor(int Id)
         {
             bool IsOpen = _VendorOpeningHoursService.GetIsVendorOpenByVendorId(Id);
            
            string value="Closed";

                if(IsOpen==true){
                    value="Open";
                }
                       
             return Json(new { data = value }, JsonRequestBehavior.AllowGet);
         }

        public ActionResult GetVendorSideBar()
        {
            return PartialView("_VendorCompanySidebar");
        }

        public ActionResult GetVendorCompanyProfile(int VendorId)
        {
            Vendor Vendor = _VendorService.GetVendorHomePage(VendorId);
            VendorReviewListingModel VendorReviewListingModel = GetVendorReviewUtility(Vendor.Id, 1);
            ViewBag.VendorId = VendorId;
            return View("VendorCompanyProfile", Vendor);
        }

        public ActionResult VendorCompanyProduct(int VendorId)
        {
            ViewBag.VendorId = VendorId;
            IEnumerable<ProductVendor> ProductVendor = GetProductsByCategoryListUtility(VendorId,0, 1);
            return View("VendorCompanyProduct", ProductVendor);
        }

        public ActionResult VendorCompanyCupon(int VendorId, int PageNumber=1)
        {
            ViewBag.VendorId = VendorId; 
            VendorCampaignListingModel Campaign = GetVendorCampaignsUtility(VendorId,PageNumber);
            return View("VendorCompanyCupon", Campaign);
        }

        public ActionResult VendorCompanyEvents(int VendorId)
        {
            ViewBag.VendorId = VendorId;
            return View("VendorCompanyEvents");
        }

        public ActionResult VendorCompanyBlog(int VendorId)
        {
            ViewBag.VendorId = VendorId;
            return View("VendorCompanyBlog");
        }

        public ActionResult VendorCompanyBlogItems(int VendorId, int PageNumber = 1, Int16 BlogPostType=1)
        {
            ViewBag.VendorId = VendorId;
            VendorBlogListingModel Vendor = GetBlogListingUtility(VendorId, PageNumber, BlogPostType);

            if (BlogPostType == 1) {
                return PartialView("_VendorCompanyBlogItem", Vendor.Blogs);
            }
            else
            {
                return PartialView("_VendorCompanyEventItem", Vendor.Blogs);
            }
           
        }

        public ActionResult BlogListingGetMore(int VendorId, int PageNumber = 1, Int16 BlogPostType=1)
        {
            ViewBag.VendorId = VendorId;
            VendorBlogListingModel Vendor = GetBlogListingUtility(VendorId, PageNumber,BlogPostType);

            if (Vendor.Blogs.Count <= 0)
            {
                return Json(new { data = "nothing" }, JsonRequestBehavior.AllowGet);
            }

            if (BlogPostType == 1)
            {
                return PartialView("_VendorCompanyBlogItem", Vendor.Blogs);
            }
            else
            {
                return PartialView("_VendorCompanyEventItem", Vendor.Blogs);
            }  

        }

        public ActionResult VendorCompanyContact(int VendorId)
        {
            ViewBag.VendorId = VendorId;
            Vendor Vendor = _VendorService.GetVendorHomePage(VendorId);
            return View("VendorCompanyContact", Vendor);
        }

     
        public ActionResult AddCustomerBookmark(int Id)
        {
                Customer Customer = _CustomerService.GetByCookie();

                bool isBookmarked = _CustomerBookmarkService.GetIsBookmarkedByCustIdVendorId(Customer.Id, Id);

                Customer Cust = _CustomerService.GetByCustId(Customer.Id);

                CustomerBookmark CustomerBookmark = _CustomerBookmarkService.GetByCustIdVendorId(Customer.Id, Id);

                if (isBookmarked == false || CustomerBookmark==null)
                {
                    CustomerBookmark = new CustomerBookmark();
                    CustomerBookmark.CustomerId=Cust.Id;
                    CustomerBookmark.VendorId=Id;
                     CustomerBookmark.Id = 0;
                    _CustomerBookmarkService.Create(CustomerBookmark);
                }
                else
                {
                    _CustomerBookmarkService.Delete(CustomerBookmark);
                }
           
            return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VendorRatingBox(int Rating)
        {
            VendorRatingModel VendorRatingModel = new VendorRatingModel(Rating);
            return PartialView("_VendorRatingBox", VendorRatingModel);
        }

        public ActionResult VendorCategory()
        {
            return View();
        }
        
        //Gets all vendor reviews/rating and avg rating 
        [OutputCache(Duration = 60, VaryByParam = "VendorId;PageNumber")]        
        public ActionResult GetVendorReviews(int VendorId,int PageNumber = 1)
        {
            VendorReviewListingModel VendorReviewListingModel = GetVendorReviewUtility(VendorId, PageNumber);
            return PartialView("_VendorProfileRatingReviews", VendorReviewListingModel); 
        }

        //Gets all vendors by category PAGING 
        [OutputCache(Duration = 60, VaryByParam = "SearchFor;PageNumber;CategoryId")]
        public ActionResult GetVendorByCategoryHomePage(int CategoryId = 1, string SearchFor = "", int PageNumber = 1, float Lat = 0, float Lgt = 0)
        {
            Customer Customer = _CustomerService.GetByCookie();
            SearchListingModel SearchListingModel;

            if (CategoryId == 0)
            {
                SearchListingModel = GetVendorSearchListingModel(CategoryId, SearchFor, PageNumber, Lat, Lgt, Customer.Id);
                return View("HomePageListing", SearchListingModel);
               // CategoryId = 30569;
            }

            SearchListingModel = GetVendorSearchListingModel(CategoryId, SearchFor, PageNumber, Lat, Lgt, Customer.Id);
            return View("HomePageListing", SearchListingModel);
        }

        public ActionResult VendorDetails(int VenodrId)
        {
            return View();
        }

        public ActionResult GetVendorAddress()
        {
            List<Vendor> Vendors = _VendorService.GetVendorsForAddress();
          
            if (Vendors.Count() == 0)
            {
                db.Database.ExecuteSqlCommand("exec upLatLng");
            }

            return View("GetVendorAddress",Vendors);
        }

        [HttpPost]
        public string SetVendorAddress(int Id, string Address,double? lat,double? longt )
        {
            VendorAddress VAdd = _VendorAddressService.GetById (Id);
            VAdd.Latitude = lat;
            VAdd.Longitude = longt;
            _VendorAddressService.Update(VAdd);
            return "";
        }

        [OutputCache(Duration = 60, VaryByParam = "none")]
        public ActionResult MainHeaderVenodr()
        {
            return PartialView("_MainHeaderVendor", "Home");
        }

        [HttpPost]
        public ActionResult GetVendorPhones(int Id)
        {
            Vendor Vendor = _VendorService.GetVendorHomePage(Id);

            return Json(new { PhoneNumbre = Vendor.Addresses.FirstOrDefault().PhoneNumber,
                              FaxNumber = Vendor.Addresses.FirstOrDefault().FaxNumber,
                              MobileNumber = Vendor.Addresses.FirstOrDefault().MobileNumber
            },
                              JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region mobile
        /// <summary>
        /// /mobile test, return top 20 business from category
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
         [OutputCache(Duration = 60, VaryByParam = "VendorId")]
        public JsonResult MApp_GetVendorCampaign(string AppSecret, int VendorId ,int PageNumber=1)
        {
            if (AppSecret != "MiAmor")
                return null;
           VendorCampaignListingModel VendorCampaignListingModel = GetVendorCampaignsUtility(VendorId,PageNumber);

            return Json(new { data = VendorCampaignListingModel }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// /mobile test, return top 20 business from category
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        [OutputCache(Duration = 60, VaryByParam = "SearchFor;PageNumber;CategoryId")]
        public JsonResult MApp_GetVendorsByCategoryID(string AppSecret, int CategoryId = 0, string SearchFor = "", int PageNumber = 1, float Lat = 0, float Lgt = 0,string CustId="")
        {
               Customer Customer =new Customer();
            if (CustId != "")
            {
                Customer = _CustomerService.GetByCustStringId(CustId);                
            }
            else
            {
                Customer.Id = 0;
            }
            if (AppSecret != "MiAmor")
                return null;
            VendorSearchLisitingModel VendorSearchLisitingModel = GetVendorByCategoryHomePageUtility(CategoryId, SearchFor, PageNumber, Lat, Lgt, Customer.Id);

            return Json(new { data = VendorSearchLisitingModel }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// /mobile test, return vendor details 
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        [OutputCache(Duration = 60, VaryByParam = "VendorId")]
        public ActionResult MApp_GetVendorByID(string AppSecret, int VendorId, int PageNumber = 1)
        {
            if (AppSecret != "MiAmor")
                return null;
            Vendor Vendor = _VendorService.GetBoxDetailsById(VendorId);
            VendorBoxModelMobile VendorBox =
                Mapper.Map<Vendor, VendorBoxModelMobile>(Vendor);
            string json = JsonConvert.SerializeObject(VendorBox, Formatting.Indented,
                             new JsonSerializerSettings
                             {
                                 ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                             });

            return Content(json, "application/json");
        }
       
        [OutputCache(Duration = 60, VaryByParam = "VendorId")]
        public JsonResult MApp_GetVendorProducts(string AppSecret, int VendorId, int CategoryId = 1)
        {
            if (AppSecret != "MiAmor")
                return null;
            IEnumerable<ProductVendor> ProductVendor = GetProductsByCategoryListUtility(0, 1);
            return Json(new { data = ProductVendor }, JsonRequestBehavior.AllowGet);
        }
       

        [HttpGet]
        [OutputCache(Duration = 60, VaryByParam = "VendorId")]
        public JsonResult MApp_GetVendorReviews(string AppSecret, int VendorId,int PageNumber = 1)
        {
            if (AppSecret != "MiAmor")
                return null;
            VendorReviewListingModel VendorReviewListingModel = GetVendorReviewUtility(VendorId, PageNumber);
            return Json(new { data = VendorReviewListingModel }, JsonRequestBehavior.AllowGet);
        }
#endregion
        #region utilities

        private VendorBlogListingModel GetBlogListingUtility(int VendorId, int PageNumber = 1,Int16 BlogPostType=1)
        {

                try
                {
                    PagedDataRequest PagedDataRequest = new PagedDataRequest(PageNumber, "", "", 6);

                    PagedDataResponse<VendorBlogPost> EventReviewPagedDataResponse = _VendorBlogPostService.GetVendorBlogPostByVendorId(PagedDataRequest, VendorId, BlogPostType);

                    VendorBlogPagingModel VendorBlogPagingModel = new VendorBlogPagingModel(PageNumber, PagedDataRequest.RowsPerPage, EventReviewPagedDataResponse.total);

                    VendorBlogListingModel BlogListingModel = new VendorBlogListingModel();
                    BlogListingModel.VendorBlogPagingModel = VendorBlogPagingModel;
                    BlogListingModel.Blogs = EventReviewPagedDataResponse.rows;
                    return BlogListingModel;
                }
                catch (Exception e)
                {
                }
                return null;
         
        }

        private VendorCampaignListingModel GetVendorCampaignsUtility(int VendorId, int PageNumber = 1)
        {
            try
            {
                PagedDataRequest PagedDataRequest = new PagedDataRequest(PageNumber, "", "");

                PagedDataResponse<Campaign> VendorReviewPagedDataResponse = _CampaignService.GetCampaignsByVendorId(PagedDataRequest, VendorId);

                VendorCampaignPagingModel VendorCampaignPagingModel = new VendorCampaignPagingModel(PageNumber, PagedDataRequest.RowsPerPage, VendorReviewPagedDataResponse.total, VendorId);

                VendorCampaignListingModel VendorCampaignListingModel = new VendorCampaignListingModel();
                VendorCampaignListingModel.VendorCampaignPagingModel = VendorCampaignPagingModel;
                VendorCampaignListingModel.Campaigns =
                Mapper.Map<List<Campaign>, List<CampaignModel>>(VendorReviewPagedDataResponse.rows);
                return VendorCampaignListingModel;
            }
            catch (Exception e)
            {
                int i = 0;
            }
            return null;
        }

        //private SearchListingModel GetAllSearchModelUtility(int CategoryId = 1, string SearchFor = "", int PageNumber = 1, float Lat = 0, float Lgt = 0, int CustId = 0)
        //{
        //    try
        //    {
        //        PagedDataRequest PagedDataRequest = new PagedDataRequest(PageNumber, "", "");

        //        PagedDataResponse<Campaign> VendorCampaingPagedDataResponse = _CampaignService.GetAllCampaigns(PagedDataRequest);
        //        PagedDataResponse<Vendor> VendorPagedDataResponse;
                
        //        if (Lat != 0 && Lgt != 0)
        //        {
        //            VendorPagedDataResponse = _VendorService.GetVendorsPagingByCategoryAndGeo(PagedDataRequest, CategoryId, Lat, Lgt);
        //        }
        //        else
        //        {
        //            VendorPagedDataResponse = _VendorService.GetVendorsPagingByCategory(PagedDataRequest, CategoryId);
        //        }

        //        SearchPagingModel SearchPagingModel = new SearchPagingModel(PageNumber, PagedDataRequest.RowsPerPage, VendorCampaingPagedDataResponse.total);

        //        PageList PageList = new PageList(PageNumber, PagedDataRequest.RowsPerPage, VendorCampaingPagedDataResponse.total, CategoryId);

        //        SearchListingModel SearchListingModel = new SearchListingModel();
        //        SearchListingModel.SearchPagingModel = SearchPagingModel;
        //        SearchListingModel.PageList = PageList;

        //        foreach (Campaign v in VendorCampaingPagedDataResponse.rows)
        //        {
        //            SearchModel sm=new SearchModel();
        //            sm.Id = v.Id;
                   
        //            sm.Description=v.FullDescription;
        //            sm.ShortDescription=v.ShortDescription;
        //            sm.Title=v.Name;
        //            sm.Pictures = new List<Picture>();
        //            if (v.CampaignPictures != null && v.CampaignPictures.Count>0)
        //            {
        //                sm.Pictures.Add(v.CampaignPictures.FirstOrDefault().Picture);
        //            }                    
        //            sm.LinkToPage="";
        //            SearchListingModel.SearchModels = new List<SearchModel>();
        //            SearchListingModel.SearchModels.Add(sm);
        //        }

        //        foreach (Vendor v in VendorPagedDataResponse.rows)
        //        {
        //            SearchModel sm = new SearchModel();
        //            sm.Id = v.Id;
        //            sm.SubTitle = v.Addresses.FirstOrDefault().Cities + " " + v.Addresses.FirstOrDefault().Neighbourhood + " " + v.Addresses.FirstOrDefault().Address1;
        //            sm.Description = v.FullDescription;
        //            sm.ShortDescription = v.ShortDescription;
        //            sm.Title = v.Name;
        //            sm.Pictures = new List<Picture>();
        //            sm.Pictures.Add(v.VendorPictures.FirstOrDefault().Picture);
        //            sm.LinkToPage = "";
        //            SearchListingModel.SearchModels = new List<SearchModel>();
        //            SearchListingModel.SearchModels.Add(sm);
        //        }

        //        return SearchListingModel;
        //    }
        //    catch (Exception e)
        //    {
        //        int i = 0;
        //    }
        //    return null;
        //}

        //TODO Create breadCrumbs list to create links
        private List<VendorCategoryBreadcrumbsModel> GetBreadCrumbsByCategoryId(int CategoryId,int CountCategory)
        {
            string key = string.Format(VENDORVREADCRUMBS_BY_ID_KEY, CategoryId);
            return _CacheManager.Get(key, () =>
            {
                List<VendorCategoryBreadcrumbsModel> ListCategoryBreadcrumbs = new List<VendorCategoryBreadcrumbsModel>();


                VendorCategory VendorCategory = _VendorCategoryService.GetById(CategoryId);
                while (VendorCategory != null)
                {
                    VendorCategoryBreadcrumbsModel VendorCategoryBreadcrumbs = new VendorCategoryBreadcrumbsModel();
                    if (VendorCategory.Id == CategoryId)
                    {
                        VendorCategoryBreadcrumbs.Count = CountCategory;
                    }
                    VendorCategoryBreadcrumbs.Name = VendorCategory.Name;
                    VendorCategoryBreadcrumbs.Id = VendorCategory.Id;
                    VendorCategoryBreadcrumbs.ParentId = VendorCategory.ParentCategoryId;
                    ListCategoryBreadcrumbs.Add(VendorCategoryBreadcrumbs);
                    VendorCategory = _VendorCategoryService.GetById(Convert.ToInt32(VendorCategory.ParentCategoryId));
                }
                return ListCategoryBreadcrumbs;
            });
        }

        private VendorReviewListingModel GetVendorReviewUtility(int VendorId, int PageNumber = 1)
        {
            try
            {
             PagedDataRequest PagedDataRequest = new PagedDataRequest(PageNumber, "", "");
           
                 PagedDataResponse<VendorReview> VendorReviewPagedDataResponse = _VendorReviewService.GetVendorsReviewPagingByVenodrId(PagedDataRequest, VendorId);
            
             VendorReviewPagingModel VendorReviewPagingModel = new VendorReviewPagingModel(PageNumber, PagedDataRequest.RowsPerPage, VendorReviewPagedDataResponse.total, VendorId);
             
            VendorReviewListingModel VendorReviewListingModel = new VendorReviewListingModel();
             VendorReviewListingModel.VendorReviewPagingModel = VendorReviewPagingModel;
             VendorReviewListingModel.AvgRating = _VendorReviewService.GetAvgReview(VendorId);
             VendorReviewListingModel.VendorReviewBox=
             Mapper.Map<List<VendorReview>, List<VendorReviewModel>>(VendorReviewPagedDataResponse.rows);
             return VendorReviewListingModel;
            }
            catch (Exception e)
            {
                int i = 0;
            }
            return null;
        }
        //TODO Create sort list to create links in dropDown 
        private List<SelectListItem> GetSortOptions(int CategoryId)
        {
            List<SelectListItem> li = new List<SelectListItem>();
            return li;
        }

        public IEnumerable<ProductVendor> GetProductsByCategoryListUtility(int VendorId,int CategoryId = 1, int PageNumber = 1)
        {
            if (CategoryId == 0)
            {
                CategoryId = 30569;
            }

            PagedDataRequest PagedDataRequest = new PagedDataRequest(PageNumber, "", "");
           
            PagedDataResponse<ProductVendor> VendorPagedDataResponse;
            VendorPagedDataResponse = _VendorProductService.GetProductsByCategory(PagedDataRequest, CategoryId, VendorId);
            //VendorPagedDataResponse.total = _VendorCategoryCountService.GetByCategoryId(CategoryId).CountVendors;

            List<ProductVendor> product = new List<ProductVendor>();
            foreach (ProductVendor row in VendorPagedDataResponse.rows)
            {
                ProductVendor ProductModel = new ProductVendor();
                ProductModel.ApprovedRatingSum = row.ApprovedRatingSum;
                ProductModel.ApprovedTotalReviews = row.ApprovedTotalReviews;
                ProductModel.FullDescription = row.FullDescription;
                ProductModel.Id = row.Id;
                ProductModel.MetaKeywords = row.MetaKeywords;
                ProductModel.Name = row.Name;
                ProductModel.NotApprovedRatingSum = row.NotApprovedRatingSum;
                ProductModel.NotApprovedTotalReviews = row.NotApprovedTotalReviews;
                ProductModel.ShortDescription = row.ShortDescription;

               // ProductModel.ProductCategories = new List<ProductCategory>();

                //foreach (ProductCategory PC in row.ProductCategories)
                //{
                //    ProductCategory ProductCategory = new ProductCategory();
                //    ProductCategory.CategoryTemplateId = PC.Category;
                //    ProductCategory.CategoryId = PC.CategoryId;
                //    ProductCategory.Id = PC.Id;
                //    ProductCategory.ProductId = PC.ProductId;
                //    ProductModel.ProductCategories.Add(ProductCategory);
                //}

                product.Add(ProductModel);
                
            }
            //PageList PageList = new PageList(PageNumber, PagedDataRequest.RowsPerPage, VendorPagedDataResponse.total, CategoryId);

            //VendorSearchLisitingModel.VendorBox = VendorBox;
            //VendorSearchLisitingModel.VendorCategoryBreadcrumbs = GetBreadCrumbsByCategoryId(CategoryId, PageList.TotalCount);
            //VendorSearchLisitingModel.AvailableSortOptions = GetSortOptions(CategoryId);
            //VendorSearchLisitingModel.PageList = PageList;

            return product.AsEnumerable();
        }

        public SearchListingModel GetVendorSearchListingModel(int CategoryId = 1, string SearchFor = "", int PageNumber = 1, float Lat = 0, float Lgt = 0, int CustId = 0)
        {
            SearchListingModel SearchListingModel = new SearchListingModel();

            SearchListingModel.VendorSearchLisitingModel = GetVendorByCategoryHomePageUtility(CategoryId, SearchFor, PageNumber, Lat, Lgt, CustId);
            SearchListingModel.VendorCampaignListingModel = VendorCampaignListingModelUtility(CategoryId,PageNumber);
            SearchListingModel.VendorBlogListingModel = GetVendorBlogListingModelUtility(CategoryId,PageNumber, 1);
           // SearchListingModel.VendorEventListingModel = GetVendorEventListingModelUtility(PageNumber);

            return SearchListingModel;
        }

        public VendorSearchLisitingModel GetVendorByCategoryHomePageUtility(int CategoryId = 1, string SearchFor = "", int PageNumber = 1, float Lat = 0, float Lgt = 0,int CustId=0)
        {
           
            PagedDataRequest PagedDataRequest = new PagedDataRequest(PageNumber, SearchFor, "");

            VendorSearchLisitingModel VendorSearchLisitingModel = new VendorSearchLisitingModel();
            PagedDataResponse<Vendor> VendorPagedDataResponse;
            if(Lat!=0 && Lgt!=0)
            {
                VendorPagedDataResponse = _VendorService.GetVendorsPagingByCategoryAndGeo(PagedDataRequest, CategoryId,Lat,Lgt);
            }
            else{
              VendorPagedDataResponse = _VendorService.GetVendorsPagingByCategory(PagedDataRequest, CategoryId);
            }
            VendorPagedDataResponse.total = _VendorCategoryCountService.GetByCategoryId(CategoryId).CountVendors;
            //List<VendorBoxModel> VendorBox =
            //    Mapper.Map<List<Vendor>, List<VendorBoxModel>>(VendorPagedDataResponse.rows);

            List<VendorBoxModel> VendorBox = new List<VendorBoxModel>();
            foreach (Vendor row in VendorPagedDataResponse.rows)
            {
                VendorBoxModel VendorBoxModel = new VendorBoxModel();
                VendorBoxModel.Addresses = row.Addresses;
                VendorBoxModel.ApprovedRatingSum = row.ApprovedRatingSum;
                VendorBoxModel.ApprovedTotalReviews = row.ApprovedTotalReviews;
                VendorBoxModel.FullDescription = row.FullDescription;
                VendorBoxModel.HasBranches = row.HasBranches;
                VendorBoxModel.Id = row.Id;
                VendorBoxModel.LogoImgPath = row.LogoImgPath;
                VendorBoxModel.MetaKeywords = row.MetaKeywords;
                VendorBoxModel.Name = row.Name;
                VendorBoxModel.NotApprovedRatingSum = row.NotApprovedRatingSum;
                VendorBoxModel.NotApprovedTotalReviews = row.NotApprovedTotalReviews;
                VendorBoxModel.ShortDescription = row.ShortDescription;
                VendorBoxModel.SiteUrl = row.SiteUrl;
                VendorBoxModel.CouponNum = row.Campaigns.Count();

                VendorBoxModel.IsBookmarkedByCurrCustomer = _CustomerBookmarkService.GetIsBookmarkedByCustIdVendorId(CustId, row.Id);
                                
                VendorBoxModel.VendorCategories = new List<VendorCategoryBreadcrumbsModel>();
                foreach (VendorCategory VC in row.VendorCategoryVendors.Select(vc => vc.VendorCategory))
                {
                 VendorCategoryBreadcrumbsModel VendorCategoryBreadcrumbsModel = new VendorCategoryBreadcrumbsModel();
                    VendorCategoryBreadcrumbsModel.Name=VC.Name;
                    VendorCategoryBreadcrumbsModel.Id=VC.Id;
                    VendorCategoryBreadcrumbsModel.ParentId=VC.ParentCategoryId;
                    VendorBoxModel.VendorCategories.Add(VendorCategoryBreadcrumbsModel);    
                }
                VendorBox.Add(VendorBoxModel);
                
            
            }
            PageList PageList = new PageList(PageNumber, PagedDataRequest.RowsPerPage, VendorPagedDataResponse.total, CategoryId);

            VendorSearchLisitingModel.VendorBox = VendorBox;
            VendorSearchLisitingModel.VendorCategoryBreadcrumbs = GetBreadCrumbsByCategoryId(CategoryId, PageList.TotalCount);
            VendorSearchLisitingModel.AvailableSortOptions = GetSortOptions(CategoryId);
            VendorSearchLisitingModel.PageList = PageList;

            //return PartialView(_VendortListing, VendorSearchLisitingModel);
            return  VendorSearchLisitingModel;
        }

        private VendorCampaignListingModel VendorCampaignListingModelUtility(int CategoryId = 1,int PageNumber = 1)
        {
               try
                {
                    PagedDataRequest PagedDataRequest = new PagedDataRequest(PageNumber, "", "", 6);

                    PagedDataResponse<Campaign> EventReviewPagedDataResponse = _CampaignService.GetAllCampaigns(PagedDataRequest);

                    VendorCampaignPagingModel VendorCampaignPagingModel = new VendorCampaignPagingModel(PageNumber, PagedDataRequest.RowsPerPage, EventReviewPagedDataResponse.total);

                    PageList PageList = new PageList(PageNumber, PagedDataRequest.RowsPerPage, EventReviewPagedDataResponse.total, CategoryId);

                    VendorCampaignListingModel VendorCampaignListingModel = new VendorCampaignListingModel();
                    VendorCampaignListingModel.VendorCampaignPagingModel = VendorCampaignPagingModel;
                    VendorCampaignListingModel.Campaigns = Mapper.Map<List<Campaign>, List<CampaignModel>>(EventReviewPagedDataResponse.rows);
                    VendorCampaignListingModel.PageList = PageList;
                   return VendorCampaignListingModel;
                }
                catch (Exception e)
                {
                }
                return null;
        }

        private VendorBlogListingModel GetVendorBlogListingModelUtility(int CategoryId = 1,int PageNumber = 1, Int16 BlogPostType = 1)
        {

            try
            {
                PagedDataRequest PagedDataRequest = new PagedDataRequest(PageNumber, "", "", 6);

                PagedDataResponse<VendorBlogPost> EventReviewPagedDataResponse = _VendorBlogPostService.GetAllVendorBlogPost(PagedDataRequest, BlogPostType);

                VendorBlogPagingModel VendorBlogPagingModel = new VendorBlogPagingModel(PageNumber, PagedDataRequest.RowsPerPage, EventReviewPagedDataResponse.total);

                PageList PageList = new PageList(PageNumber, PagedDataRequest.RowsPerPage, EventReviewPagedDataResponse.total, CategoryId);

                VendorBlogListingModel BlogListingModel = new VendorBlogListingModel();
                BlogListingModel.VendorBlogPagingModel = VendorBlogPagingModel;
                BlogListingModel.Blogs = EventReviewPagedDataResponse.rows;
                BlogListingModel.PageList = PageList;
                return BlogListingModel;

            }
            catch (Exception e)
            {
            }
            return null;

        }

        #endregion

    }
}