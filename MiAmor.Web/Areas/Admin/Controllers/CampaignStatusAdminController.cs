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
    public class CampaignStatusAdminController : Controller
       {
        #region fields
        private ICampaignStatusService _CampaignStatusService;
        #endregion

         #region ctr
        public CampaignStatusAdminController(ICampaignStatusService ICampaignStatusService)
        {
            _CampaignStatusService = ICampaignStatusService;
        }
        #endregion

        #region actions
        // GET: Admin/Customer
        [HttpPost]
        public ActionResult GetFilteredCampaignStatus(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            PagedDataResponse<CampaignStatus> PagedDataResponse = _CampaignStatusService.GetFilteredCampaignStatus(FilterElements, PageOptions);
            string json = JsonConvert.SerializeObject(PagedDataResponse, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        public ActionResult GetCampaignStatusById(int Id)
        {
            CampaignStatus CampaignStatus = _CampaignStatusService.GetById(Id);
            CampaignStatusCrudModel NewCampaignStatus =
                Mapper.Map<CampaignStatus, CampaignStatusCrudModel>(CampaignStatus);
            string json = JsonConvert.SerializeObject(NewCampaignStatus, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult InsertNewCampaignStatus(CampaignStatusCrudModel NewCampaignStatus)
        {
            CampaignStatus CampaignStatus =
                Mapper.Map<CampaignStatusCrudModel, CampaignStatus>(NewCampaignStatus);
            _CampaignStatusService.Create(CampaignStatus);
            string json = JsonConvert.SerializeObject(CampaignStatus, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        public ActionResult SaveChanges(CampaignStatusCrudModel CampaignStatusEdited)
        {
            CampaignStatus CampaignStatus =
                Mapper.Map<CampaignStatusCrudModel, CampaignStatus>(CampaignStatusEdited);
            _CampaignStatusService.Update(CampaignStatus);
            string json = JsonConvert.SerializeObject(CampaignStatusEdited, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult EditCampaignStatus(int Id, string ColumnChanged, string Value)
        {
            CampaignStatus UpCampaignStatus = _CampaignStatusService.GetById(Id);
            string ColChangedType = UpCampaignStatus.GetType().GetProperty(ColumnChanged).PropertyType.Name;
            object ChangedVal = EfFilterHelper.GetValueByColType(Value, ColChangedType);
            UpCampaignStatus.GetType().GetProperty(ColumnChanged).SetValue(UpCampaignStatus, ChangedVal);
            _CampaignStatusService.Update(UpCampaignStatus);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCampaignStatus(int Id)
        {
            CampaignStatus CampaignStatus = _CampaignStatusService.GetById(Id);
            _CampaignStatusService.Delete(CampaignStatus);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllStatus()
        {
            IEnumerable<CampaignStatus> Statuss;
            Statuss = _CampaignStatusService.GetAll();

            string json = JsonConvert.SerializeObject(Statuss, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }


        #endregion
    }
}