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
    public class ProductCategoryAdminController : Controller
    {
       #region fields
        private IProductCategoryService _ProductCategoryService;
        #endregion

         #region ctr
        public ProductCategoryAdminController(IProductCategoryService IProductCategoryService)
        {
            _ProductCategoryService = IProductCategoryService;
        }
        #endregion

        #region actions
        // GET: Admin/Customer
        [HttpPost]
        public ActionResult GetFilteredProductCategories(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            PagedDataResponse<ProductCategory> PagedDataResponse = _ProductCategoryService.GetFilteredProductCategories(FilterElements, PageOptions);
            string json = JsonConvert.SerializeObject(PagedDataResponse, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        public ActionResult GetProductCategoryById(int Id)
        {
            ProductCategory ProductCategory = _ProductCategoryService.GetById(Id);
            ProductCategoryCrudModel NewProductCategory =
                Mapper.Map<ProductCategory, ProductCategoryCrudModel>(ProductCategory);
            string json = JsonConvert.SerializeObject(NewProductCategory, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult InsertNewProductCategory(ProductCategoryCrudModel NewProductCategory)
        {
            ProductCategory ProductCategory =
                Mapper.Map<ProductCategoryCrudModel, ProductCategory>(NewProductCategory);
            ProductCategory.CreatedOnUtc = DateTime.Now;
            ProductCategory.UpdatedOnUtc = DateTime.Now;
            _ProductCategoryService.Create(ProductCategory);
            string json = JsonConvert.SerializeObject(ProductCategory, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        public ActionResult SaveChanges(ProductCategoryCrudModel ProductCategoryEdited)
        {
            ProductCategory ProductCategory =
                Mapper.Map<ProductCategoryCrudModel, ProductCategory>(ProductCategoryEdited);
            ProductCategory.CreatedOnUtc = DateTime.Now;
            ProductCategory.UpdatedOnUtc = DateTime.Now;
            _ProductCategoryService.Update(ProductCategory);
            string json = JsonConvert.SerializeObject(ProductCategoryEdited, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult EditProductCategory(int Id, string ColumnChanged, string Value)
        {
            ProductCategory UpProductCategory = _ProductCategoryService.GetById(Id);
            string ColChangedType = UpProductCategory.GetType().GetProperty(ColumnChanged).PropertyType.Name;
            object ChangedVal = EfFilterHelper.GetValueByColType(Value, ColChangedType);
            UpProductCategory.GetType().GetProperty(ColumnChanged).SetValue(UpProductCategory, ChangedVal); 
            _ProductCategoryService.Update(UpProductCategory);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteProductCategory(int Id)
        {
            ProductCategory ProductCategory = _ProductCategoryService.GetById(Id);
            _ProductCategoryService.Delete(ProductCategory);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetParentCategoryAutoComplete(string PartialName)
        {
            List<ProductCategory> ProductCategories = _ProductCategoryService.GetParentCategoryByPartialName(PartialName, 5);
            List<ParentCategoryAutocompleteModel> ParentCategoryAutocompleteModels = new List<ParentCategoryAutocompleteModel>();
            ParentCategoryAutocompleteModels =
                Mapper.Map<List<ProductCategory>, List<ParentCategoryAutocompleteModel>>(ProductCategories);
            string json = JsonConvert.SerializeObject(ParentCategoryAutocompleteModels, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        #endregion
    }
}