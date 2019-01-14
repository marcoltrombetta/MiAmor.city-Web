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
using MiAmor.Web.Area;

namespace MiAmor.Web.Areas.Admin.Controllers
{
    public class CampaignAdminController : Controller
     {
        #region fields
        private ICampaignService _CampaignService;
        #endregion

         #region ctr
        public CampaignAdminController(ICampaignService ICampaignService)
        {
            _CampaignService = ICampaignService;
        }
        #endregion

        #region actions
        // GET: Admin/Customer
        [HttpPost]
        public ActionResult GetFilteredCampaigns(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {   
            PagedDataResponse<Campaign> PagedDataResponse = _CampaignService.GetFilteredCampaigns(FilterElements, PageOptions);
            string json = JsonConvert.SerializeObject(PagedDataResponse, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        public ActionResult GetCampaignById(int Id)
        {
            Campaign Campaign = _CampaignService.GetById(Id);
            CampaignCrudModel NewCampaign =
                Mapper.Map<Campaign, CampaignCrudModel>(Campaign);
            string json = JsonConvert.SerializeObject(NewCampaign, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult InsertNewCampaign(CampaignCrudModel NewCampaign)
        {

            Campaign Campaign =
                Mapper.Map<CampaignCrudModel, Campaign>(NewCampaign);
            Campaign.FinalEndDate = DateTime.Now;
            Campaign.StartDateTime = DateTime.Now;
            Campaign.EndDateTime = DateTime.Now;
            Campaign.ExpirationDateTime = DateTime.Now;
            _CampaignService.Create(Campaign);
            string json = JsonConvert.SerializeObject(Campaign, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }
        [HttpPost]
        public ActionResult SaveChanges(CampaignCrudModel CampaignEdited)
        {
            Campaign Campaign =
                Mapper.Map<CampaignCrudModel, Campaign>(CampaignEdited);
            Campaign.FinalEndDate = DateTime.Now;
            Campaign.StartDateTime = DateTime.Now;
            Campaign.EndDateTime = DateTime.Now;
            Campaign.ExpirationDateTime = DateTime.Now;
            _CampaignService.Update(Campaign);
            string json = JsonConvert.SerializeObject(CampaignEdited, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult EditCampaign(int Id, string ColumnChanged, string Value)
        {
            Campaign UpCampaign = _CampaignService.GetById(Id);
            string ColChangedType = UpCampaign.GetType().GetProperty(ColumnChanged).PropertyType.Name;
            object ChangedVal = EfFilterHelper.GetValueByColType(Value, ColChangedType);
            UpCampaign.GetType().GetProperty(ColumnChanged).SetValue(UpCampaign, ChangedVal);
            _CampaignService.Update(UpCampaign);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteCampaign(int Id)
        {
            Campaign Campaign = _CampaignService.GetById(Id);
            _CampaignService.Delete(Campaign);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCampaignAutoComplete(string PartialName)
        {
            List<Campaign> Campaigns = _CampaignService.GetCampaignByPartialName(PartialName, 5);
            List<CampaignAutocompleteModel> CampaignAutocompleteModels = new List<CampaignAutocompleteModel>();
            CampaignAutocompleteModels =
                Mapper.Map<List<Campaign>, List<CampaignAutocompleteModel>>(Campaigns);
            string json = JsonConvert.SerializeObject(CampaignAutocompleteModels, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        #endregion
    }
}