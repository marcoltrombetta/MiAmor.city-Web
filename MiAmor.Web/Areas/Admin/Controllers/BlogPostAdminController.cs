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
using MiAmor.Web.Areas.Admin.Models;
using MiAmor.Web.Area;

namespace MiAmor.Web.Areas.Admin.Controllers
{
    public class BlogPostAdminController : Controller
    {
        #region fields
        private IBlogPostService _BlogPostService;
        #endregion

         #region ctr
        public BlogPostAdminController(IBlogPostService IBlogPostService)
        {
            _BlogPostService = IBlogPostService;
        }
        #endregion

        #region actions
        // GET: Admin/Customer
   [HttpPost]
        public ActionResult GetFilteredBlogPosts(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            PagedDataResponse<BlogPost> PagedDataResponse = _BlogPostService.GetFilteredBlogPosts(FilterElements, PageOptions);
            string json = JsonConvert.SerializeObject(PagedDataResponse, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        public ActionResult GetBlogPostById(int Id)
        {
            BlogPost BlogPost = _BlogPostService.GetById(Id);
            BlogPostCrudModel NewBlogPost =
                Mapper.Map<BlogPost, BlogPostCrudModel>(BlogPost);
            string json = JsonConvert.SerializeObject(NewBlogPost, Formatting.Indented,
                                     new JsonSerializerSettings
                                     {
                                         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                     });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult InsertNewBlogPost(BlogPostCrudModel NewBlogPost)
        {
            BlogPost BlogPost =
                Mapper.Map<BlogPostCrudModel, BlogPost>(NewBlogPost);
            BlogPost.CreatedOnUtc = DateTime.Now;
            _BlogPostService.Create(BlogPost);
            string json = JsonConvert.SerializeObject(BlogPost, Formatting.Indented,
                                     new JsonSerializerSettings
                                     {
                                         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                     });
            return Content(json, "application/json");
        }

         [HttpPost]
        public ActionResult SaveChanges(BlogPostCrudModel BlogPostEdited)
        {
            BlogPost BlogPost =
                Mapper.Map<BlogPostCrudModel, BlogPost>(BlogPostEdited);
            BlogPost.CreatedOnUtc = DateTime.Now;
            _BlogPostService.Update(BlogPost);
            string json = JsonConvert.SerializeObject(BlogPostEdited, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult EditBlogPost(int Id, string ColumnChanged, string Value)
        {
            BlogPost UpBlogPost = _BlogPostService.GetById(Id);
            string ColChangedType = UpBlogPost.GetType().GetProperty(ColumnChanged).PropertyType.Name;
            object ChangedVal = EfFilterHelper.GetValueByColType(Value, ColChangedType);
            UpBlogPost.GetType().GetProperty(ColumnChanged).SetValue(UpBlogPost, ChangedVal);
            _BlogPostService.Update(UpBlogPost);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

         [HttpPost]
        public ActionResult DeleteBlogPost(int Id)
        {
            BlogPost BlogPost = _BlogPostService.GetById(Id);
            _BlogPostService.Delete(BlogPost);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

         public ActionResult GetBlogPostAutoComplete(string PartialName)
         {
             List<BlogPost> BlogPosts = _BlogPostService.GetBlogPostByPartialName(PartialName, 5);
             List<BlogPostAutocompleteModel> BlogPostAutocompleteModels = new List<BlogPostAutocompleteModel>();
             BlogPostAutocompleteModels =
                 Mapper.Map<List<BlogPost>, List<BlogPostAutocompleteModel>>(BlogPosts);
             string json = JsonConvert.SerializeObject(BlogPostAutocompleteModels, Formatting.Indented,
                                    new JsonSerializerSettings
                                    {
                                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                    });
             return Content(json, "application/json");
         }
        #endregion
    }
}