using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI ;
using System.Web.Mvc;
using MiAmor.Services;
using MiAmor.Data;
using MiAmor.Core;
using System.Threading;
using System.Globalization;
using MiAmor.Web.Models;
using AutoMapper;

namespace MiAmor.Web.Controllers
{
    public class HomeController : Controller
    {
         #region fields
        private IVendorCategoryService _VendorCategoryService;
        private INeighbourhoodService _NeighbourhoodService;
        private ISliderService _SliderService;
        #endregion

        #region ctr
        public HomeController(IVendorCategoryService VendorCategoryService, INeighbourhoodService NeighbourhoodService, ISliderService SliderService)
        {
            _VendorCategoryService = VendorCategoryService;
            _NeighbourhoodService = NeighbourhoodService;
            _SliderService = SliderService;
        }

        #endregion
        #region Actions

        [HttpPost]
        public ActionResult Search(HeaderSearchbarModel HeaderSearchbarModel)
        {           
            return View();
        }

        [OutputCache(Duration = 60)]
        public ActionResult ContactUs()
        {           
            return View();
        }

        [OutputCache(Duration = 60,VaryByParam ="PageNumber;CategoryId")]
        public ActionResult Index(int? CategoryId, int? PageNumber)
        {
            if (CategoryId == null)
            {
                CategoryId = 0;                           
            }
            ViewBag.CategoryId = CategoryId;
            if (PageNumber == null || PageNumber==0)
            {
                PageNumber = 1;
            }
         
            ViewBag.PageNumber = PageNumber;

            return View();
        }

        public ActionResult MainHeaderImageSlider() {
            Slider SlideList = _SliderService.GetHomeSlider();
            SliderModel model = Mapper.Map<Slider, SliderModel>(SlideList);

            return PartialView("_MainHeaderImageSlider",model);
        }

        [OutputCache(Duration = 60, VaryByParam = "none")]
        public ActionResult MainHeader()
        {
            return PartialView("_MainHeader");
        }

        public ActionResult MainHeaderMenu()
        {
            return PartialView("_MainHeaderMenu");
        }

        public ActionResult MainHeaderTopBar()
        {
            return PartialView("_MainHeaderTopBar");
        }

        [OutputCache(Duration = 60, VaryByParam = "none")]
        public ActionResult MainFooter()
        {
            return PartialView("_MainFooter");
        }
       
        public ActionResult MainHeaderSearchBar()
        {
            return PartialView("_MainHeaderSearchBar");
        }

        public ActionResult CategorySearchCombo()
        {
            List<VendorCategory> vc = new List<VendorCategory>();

            vc = _VendorCategoryService.GetCategoryByParentId(0);
       
            return PartialView("_CategorySearch",vc);
        }

        public ActionResult LocationSearchBar()
        {
            List<Neighbourhood> vc = new List<Neighbourhood>();

            vc = _NeighbourhoodService.GetByCityId(1);

            return PartialView("_LocationSearchBar", vc);
        }

        public ActionResult MainHeaderVendorSlider()
        {
            return PartialView("_MainHeaderVendorSlider");
        }
        
        //public ActionResult MainHeaderImageSlider()
        //{
        //    return PartialView("_MainHeaderImageSlider");
        //}

        public void InitializeCulture(String selectedLanguage)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(selectedLanguage);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }


        #endregion

        #region utilities
        #endregion
    }
}