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
    public class VendorCategoryAdminController : Controller
    {
        #region fields
        private IVendorCategoryService _VendorCategoryService;
        #endregion

         #region ctr
        public VendorCategoryAdminController(IVendorCategoryService IVendorCategoryService)
        {
            _VendorCategoryService = IVendorCategoryService;
        }
        #endregion

        #region actions
        // GET: Admin/Customer
        [HttpPost]
        public ActionResult GetFilteredVendorCategories(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            PagedDataResponse<VendorCategory> PagedDataResponse = _VendorCategoryService.GetFilteredVendorCategories(FilterElements, PageOptions);
            List<VendorCategoryCrudModel> VendorCategoryCrudModelList =
                Mapper.Map<List<VendorCategory>, List<VendorCategoryCrudModel>>(PagedDataResponse.rows);
            PagedDataResponse<VendorCategoryCrudModel> PagedDataResponseReturn = new PagedDataResponse<VendorCategoryCrudModel>(PagedDataResponse.total, PagedDataResponse.totalFiltered, VendorCategoryCrudModelList, 0);
            string json = JsonConvert.SerializeObject(PagedDataResponseReturn, Formatting.Indented,
                                     new JsonSerializerSettings
                                     {
                                         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                     });
            return Content(json, "application/json");
        }

        public ActionResult GetVendorCategoryById(int Id)
        {
            VendorCategory VendorCategory = _VendorCategoryService.GetById(Id);
            VendorCategoryCrudModel NewVendorCategory =
                Mapper.Map<VendorCategory, VendorCategoryCrudModel>(VendorCategory);
            string json = JsonConvert.SerializeObject(NewVendorCategory, Formatting.Indented,
                                     new JsonSerializerSettings
                                     {
                                         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                     });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult InsertNewVendorCategory(VendorCategoryCrudModel NewVendorCategory)
        {
            VendorCategory VendorCategory =
                Mapper.Map<VendorCategoryCrudModel, VendorCategory>(NewVendorCategory);
            VendorCategory.CreatedOnUtc = DateTime.Now;
            VendorCategory.UpdatedOnUtc = DateTime.Now;
            _VendorCategoryService.Create(VendorCategory);
            string json = JsonConvert.SerializeObject(VendorCategory, Formatting.Indented,
                                     new JsonSerializerSettings
                                     {
                                         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                     });
            return Content(json, "application/json");
        }

        public ActionResult SaveChanges(VendorCategoryCrudModel VendorCategoryEdited)
        {
            VendorCategory VendorCategory =
                Mapper.Map<VendorCategoryCrudModel, VendorCategory>(VendorCategoryEdited);
            VendorCategory.CreatedOnUtc = DateTime.Now;
            VendorCategory.UpdatedOnUtc = DateTime.Now;
            _VendorCategoryService.Update(VendorCategory);
            string json = JsonConvert.SerializeObject(VendorCategoryEdited, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult EditVendorCategory(int Id, string ColumnChanged, string Value)
        {
            VendorCategory UpVendorCategory = _VendorCategoryService.GetById(Id);
            string ColChangedType = UpVendorCategory.GetType().GetProperty(ColumnChanged).PropertyType.Name;
            object ChangedVal = EfFilterHelper.GetValueByColType(Value, ColChangedType);
            UpVendorCategory.GetType().GetProperty(ColumnChanged).SetValue(UpVendorCategory, ChangedVal); 
            _VendorCategoryService.Update(UpVendorCategory);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteVendorCategory(int Id)
        {
            VendorCategory VendorCategory = _VendorCategoryService.GetById(Id);
            _VendorCategoryService.Delete(VendorCategory);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
    
}