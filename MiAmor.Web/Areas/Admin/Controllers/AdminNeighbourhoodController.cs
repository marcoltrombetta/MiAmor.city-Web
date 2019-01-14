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
    public class AdminNeighbourhoodController : Controller
    {
        private INeighbourhoodService _NeighbourhoodService;
        public AdminNeighbourhoodController(INeighbourhoodService NeighbourhoodService)
            {
                _NeighbourhoodService = NeighbourhoodService;
            }

        public ActionResult GetNeighbourhoods()
        {
            IEnumerable<Neighbourhood> Neighbourhoods;
            Neighbourhoods = _NeighbourhoodService.GetAll();

            string json = JsonConvert.SerializeObject(Neighbourhoods, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        public ActionResult GetNeighbourhoodsByCityId(int CityId)
        {
            List<Neighbourhood> Neighbourhoods = _NeighbourhoodService.GetByCityId(CityId);

            string json = JsonConvert.SerializeObject(Neighbourhoods, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        public ActionResult GetNeighbourhoodAutoComplete(string PartialName, int CityId)
        {
            List<Neighbourhood> Neighbourhoods = _NeighbourhoodService.GetNeighbourhoodByPartialName(PartialName, CityId, 5);
            List<NeighbourhoodAutocompleteModel> NeighbourhoodAutocompleteModels = new List<NeighbourhoodAutocompleteModel>();
            NeighbourhoodAutocompleteModels =
                Mapper.Map<List<Neighbourhood>, List<NeighbourhoodAutocompleteModel>>(Neighbourhoods);
            string json = JsonConvert.SerializeObject(NeighbourhoodAutocompleteModels, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

      
    }
}