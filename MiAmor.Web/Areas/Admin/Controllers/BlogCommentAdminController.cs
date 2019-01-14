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
    public class BlogCommentAdminController : Controller
    {
        #region fields
        private IBlogCommentService _BlogCommentService;
        #endregion

         #region ctr
        public BlogCommentAdminController(IBlogCommentService IBlogCommentService)
        {
            _BlogCommentService = IBlogCommentService;
        }
        #endregion

        #region actions
        // GET: Admin/Customer
   [HttpPost]
        public ActionResult GetFilteredBlogComments(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            PagedDataResponse<BlogComment> PagedDataResponse = _BlogCommentService.GetFilteredBlogComments(FilterElements, PageOptions);
            string json = JsonConvert.SerializeObject(PagedDataResponse, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        public ActionResult GetBlogCommentById(int Id)
        {
            BlogComment BlogComment = _BlogCommentService.GetById(Id);
            BlogCommentCrudModel NewBlogComment =
                Mapper.Map<BlogComment, BlogCommentCrudModel>(BlogComment);
            string json = JsonConvert.SerializeObject(NewBlogComment, Formatting.Indented,
                                     new JsonSerializerSettings
                                     {
                                         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                     });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult InsertNewBlogComment(BlogCommentCrudModel NewBlogComment)
        {
            BlogComment BlogComment =
                Mapper.Map<BlogCommentCrudModel, BlogComment>(NewBlogComment);
            BlogComment.CreatedOnUtc = DateTime.Now;
            _BlogCommentService.Create(BlogComment);
            string json = JsonConvert.SerializeObject(BlogComment, Formatting.Indented,
                                     new JsonSerializerSettings
                                     {
                                         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                     });
            return Content(json, "application/json");
        }

         [HttpPost]
        public ActionResult SaveChanges(BlogCommentCrudModel BlogCommentEdited)
        {
            BlogComment BlogComment =
                Mapper.Map<BlogCommentCrudModel, BlogComment>(BlogCommentEdited);
            BlogComment.CreatedOnUtc = DateTime.Now;
            _BlogCommentService.Update(BlogComment);
            string json = JsonConvert.SerializeObject(BlogCommentEdited, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult EditBlogComment(int Id, string ColumnChanged, string Value)
        {
            BlogComment UpBlogComment = _BlogCommentService.GetById(Id);
            string ColChangedType = UpBlogComment.GetType().GetProperty(ColumnChanged).PropertyType.Name;
            object ChangedVal = EfFilterHelper.GetValueByColType(Value, ColChangedType);
            UpBlogComment.GetType().GetProperty(ColumnChanged).SetValue(UpBlogComment, ChangedVal);
            _BlogCommentService.Update(UpBlogComment);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

         [HttpPost]
        public ActionResult DeleteBlogComment(int Id)
        {
            BlogComment BlogComment = _BlogCommentService.GetById(Id);
            _BlogCommentService.Delete(BlogComment);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}