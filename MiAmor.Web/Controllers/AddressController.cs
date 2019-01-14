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
    public class AddressController : Controller
    {
        // GET: Vendor

        #region fields
        ICityService _CityService;
        INeighbourhoodService _NeighbourhoodService;
        #endregion

        #region ctr
        public AddressController(ICityService CityService, INeighbourhoodService NeighbourhoodService)
        {
            _CityService = CityService;
            _NeighbourhoodService = NeighbourhoodService;
        }

        #endregion

        // GET: Address
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetLocationDetailsSideBar()
        {
            List<City> Cities=_CityService.GetAll().ToList();
            return PartialView("_LocationDetailsSideBar", Cities);
        }

        public ActionResult GetNeighbourhood(int CityId)
        {
            List<Neighbourhood> Neig = _NeighbourhoodService.GetByCityId(CityId);
            return Json(new { data = Neig }, JsonRequestBehavior.AllowGet);
        }

    }
}