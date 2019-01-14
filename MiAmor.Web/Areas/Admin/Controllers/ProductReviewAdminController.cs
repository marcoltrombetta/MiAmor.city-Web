using AutoMapper;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Web.Areas.Admin.Models;
using MiAmor.Web.Areas.Admin.Models;
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
    public class ProductReviewAdminController : Controller
    {
        #region fields
        private IProductReviewService _ProductReviewService;
        #endregion

         #region ctr
        public ProductReviewAdminController(IProductReviewService IProductReviewService)
        {
            _ProductReviewService = IProductReviewService;
        }
        #endregion

        #region actions
        // GET: Admin/Customer
        [HttpPost]
        public ActionResult GetFilteredProductReviews(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            PagedDataResponse<ProductReview> PagedDataResponse = _ProductReviewService.GetFilteredProductReviews(FilterElements, PageOptions);
            List<ProductReviewCrudModel> ProductReviewCrudModelList =
                Mapper.Map<List<ProductReview>, List<ProductReviewCrudModel>>(PagedDataResponse.rows);
            PagedDataResponse<ProductReviewCrudModel> PagedDataResponseReturn = new PagedDataResponse<ProductReviewCrudModel>(PagedDataResponse.total, PagedDataResponse.totalFiltered, ProductReviewCrudModelList, 0);
            string json = JsonConvert.SerializeObject(PagedDataResponseReturn, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        public ActionResult GetProductReviewById(int Id)
        {
            ProductReview ProductReview = _ProductReviewService.GetById(Id);
            ProductReviewCrudModel NewProductReview =
                Mapper.Map<ProductReview, ProductReviewCrudModel>(ProductReview);

            NewProductReview.Customer =
                Mapper.Map<Customer, CustomerCrudModel>(ProductReview.Customer);

            NewProductReview.Product =
                Mapper.Map<Product, ProductCrudModel>(ProductReview.Product);

            string json = JsonConvert.SerializeObject(NewProductReview, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult InsertNewProductReview(ProductReviewCrudModel NewProductReview)
        {
            ProductReview ProductReview =
                Mapper.Map<ProductReviewCrudModel, ProductReview>(NewProductReview);
            ProductReview.CreatedOnUtc = DateTime.Now;
            _ProductReviewService.Create(ProductReview);
            string json = JsonConvert.SerializeObject(ProductReview, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        public ActionResult SaveChanges(ProductReviewCrudModel ProductReviewEdited)
        {
            ProductReview ProductReview =
                Mapper.Map<ProductReviewCrudModel, ProductReview>(ProductReviewEdited);
            ProductReview.CreatedOnUtc = DateTime.Now;
            _ProductReviewService.Update(ProductReview);
            string json = JsonConvert.SerializeObject(ProductReviewEdited, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult EditProductReview(int Id, string ColumnChanged, string Value)
        {
            ProductReview UpProductReview = _ProductReviewService.GetById(Id);
            string ColChangedType = UpProductReview.GetType().GetProperty(ColumnChanged).PropertyType.Name;
            object ChangedVal = EfFilterHelper.GetValueByColType(Value, ColChangedType);
            UpProductReview.GetType().GetProperty(ColumnChanged).SetValue(UpProductReview, ChangedVal); 
            _ProductReviewService.Update(UpProductReview);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteProductReview(int Id)
        {
            ProductReview ProductReview = _ProductReviewService.GetById(Id);
            _ProductReviewService.Delete(ProductReview);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        #endregion    
    }
}