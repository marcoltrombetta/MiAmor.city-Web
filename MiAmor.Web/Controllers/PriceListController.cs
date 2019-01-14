using MiAmor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiAmor.Core;

namespace MiAmor.Web.Controllers
{
    public class PriceListController : Controller
    {

          #region fields

        private IPriceListService _PriceListService;
      

        #endregion

        #region ctr
        public PriceListController(IPriceListService PriceListService)
        {
            _PriceListService = PriceListService;
        }

        #endregion

        // GET: PriceList
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPriceLists()
        {
            List<PriceList> pl=_PriceListService.GetAll().ToList();
           return PartialView("_PriceList", pl);
        }

        public ActionResult GetPriceListPrices(int Id)
        {
            PriceList pl = _PriceListService.GetById(Id);
            return View("PriceListPrices", pl);
        }
    }
}