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
    public class BlogPostTagAdminController : Controller
    {
        #region fields
        private IBlogPostTagService _BlogPostTagService;
        #endregion

         #region ctr
        public BlogPostTagAdminController(IBlogPostTagService IBlogPostTagService)
        {
            _BlogPostTagService = IBlogPostTagService;
        }
        #endregion

        #region actions
        // GET: Admin/Customer
   [HttpPost]
        public ActionResult GetFilteredBlogPostTags(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            PagedDataResponse<BlogPostTag> PagedDataResponse = _BlogPostTagService.GetFilteredBlogPostTags(FilterElements, PageOptions);
            string json = JsonConvert.SerializeObject(PagedDataResponse, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        public ActionResult GetBlogPostTagById(int Id)
        {
            BlogPostTag BlogPostTag = _BlogPostTagService.GetById(Id);
            BlogPostTagCrudModel NewBlogPostTag =
                Mapper.Map<BlogPostTag, BlogPostTagCrudModel>(BlogPostTag);
            string json = JsonConvert.SerializeObject(NewBlogPostTag, Formatting.Indented,
                                     new JsonSerializerSettings
                                     {
                                         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                     });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult InsertNewBlogPostTag(BlogPostTagCrudModel NewBlogPostTag)
        {
            BlogPostTag BlogPostTag =
                Mapper.Map<BlogPostTagCrudModel, BlogPostTag>(NewBlogPostTag);
            _BlogPostTagService.Create(BlogPostTag);
            string json = JsonConvert.SerializeObject(BlogPostTag, Formatting.Indented,
                                     new JsonSerializerSettings
                                     {
                                         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                     });
            return Content(json, "application/json");
        }

         [HttpPost]
        public ActionResult SaveChanges(BlogPostTagCrudModel BlogPostTagEdited)
        {
            BlogPostTag BlogPostTag =
                Mapper.Map<BlogPostTagCrudModel, BlogPostTag>(BlogPostTagEdited);
            _BlogPostTagService.Update(BlogPostTag);
            string json = JsonConvert.SerializeObject(BlogPostTagEdited, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult EditBlogPostTag(int Id, string ColumnChanged, string Value)
        {
            BlogPostTag UpBlogPostTag = _BlogPostTagService.GetById(Id);
            string ColChangedType = UpBlogPostTag.GetType().GetProperty(ColumnChanged).PropertyType.Name;
            object ChangedVal = EfFilterHelper.GetValueByColType(Value, ColChangedType);
            UpBlogPostTag.GetType().GetProperty(ColumnChanged).SetValue(UpBlogPostTag, ChangedVal);
            _BlogPostTagService.Update(UpBlogPostTag);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

         [HttpPost]
        public ActionResult DeleteBlogPostTag(int Id)
        {
            BlogPostTag BlogPostTag = _BlogPostTagService.GetById(Id);
            _BlogPostTagService.Delete(BlogPostTag);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}