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
    public class ProductAttributeAdminController : Controller
    {
         #region fields
        private IProductAttributeService _ProductAttributeService;
        #endregion

         #region ctr
        public ProductAttributeAdminController(IProductAttributeService IProductAttributeService)
        {
            _ProductAttributeService = IProductAttributeService;
        }
        #endregion

        #region actions
        // GET: Admin/Customer
        [HttpPost]
        public ActionResult GetFilteredProductAttributes(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            PagedDataResponse<ProductAttribute> PagedDataResponse = _ProductAttributeService.GetFilteredProductAttributes(FilterElements, PageOptions);
            string json = JsonConvert.SerializeObject(PagedDataResponse, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        public ActionResult GetProductAttributeById(int Id)
        {
            ProductAttribute ProductAttribute = _ProductAttributeService.GetById(Id);
            ProductAttributeCrudModel NewProductAttribute =
                Mapper.Map<ProductAttribute, ProductAttributeCrudModel>(ProductAttribute);
            string json = JsonConvert.SerializeObject(NewProductAttribute, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult InsertNewProductAttribute(ProductAttributeCrudModel NewProductAttribute)
        {
            ProductAttribute ProductAttribute =
                Mapper.Map<ProductAttributeCrudModel, ProductAttribute>(NewProductAttribute);
            _ProductAttributeService.Create(ProductAttribute);
            string json = JsonConvert.SerializeObject(ProductAttribute, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        public ActionResult SaveChanges(ProductAttributeCrudModel ProductAttributeEdited)
        {
            ProductAttribute ProductAttribute =
                Mapper.Map<ProductAttributeCrudModel, ProductAttribute>(ProductAttributeEdited);
            _ProductAttributeService.Update(ProductAttribute);
            string json = JsonConvert.SerializeObject(ProductAttributeEdited, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult EditProductAttribute(int Id, string ColumnChanged, string Value)
        {
            ProductAttribute UpProductAttribute = _ProductAttributeService.GetById(Id);
            string ColChangedType = UpProductAttribute.GetType().GetProperty(ColumnChanged).PropertyType.Name;
            object ChangedVal = EfFilterHelper.GetValueByColType(Value, ColChangedType);
            UpProductAttribute.GetType().GetProperty(ColumnChanged).SetValue(UpProductAttribute, ChangedVal); 
            _ProductAttributeService.Update(UpProductAttribute);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteProductAttribute(int Id)
        {
            ProductAttribute ProductAttribute = _ProductAttributeService.GetById(Id);
            _ProductAttributeService.Delete(ProductAttribute);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}