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
    public class EventCommentAdminController : Controller
     {
        #region fields
        private IEventCommentService _EventCommentService;
        #endregion

         #region ctr
        public EventCommentAdminController(IEventCommentService IEventCommentService)
        {
            _EventCommentService = IEventCommentService;
        }
        #endregion

        #region actions
        // GET: Admin/Customer
   [HttpPost]
        public ActionResult GetFilteredEventComments(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            PagedDataResponse<EventComment> PagedDataResponse = _EventCommentService.GetFilteredEventComments(FilterElements, PageOptions);
            string json = JsonConvert.SerializeObject(PagedDataResponse, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        public ActionResult GetEventCommentById(int Id)
        {
            EventComment EventComment = _EventCommentService.GetById(Id);
            EventCommentCrudModel NewEventComment =
                Mapper.Map<EventComment, EventCommentCrudModel>(EventComment);
            string json = JsonConvert.SerializeObject(NewEventComment, Formatting.Indented,
                                     new JsonSerializerSettings
                                     {
                                         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                     });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult InsertNewEventComment(EventCommentCrudModel NewEventComment)
        {
            EventComment EventComment =
                Mapper.Map<EventCommentCrudModel, EventComment>(NewEventComment);
            EventComment.CreatedOnUtc = DateTime.Now;
            _EventCommentService.Create(EventComment);
            string json = JsonConvert.SerializeObject(EventComment, Formatting.Indented,
                                     new JsonSerializerSettings
                                     {
                                         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                     });
            return Content(json, "application/json");
        }

         [HttpPost]
        public ActionResult SaveChanges(EventCommentCrudModel EventCommentEdited)
        {
            EventComment EventComment =
                Mapper.Map<EventCommentCrudModel, EventComment>(EventCommentEdited);
            EventComment.CreatedOnUtc = DateTime.Now;
            _EventCommentService.Update(EventComment);
            string json = JsonConvert.SerializeObject(EventCommentEdited, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult EditEventComment(int Id, string ColumnChanged, string Value)
        {
            EventComment UpEventComment = _EventCommentService.GetById(Id);
            string ColChangedType = UpEventComment.GetType().GetProperty(ColumnChanged).PropertyType.Name;
            object ChangedVal = EfFilterHelper.GetValueByColType(Value, ColChangedType);
            UpEventComment.GetType().GetProperty(ColumnChanged).SetValue(UpEventComment, ChangedVal);
            _EventCommentService.Update(UpEventComment);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

         [HttpPost]
        public ActionResult DeleteEventComment(int Id)
        {
            EventComment EventComment = _EventCommentService.GetById(Id);
            _EventCommentService.Delete(EventComment);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}