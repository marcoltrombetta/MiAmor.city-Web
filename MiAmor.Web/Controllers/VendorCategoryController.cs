using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using System.Data.Entity;
using System.Dynamic;

using MiAmor.Services;
using MiAmor.Data;
using MiAmor.Core;
using MiAmor.Web.Models;


namespace MiAmor.Web.Controllers
{
    public class VendorCategoryController : Controller
    {
        // GET: VendorCategory
        #region fields
        IVendorCategoryService _VendorCategoryService;
        IVendorCategoryCountService _VendorCategoryCountService;
        IVendorService _VendorService;
        #endregion

        #region ctr
        public VendorCategoryController(IVendorCategoryService VendorCategoryService, IVendorService VendorService, IVendorCategoryCountService VendorCategoryCountService)
        {
            _VendorCategoryService = VendorCategoryService;
            _VendorService = VendorService;
            _VendorCategoryCountService = VendorCategoryCountService;
       
        }

        #endregion

        #region Actions
        
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Gets All Categories for navigation 
        /// </summary>        
        /// <returns>Categories</returns>
        /// 
        [OutputCache(Duration = 60, VaryByParam = "CategoryId")]
        public ActionResult Navigation(int CategoryId=0,int PageId=0)
        {
            if (CategoryId == null)
            {
                CategoryId = 0;
            }
            List<VendorCategory> Categories = _VendorCategoryService.GetParentNavigationVendors(CategoryId,PageId);
            return PartialView("_Navigation",Categories);
        }

        /// <summary>
        /// Gets All Categories for navigation 
        /// </summary>        
        /// <returns>Categories</returns>
        /// 
        [OutputCache(Duration = 60, VaryByParam = "CategoryId")]
        public JsonResult MApp_GetCategoriesByID(string AppSecret,int CategoryId = 0, int PageId = 0)
        {
            if (AppSecret != "MiAmor")
                return null;
            if (CategoryId == null)
            {
                CategoryId = 0;
            }
            List<VendorCategory> Categories = _VendorCategoryService.GetParentNavigationVendors(CategoryId, PageId);
            return Json(Categories, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets All Categories for navigation 
        /// </summary>        
        /// <returns>Categories</returns>
        /// 
        [OutputCache(Duration = 60, VaryByParam = "CategoryId;PageId")]
        public JsonResult MApp_GetSubCategoriesByID(int CategoryId = 0,int PageId=0)
        {
            if (CategoryId == null)
            {
                CategoryId = 0;
            }
            List<VendorCategory> Categories = _VendorCategoryService.GetParentNavigationVendors(CategoryId, PageId);
            return Json(Categories, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Gets All Categories for footer navigation 
        /// </summary>        
        /// <returns>Categories</returns>
        /// 
        [OutputCache(Duration = 60, VaryByParam = "none")]
        public ActionResult FooterNavigation(int ParentId)
        {
            List<VendorCategoryCount> Query = _VendorCategoryCountService.GeAllParentCategoryCount();
            return PartialView("_FooterNavigation", Query);
        }

        #endregion

        #region utilities
        //TODO Create breadCrumbs list to create links
         private List<VendorCategoryBreadcrumbsModel> GetBreadCrumbsByCategoryId(int CategoryId)
         {
             List<VendorCategoryBreadcrumbsModel> br = new List<VendorCategoryBreadcrumbsModel>();
             return br;
         }
        //TODO Create sort list to create links in dropDown
         private List<SelectListItem> GetSortOptions(int CategoryId)
         {
             List<SelectListItem> li = new List<SelectListItem>();
             return li;
         }
     
         //private List<VendorCategoryNavigationModel> PrepareVendorsCategoryNavigation(List<VendorCategoryCountHelper> Query)
         //{
         //    List<VendorCategoryNavigationModel> ListVendorCategoryNavigation = new List<VendorCategoryNavigationModel>();
         //    foreach (var Vend in Query)
         //    {
         //        VendorCategory vc = Vend.VendorCategory ;
         //        if (vc.ParentCategoryId == null || vc.ParentCategoryId==0)
         //        {
         //            VendorCategoryNavigationModel VCN = new VendorCategoryNavigationModel();
         //            VCN.Id = vc.Id;
         //            VCN.Name = vc.Name;
         //            VCN.CountVendors = Vend.CountVendors;
         //            VCN.CssClass = vc.CssClass;
         //            VCN.VendorCategoryNavigations = new List<VendorCategoryNavigationModel>();
         //            ListVendorCategoryNavigation.Add(VCN);
         //        }
         //        else
         //        {
         //            VendorCategoryNavigationModel VCN = new VendorCategoryNavigationModel();
         //            VCN.Id = vc.Id;
         //            VCN.Name = vc.Name;
         //            VCN.CountVendors = Vend.CountVendors;
         //            VCN.CssClass = vc.CssClass;
         //            VendorCategoryNavigationModel ParentVCN = ListVendorCategoryNavigation.Find(x => x.Id == vc.ParentCategoryId);
         //            ParentVCN.CountVendors += VCN.CountVendors;
         //            ParentVCN.VendorCategoryNavigations.Add(VCN);

         //        }
         //    }
         //    try
         //    {
         //        foreach (var item in ListVendorCategoryNavigation)
         //        {
         //            VendorCategoryCount vcc = new VendorCategoryCount();
         //            vcc.CountVendors = item.CountVendors;
         //            vcc.VendorCategoryId = item.Id;
         //            vcc.Name = item.Name;
         //            vcc.CssClass = item.CssClass;
         //            _VendorCategoryCountService.Create(vcc);
         //        }
         //    }
         //    catch (Exception ex)
         //    {
         //        int j = 0;
         //    }
         //    return ListVendorCategoryNavigation;
         //}
        #endregion

    }
}