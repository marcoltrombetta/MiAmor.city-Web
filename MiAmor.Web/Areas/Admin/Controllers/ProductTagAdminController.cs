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
    public class ProductTagAdminController : Controller
    {
           #region fields
        private IProductTagService _ProductTagService;
        #endregion

         #region ctr
        public ProductTagAdminController(IProductTagService IProductTagService)
        {
            _ProductTagService = IProductTagService;
        }
        #endregion

        #region actions
        // GET: Admin/Customer
        [HttpPost]
        public ActionResult GetFilteredProductTags(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            PagedDataResponse<ProductTag> PagedDataResponse = _ProductTagService.GetFilteredProductTags(FilterElements, PageOptions);
            string json = JsonConvert.SerializeObject(PagedDataResponse, Formatting.Indented,
                                          new JsonSerializerSettings
                                          {
                                              ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                          });
            return Content(json, "application/json");
        }

        public ActionResult GetProductTagById(int Id)
        {
            ProductTag ProductTag = _ProductTagService.GetById(Id);
            ProductTagCrudModel NewProductTag =
                Mapper.Map<ProductTag, ProductTagCrudModel>(ProductTag);
            string json = JsonConvert.SerializeObject(NewProductTag, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult InsertNewProductTag(ProductTagCrudModel NewProductTag)
        {
            ProductTag ProductTag =
                Mapper.Map<ProductTagCrudModel, ProductTag>(NewProductTag);
            _ProductTagService.Create(ProductTag);
            string json = JsonConvert.SerializeObject(ProductTag, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        public ActionResult SaveChanges(ProductTagCrudModel ProductTagEdited)
        {
            ProductTag ProductTag =
                Mapper.Map<ProductTagCrudModel, ProductTag>(ProductTagEdited);
            _ProductTagService.Update(ProductTag);
            string json = JsonConvert.SerializeObject(ProductTagEdited, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult EditProductTag(int Id, string ColumnChanged, string Value)
        {
            ProductTag UpProductTag = _ProductTagService.GetById(Id);
            string ColChangedType = UpProductTag.GetType().GetProperty(ColumnChanged).PropertyType.Name;
            object ChangedVal = EfFilterHelper.GetValueByColType(Value, ColChangedType);
            UpProductTag.GetType().GetProperty(ColumnChanged).SetValue(UpProductTag, ChangedVal); 
            _ProductTagService.Update(UpProductTag);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteProductTag(int Id)
        {
            ProductTag ProductTag = _ProductTagService.GetById(Id);
            _ProductTagService.Delete(ProductTag);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        #endregion    
    }
}