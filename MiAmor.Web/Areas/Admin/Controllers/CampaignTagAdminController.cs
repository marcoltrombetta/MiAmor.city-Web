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
    public class CampaignTagAdminController : Controller
    {
        #region fields
        private ICampaignTagService _CampaignTagService;
        #endregion

         #region ctr
        public CampaignTagAdminController(ICampaignTagService ICampaignTagService)
        {
            _CampaignTagService = ICampaignTagService;
        }
        #endregion

        #region actions
        // GET: Admin/Customer
        [HttpPost]
        public ActionResult GetFilteredCampaignTags(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            PagedDataResponse<CampaignTag> PagedDataResponse = _CampaignTagService.GetFilteredCampaignTags(FilterElements, PageOptions);
            string json = JsonConvert.SerializeObject(PagedDataResponse, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        public ActionResult GetCampaignTagById(int Id)
        {
            CampaignTag CampaignTag = _CampaignTagService.GetById(Id);
            CampaignTagCrudModel NewCampaignTag =
                Mapper.Map<CampaignTag, CampaignTagCrudModel>(CampaignTag);
            string json = JsonConvert.SerializeObject(NewCampaignTag, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult InsertNewCampaignTag(CampaignTagCrudModel NewCampaignTag)
        {
            CampaignTag CampaignTag =
                Mapper.Map<CampaignTagCrudModel, CampaignTag>(NewCampaignTag);
            _CampaignTagService.Create(CampaignTag);
            string json = JsonConvert.SerializeObject(CampaignTag, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        public ActionResult SaveChanges(CampaignTagCrudModel CampaignTagEdited)
        {
            CampaignTag CampaignTag =
                Mapper.Map<CampaignTagCrudModel, CampaignTag>(CampaignTagEdited);
            _CampaignTagService.Update(CampaignTag);
            string json = JsonConvert.SerializeObject(CampaignTagEdited, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult EditCampaignTag(int Id, string ColumnChanged, string Value)
        {
            CampaignTag UpCampaignTag = _CampaignTagService.GetById(Id);
            string ColChangedType = UpCampaignTag.GetType().GetProperty(ColumnChanged).PropertyType.Name;
            object ChangedVal = EfFilterHelper.GetValueByColType(Value, ColChangedType);
            UpCampaignTag.GetType().GetProperty(ColumnChanged).SetValue(UpCampaignTag, ChangedVal);
            _CampaignTagService.Update(UpCampaignTag);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCampaignTag(int Id)
        {
            CampaignTag CampaignTag = _CampaignTagService.GetById(Id);
            _CampaignTagService.Delete(CampaignTag);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}