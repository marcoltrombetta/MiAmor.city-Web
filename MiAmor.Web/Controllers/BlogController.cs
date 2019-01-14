using System.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using AutoMapper;
using MiAmor.Services;
using MiAmor.Data;
using MiAmor.Core;
using MiAmor.Web.Filters;
using MiAmor.Web.Models;
using Newtonsoft.Json;

namespace MiAmor.Web.Controllers
{

    public class BlogController : Controller
    {

        #region fields

        private IBlogPostService _BlogPostService;
        private IBlogCommentService _BlogCommentService;
        ICustomerService _CustomerService;
        IVendorCategoryService _VendorCategoryService;
        private IParentWebSiteBlogPostService _ParentWebSiteBlogPostService;
        #endregion

        #region ctr
        public BlogController(IBlogPostService BlogPostService,IBlogCommentService BlogCommentService,
             ICustomerService CustomerService, IVendorCategoryService VendorCategoryService,
            IParentWebSiteBlogPostService ParentWebSiteBlogPostService)
        {
            _ParentWebSiteBlogPostService = ParentWebSiteBlogPostService;
            _BlogPostService = BlogPostService;
            _BlogCommentService = BlogCommentService;
            _CustomerService = CustomerService;
            _VendorCategoryService = VendorCategoryService;
        }

        #endregion

        #region actions
        // GET: Blog

        //public ActionResult Navigation(int CategoryId = 0, int PageId = 0)
        //{
        //    List<VendorCategory> Categories = _VendorCategoryService.GetParentNavigationVendors(CategoryId, PageId);
        //    return PartialView("_Navigation", Categories);
        //}
        public ActionResult GetBlogLastestPostByCategoryId(int CategoryId)
        {

            List<BlogPost> ListBlog = _ParentWebSiteBlogPostService.GetLastestBlogPostByCategoryId(CategoryId,2);

            List<BlogModel> model = Mapper.Map<List<BlogPost>, List<BlogModel>>(ListBlog);

            return PartialView("_BlogLastestPost", model);
        }

        public ActionResult Index(Int16 BlogPostType = 1, int PageNumber = 1)
        {
            BlogListingModel BlogListingModel = GetBlogListingUtility(PageNumber,BlogPostType);
            if (BlogPostType == 1)
            {
                return View("Index", BlogListingModel.Blogs);
            }
            else
            {
                return View("IndexEvent", BlogListingModel.Blogs);
            }
        }

        public ActionResult BlogListingGetMore(Int16 BlogPostType = 1, int PageNumber = 1)
        {
            if (BlogPostType == 1)
            {
                BlogListingModel BlogListingModel = GetBlogListingUtility(PageNumber, BlogPostType);
                if (BlogListingModel.Blogs.Count <= 0)
                {
                    return Json(new { data = "nothing" }, JsonRequestBehavior.AllowGet);
                }
                return PartialView("_Timelinepanel", BlogListingModel.Blogs);
            }
            else {
                BlogListingModel BlogListingModel = GetBlogListingUtility(PageNumber, BlogPostType);
                if (BlogListingModel.Blogs.Count <= 0)
                {
                    return Json(new { data = "nothing" }, JsonRequestBehavior.AllowGet);
                }
                return PartialView("_TimelinepanelEvent", BlogListingModel.Blogs);
            }
        }

