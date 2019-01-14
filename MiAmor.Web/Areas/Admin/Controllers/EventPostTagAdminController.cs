using AutoMapper;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Web.Area;
using MiAmor.Web.Areas.Admin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiAmor.Web.Areas.Admin.Controllers
{
    public class EventPostTagAdminController : Controller
    {
        #region fields
        private IEventPostTagService _EventPostTagService;
        #endregion

         #region ctr
        public EventPostTagAdminController(IEventPostTagService IEventPostTagService)
        {
            _EventPostTagService = IEventPostTagService;
        }
        #endregion

        #region actions
        // GET: Admin/Customer
        [HttpPost]
        public ActionResult GetFilteredEventPostTags(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            PagedDataResponse<EventPostTag> PagedDataResponse = _EventPostTagService.GetFilteredEventPostTags(FilterElements, PageOptions);
            string json = JsonConvert.SerializeObject(PagedDataResponse, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        public ActionResult GetEventPostTagById(int Id)
        {
            EventPostTag EventPostTag = _EventPostTagService.GetById(Id);
            EventPostTagCrudModel NewEventPostTag =
                Mapper.Map<EventPostTag, EventPostTagCrudModel>(EventPostTag);
            string json = JsonConvert.SerializeObject(NewEventPostTag, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult InsertNewEventPostTag(EventPostTagCrudModel NewEventPostTag)
        {
            EventPostTag EventPostTag =
                Mapper.Map<EventPostTagCrudModel, EventPostTag>(NewEventPostTag);
            _EventPostTagService.Create(EventPostTag);
            string json = JsonConvert.SerializeObject(EventPostTag, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        public ActionResult SaveChanges(EventPostTagCrudModel EventPostTagEdited)
        {
            EventPostTag EventPostTag =
                Mapper.Map<EventPostTagCrudModel, EventPostTag>(EventPostTagEdited);
            _EventPostTagService.Update(EventPostTag);
            string json = JsonConvert.SerializeObject(EventPostTagEdited, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult EditEventPostTag(int Id, string ColumnChanged, string Value)
        {
            EventPostTag UpEventPostTag = _EventPostTagService.GetById(Id);
            string ColChangedType = UpEventPostTag.GetType().GetProperty(ColumnChanged).PropertyType.Name;
            object ChangedVal = EfFilterHelper.GetValueByColType(Value, ColChangedType);
            UpEventPostTag.GetType().GetProperty(ColumnChanged).SetValue(UpEventPostTag, ChangedVal);
            _EventPostTagService.Update(UpEventPostTag);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteEventPostTag(int Id)
        {
            EventPostTag EventPostTag = _EventPostTagService.GetById(Id);
            _EventPostTagService.Delete(EventPostTag);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}