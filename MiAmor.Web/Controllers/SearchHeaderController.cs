using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;

namespace MiAmor.Web.Controllers
{
    public class SearchHeaderController : Controller
    {
        #region fields

        #endregion

        #region ctr


        #endregion
        #region Actions
        // GET: SearchHeader
        [OutputCache(CacheProfile ="Test", VaryByParam = "none")]
        public ActionResult Index()
        {
            return View();
        }
        [OutputCache(CacheProfile ="Test", VaryByParam = "none")]
        public PartialViewResult TopSearchBar()
        {
            return PartialView("_TopSearchBar");
        }
        #endregion
        #region utilities
        #endregion
    }
}