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
    public class CampaignDeliveryAdminController : Controller
    {
        #region fields
        private ICampaignDeliveryService _CampaignDeliveryService;
        #endregion

         #region ctr
        public CampaignDeliveryAdminController(ICampaignDeliveryService ICampaignDeliveryService)
        {
            _CampaignDeliveryService = ICampaignDeliveryService;
        }
        #endregion

        #region actions
        // GET: Admin/Customer
        [HttpPost]
        public ActionResult GetFilteredCampaignDeliveries(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            PagedDataResponse<CampaignDelivery> PagedDataResponse = _CampaignDeliveryService.GetFilteredCampaignDeliveries(FilterElements, PageOptions);
            string json = JsonConvert.SerializeObject(PagedDataResponse, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        public ActionResult GetCampaignDeliveryById(int Id)
        {
            CampaignDelivery CampaignDelivery = _CampaignDeliveryService.GetById(Id);
            CampaignDeliveryCrudModel NewCampaignDelivery =
                Mapper.Map<CampaignDelivery, CampaignDeliveryCrudModel>(CampaignDelivery);
            string json = JsonConvert.SerializeObject(NewCampaignDelivery, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult InsertNewCampaignDelivery(CampaignDeliveryCrudModel NewCampaignDelivery)
        {
            CampaignDelivery CampaignDelivery =
                Mapper.Map<CampaignDeliveryCrudModel, CampaignDelivery>(NewCampaignDelivery);
            _CampaignDeliveryService.Create(CampaignDelivery);
            string json = JsonConvert.SerializeObject(CampaignDelivery, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        public ActionResult SaveChanges(CampaignDeliveryCrudModel CampaignDeliveryEdited)
        {
            CampaignDelivery CampaignDelivery =
                Mapper.Map<CampaignDeliveryCrudModel, CampaignDelivery>(CampaignDeliveryEdited);
            _CampaignDeliveryService.Update(CampaignDelivery);
            string json = JsonConvert.SerializeObject(CampaignDeliveryEdited, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult EditCampaignDelivery(int Id, string ColumnChanged, string Value)
        {
            CampaignDelivery UpCampaignDelivery = _CampaignDeliveryService.GetById(Id);
            string ColChangedType = UpCampaignDelivery.GetType().GetProperty(ColumnChanged).PropertyType.Name;
            object ChangedVal = EfFilterHelper.GetValueByColType(Value, ColChangedType);
            UpCampaignDelivery.GetType().GetProperty(ColumnChanged).SetValue(UpCampaignDelivery, ChangedVal);
            _CampaignDeliveryService.Update(UpCampaignDelivery);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCampaignDelivery(int Id)
        {
            CampaignDelivery CampaignDelivery = _CampaignDeliveryService.GetById(Id);
            _CampaignDeliveryService.Delete(CampaignDelivery);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}