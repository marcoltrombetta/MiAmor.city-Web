using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiAmor.Services;
using MiAmor.Data;
using MiAmor.Core;

namespace MiAmor.Web.Controllers
{
    public class LanguageController : Controller
    {
        #region fields
        private ILanguageService _LanguageService;
        #endregion

        #region ctr
        public LanguageController(ILanguageService LanguageService)
        {
            _LanguageService = LanguageService;
        }
        #endregion
        #region Actions
        // GET: Language
        public ActionResult Index()
        {
            IEnumerable<Language> Languages = _LanguageService.GetAll();
            return View(Languages);
        }

        [HttpPost]
        public bool  SetLanguage(Language objLanguage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _LanguageService.Update(objLanguage);
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }


        [HttpPost]
        public string GetLengueString(string key) {

            return _LanguageService.GetValue(key, null).ToString();

        }

        #endregion

        #region utilities
        #endregion
    }
}

