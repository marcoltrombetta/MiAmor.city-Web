using AutoMapper;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Web.Areas.Admin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiAmor.Web.Area;
namespace MiAmor.Web.Areas.Admin.Controllers
{
    public class EventPostAdminController : Controller
    {
        #region fields
        private IEventPostService _EventPostService;
        #endregion

         #region ctr
        public EventPostAdminController(IEventPostService IEventPostService)
        {
            _EventPostService = IEventPostService;
        }
        #endregion

        #region actions
        // GET: Admin/Customer
        [HttpPost]
        public ActionResult GetFilteredEventPosts(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            PagedDataResponse<EventPost> PagedDataResponse = _EventPostService.GetFilteredEventPosts(FilterElements, PageOptions);
            string json = JsonConvert.SerializeObject(PagedDataResponse, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        public ActionResult GetEventPostById(int Id)
        {
            EventPost EventPost = _EventPostService.GetById(Id);
            EventPostCrudModel NewEventPost =
                Mapper.Map<EventPost, EventPostCrudModel>(EventPost);
            string json = JsonConvert.SerializeObject(NewEventPost, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult InsertNewEventPost(EventPostCrudModel NewEventPost)
        {
            EventPost EventPost =
                Mapper.Map<EventPostCrudModel, EventPost>(NewEventPost);
            EventPost.CreatedOnUtc = DateTime.Now;
            EventPost.StartDateUtc = DateTime.Now;
            EventPost.EndDateUtc = DateTime.Now;
            _EventPostService.Create(EventPost);
            string json = JsonConvert.SerializeObject(EventPost, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        public ActionResult SaveChanges(EventPostCrudModel EventPostEdited)
        {
            EventPost EventPost =
                Mapper.Map<EventPostCrudModel, EventPost>(EventPostEdited);
          
            _EventPostService.Update(EventPost);
            string json = JsonConvert.SerializeObject(EventPostEdited, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult EditEventPost(int Id, string ColumnChanged, string Value)
        {            
            EventPost UpEventPost = _EventPostService.GetById(Id);
            string ColChangedType=UpEventPost.GetType().GetProperty(ColumnChanged).PropertyType.Name;
            object ChangedVal = EfFilterHelper.GetValueByColType(Value, ColChangedType);
            UpEventPost.GetType().GetProperty(ColumnChanged).SetValue(UpEventPost, ChangedVal);
            _EventPostService.Update(UpEventPost);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteEventPost(int Id)
        {
            EventPost EventPost = _EventPostService.GetById(Id);
            _EventPostService.Delete(EventPost);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEventPostAutoComplete(string PartialName)
        {
            List<EventPost> EventPosts = _EventPostService.GetEventPostByPartialName(PartialName, 5);
            List<EventPostAutocompleteModel> EventPostAutocompleteModels = new List<EventPostAutocompleteModel>();
            EventPostAutocompleteModels =
                Mapper.Map<List<EventPost>, List<EventPostAutocompleteModel>>(EventPosts);
            string json = JsonConvert.SerializeObject(EventPostAutocompleteModels, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        

        #endregion
    }
}