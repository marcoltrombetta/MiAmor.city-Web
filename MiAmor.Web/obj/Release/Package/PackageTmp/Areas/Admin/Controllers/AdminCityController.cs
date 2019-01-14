using AutoMapper;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace MiAmor.Web.Areas.Admin.Controllers
{
    public class AdminCityController : Controller
    {
        private ICityService _CityService;
        public AdminCityController(ICityService CityService)
            {               
                _CityService=CityService;
            }

        public ActionResult GetCities()
        {
            IEnumerable<City> Cities;
            Cities = _CityService.GetAll();

            string json = JsonConvert.SerializeObject(Cities, Formatting.Indented,
                                     new JsonSerializerSettings
                                     {
                                         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                     });
            return Content(json, "application/json");
        }

        public ActionResult GetCityAutoComplete(string PartialName)
        {
            List<City> Cities = _CityService.GetCityByPartialName(PartialName, 5);
            List<CityAutocompleteModel> CityAutocompleteModels = new List<CityAutocompleteModel>();
            CityAutocompleteModels =
                Mapper.Map<List<City>, List<CityAutocompleteModel>>(Cities);
            string json = JsonConvert.SerializeObject(CityAutocompleteModels, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

       
    }
}