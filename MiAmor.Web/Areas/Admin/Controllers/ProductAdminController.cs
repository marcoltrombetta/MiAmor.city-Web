
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
    public class ProductAdminController : Controller
    {
        #region fields
        private IProductService _ProductService;
        #endregion

         #region ctr
        public ProductAdminController(IProductService IProductService)
        {
            _ProductService = IProductService;
        }
        #endregion

        #region actions
        // GET: Admin/Customer
        [HttpPost]
        public ActionResult GetFilteredProducts(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            PagedDataResponse<Product> PagedDataResponse = _ProductService.GetFilteredProducts(FilterElements, PageOptions);
            string json = JsonConvert.SerializeObject(PagedDataResponse, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        public ActionResult GetProductById(int Id)
        {
            Product Product = _ProductService.GetById(Id);
            ProductCrudModel NewProduct =
                Mapper.Map<Product, ProductCrudModel>(Product);
            string json = JsonConvert.SerializeObject(NewProduct, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult InsertNewProduct(ProductCrudModel NewProduct)
        {
            
            Product Product =
                Mapper.Map<ProductCrudModel, Product>(NewProduct);
            Product.CreatedOnUtc = DateTime.Now;
            Product.UpdatedOnUtc = DateTime.Now;
            Product.AvailableStartDateTimeUtc = DateTime.Now;
            Product.AvailableEndDateTimeUtc = DateTime.Now;
            _ProductService.Create(Product);
            string json = JsonConvert.SerializeObject(Product, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        public ActionResult SaveChanges(ProductCrudModel ProductEdited)
        {
            Product Product =
                Mapper.Map<ProductCrudModel, Product>(ProductEdited);
            Product.CreatedOnUtc = DateTime.Now;
            Product.UpdatedOnUtc = DateTime.Now;
            Product.AvailableStartDateTimeUtc = DateTime.Now;
            Product.AvailableEndDateTimeUtc = DateTime.Now;
            _ProductService.Update(Product);
            string json = JsonConvert.SerializeObject(ProductEdited, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult EditProduct(int Id, string ColumnChanged, string Value)
        {
            Product UpProduct = _ProductService.GetById(Id);
            string ColChangedType = UpProduct.GetType().GetProperty(ColumnChanged).PropertyType.Name;
            object ChangedVal = EfFilterHelper.GetValueByColType(Value, ColChangedType);
            UpProduct.GetType().GetProperty(ColumnChanged).SetValue(UpProduct, ChangedVal); 
            _ProductService.Update(UpProduct);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteProduct(int Id)
        {
            Product Product = _ProductService.GetById(Id);
            _ProductService.Delete(Product);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAllProducts()
        {
            IEnumerable<Product> Products;
            Products = _ProductService.GetAll();
            string json = JsonConvert.SerializeObject(Products, Formatting.Indented,
                                                  new JsonSerializerSettings
                                                  {
                                                      ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                  });
            return Content(json, "application/json");
        }
        #endregion
    }
}