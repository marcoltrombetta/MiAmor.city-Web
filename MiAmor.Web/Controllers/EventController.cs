using AutoMapper;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiAmor.Web.Controllers
{
    public class EventController : Controller
    {

        #region fields

        private IEventPostService _EventPostService;
        private IParentWebSiteBlogPostService _ParentWebSiteBlogPostService;
      
        #endregion

        #region ctr
        public EventController(IEventPostService EventPostService, IParentWebSiteBlogPostService ParentWebSiteBlogPostService)
        {
            _EventPostService = EventPostService;
            _ParentWebSiteBlogPostService = ParentWebSiteBlogPostService;
        }

        #endregion

        // GET: Event
        public ActionResult EventListingGetMore(int PageNumber = 1)
        {
            EventListingModel EventListingModel = GetEventListingUtility(PageNumber);
            if (EventListingModel.Events.Count <= 0)
            {
                return Json(new { data = "nothing" }, JsonRequestBehavior.AllowGet);
            }
            return PartialView("_Timelinepanel", EventListingModel.Events);
        }

        public ActionResult Index(int PageNumber = 1, Int16 BlogPostType=2)
        {
            EventListingModel EventListingModel = GetEventListingUtility(PageNumber, BlogPostType);
            return View("Index", EventListingModel.Events);
        }

        
        #region utility

        private EventListingModel GetEventListingUtility(int PageNumber = 1, Int16 BlogPostType=2)
        {
            try
            {
                PagedDataRequest PagedDataRequest = new PagedDataRequest(PageNumber, "", "", 6);

                PagedDataResponse<ParentWebSiteBlogPost> EventReviewPagedDataResponse = _ParentWebSiteBlogPostService.GetBlogTimeLine(PagedDataRequest, BlogPostType);

                EventPagingModel EventPagingModel = new EventPagingModel(PageNumber, PagedDataRequest.RowsPerPage, EventReviewPagedDataResponse.total);

                EventListingModel EventListingModel = new EventListingModel();
                EventListingModel.EventPagingModel = EventPagingModel;
                EventListingModel.Events = Mapper.Map<List<ParentWebSiteBlogPost>, List<EventModel>>(EventReviewPagedDataResponse.rows);
                return EventListingModel;
            }
            catch (Exception e)
            {
            }
            return null;
        }

        #endregion
    }
}