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
    public class CampaignAttributeAdminController : Controller
    {
        #region fields
        private ICampaignAttributeService _CampaignAttributeService;
        #endregion

         #region ctr
        public CampaignAttributeAdminController(ICampaignAttributeService ICampaignAttributeService)
        {
            _CampaignAttributeService = ICampaignAttributeService;
        }
        #endregion

        #region actions
        // GET: Admin/Customer
        [HttpPost]
        public ActionResult GetFilteredCampaignAttributes(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            PagedDataResponse<CampaignAttribute> PagedDataResponse = _CampaignAttributeService.GetFilteredCampaignAttributes(FilterElements, PageOptions);
            string json = JsonConvert.SerializeObject(PagedDataResponse, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        public ActionResult GetCampaignAttributeById(int Id)
        {
            CampaignAttribute CampaignAttribute = _CampaignAttributeService.GetById(Id);
            CampaignAttributeCrudModel NewCampaignAttribute =
                Mapper.Map<CampaignAttribute, CampaignAttributeCrudModel>(CampaignAttribute);
            string json = JsonConvert.SerializeObject(NewCampaignAttribute, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult InsertNewCampaignAttribute(CampaignAttributeCrudModel NewCampaignAttribute)
        {
            CampaignAttribute CampaignAttribute =
                Mapper.Map<CampaignAttributeCrudModel, CampaignAttribute>(NewCampaignAttribute);
            _CampaignAttributeService.Create(CampaignAttribute);
            string json = JsonConvert.SerializeObject(CampaignAttribute, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        public ActionResult SaveChanges(CampaignAttributeCrudModel CampaignAttributeEdited)
        {
            CampaignAttribute CampaignAttribute =
                Mapper.Map<CampaignAttributeCrudModel, CampaignAttribute>(CampaignAttributeEdited);
            _CampaignAttributeService.Update(CampaignAttribute);
            string json = JsonConvert.SerializeObject(CampaignAttributeEdited, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult EditCampaignAttribute(int Id, string ColumnChanged, string Value)
        {
            CampaignAttribute UpCampaignAttribute = _CampaignAttributeService.GetById(Id);
            string ColChangedType = UpCampaignAttribute.GetType().GetProperty(ColumnChanged).PropertyType.Name;
            object ChangedVal = EfFilterHelper.GetValueByColType(Value, ColChangedType);
            UpCampaignAttribute.GetType().GetProperty(ColumnChanged).SetValue(UpCampaignAttribute, ChangedVal);
            _CampaignAttributeService.Update(UpCampaignAttribute);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCampaignAttribute(int Id)
        {
            CampaignAttribute CampaignAttribute = _CampaignAttributeService.GetById(Id);
            _CampaignAttributeService.Delete(CampaignAttribute);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}