        public ActionResult AddBlogLike(int Id, bool Plus)
        {
            Customer Customer = _CustomerService.GetByCookie();

            BlogPost BlogPost = _BlogPostService.GetById(Id);

            if (Plus) {
                BlogPost.TotalLikes++;
            }
            else
            {
                BlogPost.TotalLikes--;
            }
                       
            _BlogPostService.Update(BlogPost);

            return Json(new { total = BlogPost.TotalLikes }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BlogSendMessage(BlogMessageModel model)
        {
            if (!_CustomerService.IsCustExistsByEmail(model.Email))
            {
                Customer Customer = new Customer();
                Customer.FirstName = model.Name;
                Customer.LastName = model.LastName;
                Customer.Email = model.Email;
                _CustomerService.Create(Customer);
            }
           
            BlogComment CustomerComment = new BlogComment();
            CustomerComment.Customer = _CustomerService.GetCustomerByEmail(model.Email);
            CustomerComment.BlogPostId = model.BlogPostId;
            CustomerComment.CustomerId = CustomerComment.Customer.Id;
            CustomerComment.CommentText = model.CommentText;
            CustomerComment.CreatedOnUtc = DateTime.Now;
            _BlogCommentService.Create(CustomerComment);

            return Json(new { data="" }, JsonRequestBehavior.AllowGet);
        }
     

        public ActionResult BlogPost(int Id)
        {
            BlogPost BlogModel = _BlogPostService.GetById(Id);

            BlogModel model = Mapper.Map<BlogPost, BlogModel>(BlogModel);

            return View("BlogPost", model);
        }

        public ActionResult GetBlogLastestPost(int count=2) {

            List<BlogPost> ListBlog = _ParentWebSiteBlogPostService.GetLastestBlogPost(count);

             List<BlogModel> model = Mapper.Map<List<BlogPost>, List<BlogModel>>(ListBlog);

             return PartialView("_BlogLastestPost", model);
        }


        public ActionResult GetBlogPostComments(int pageNumber=1) {
            BlogCommentsListingModel BlogComment = BlogCommentsUtility(4,pageNumber);

            return PartialView("_BlogPostComments", BlogComment);
        }

        public ActionResult GetBlogCategories(int CategoryId = 0, int PageId = 0)
        {
            if (CategoryId == null)
            {
                CategoryId = 0;
            }
            List<VendorCategory> Categories = _VendorCategoryService.GetParentNavigationVendors(CategoryId, PageId).Take(5).ToList();
            return PartialView("_BlogPostCategories", Categories);
            //List<BlogCategory> BlogCategory = _BlogCategoryService.GetCategories();

            //return PartialView("_BlogPostCategories", BlogCategory);
        }

#endregion
        #region utility 
        private BlogCommentsListingModel BlogCommentsUtility(int BlogId,int PageNumber)
        {
            try
            {
                PagedDataRequest PagedDataRequest = new PagedDataRequest(PageNumber, "", "");

                PagedDataResponse<BlogComment> BlogCommentsPagedDataResponse = _BlogCommentService.GetCommentsByBlogPostId(PagedDataRequest, BlogId);

                BlogPagingModel BlogCommentsPagingModel = new BlogPagingModel(PageNumber, PagedDataRequest.RowsPerPage, BlogCommentsPagedDataResponse.total);

                BlogCommentsListingModel BlogCommentsListingModel = new BlogCommentsListingModel();
                BlogCommentsListingModel.BlogPagingModel= BlogCommentsPagingModel;
                BlogCommentsListingModel.BlogComments = BlogCommentsPagedDataResponse.rows;
                return BlogCommentsListingModel;
            }
            catch (Exception e)
            {
                int i = 0;
            }
            return null;
        }

        private BlogListingModel GetBlogListingUtility(int PageNumber = 1, Int16 BlogPostType = 1)
        {
            try
            {
                try
                {
                    PagedDataRequest PagedDataRequest = new PagedDataRequest(PageNumber, "", "", 6);

                    PagedDataResponse<BlogPost> EventReviewPagedDataResponse = _ParentWebSiteBlogPostService.GetBlogTimeLine(PagedDataRequest, BlogPostType);

                    BlogPagingModel BlogPagingModel = new BlogPagingModel(PageNumber, PagedDataRequest.RowsPerPage, EventReviewPagedDataResponse.total);

                    BlogListingModel BlogListingModel = new BlogListingModel();
                    BlogListingModel.BlogPagingModel = BlogPagingModel;
                    BlogListingModel.Blogs = Mapper.Map<List<BlogPost>, List<BlogModel>>(EventReviewPagedDataResponse.rows);
                    return BlogListingModel;
                }
                catch (Exception e)
                {
                }
                return null;
            }
            catch (Exception e)
            {
            }
            return null;
        }

        private BlogPostAppModel GetBlogListingApp(int BlogPostId)
        {
            BlogPost BlogPost = _BlogPostService.GetByAppPost(BlogPostId);
            BlogPostAppModel BlogPostAppModel =
                Mapper.Map<BlogPost, BlogPostAppModel>(BlogPost);
            return BlogPostAppModel;

        }
        #endregion

        #region mobile
        public ActionResult MApp_GetBlogDetails(string AppSecret, int BlogPostId)
        {
            if (AppSecret != "MiAmor")
                return null;
            BlogPostAppModel BlogPostAppModel = GetBlogListingApp(BlogPostId);

            string json = JsonConvert.SerializeObject(BlogPostAppModel, Formatting.Indented,
                          new JsonSerializerSettings
                          {
                              ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                          });
            return Content(json, "application/json");
        }

        public ActionResult MApp_GetBlogListing(string AppSecret, int PageNumber = 1)
        {
            if (AppSecret != "MiAmor")
                return null;
            BlogListingModel BlogListingModel = GetBlogListingUtility(PageNumber);
           string json=  JsonConvert.SerializeObject(BlogListingModel, Formatting.Indented,
                           new JsonSerializerSettings
                           {
                               ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                           });
             return Content(json, "application/json");
        }
        #endregion
    }
}