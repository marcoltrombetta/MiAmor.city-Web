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
    public class VendorCategoryCountAdminController : Controller
    {
        #region fields
        private IVendorCategoryCountService _VendorCategoryCountService;
        #endregion

         #region ctr
        public VendorCategoryCountAdminController(IVendorCategoryCountService IVendorCategoryCountService)
        {
            _VendorCategoryCountService = IVendorCategoryCountService;
        }
        #endregion

        #region actions
        // GET: Admin/Customer
        [HttpPost]
        public ActionResult GetFilteredVendorCategoryCounts(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            PagedDataResponse<VendorCategoryCount> PagedDataResponse = _VendorCategoryCountService.GetFilteredVendorCategoryCounts(FilterElements, PageOptions);
            List<VendorCategoryCountCrudModel> VendorCategoryCountCrudModelList =
                Mapper.Map<List<VendorCategoryCount>, List<VendorCategoryCountCrudModel>>(PagedDataResponse.rows);
            PagedDataResponse<VendorCategoryCountCrudModel> PagedDataResponseReturn = new PagedDataResponse<VendorCategoryCountCrudModel>(PagedDataResponse.total, PagedDataResponse.totalFiltered, VendorCategoryCountCrudModelList, 0);
            string json = JsonConvert.SerializeObject(PagedDataResponseReturn, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        public ActionResult GetVendorCategoryCountById(int Id)
        {
            VendorCategoryCount VendorCategoryCount = _VendorCategoryCountService.GetById(Id);
            VendorCategoryCountCrudModel NewVendorCategoryCount =
                Mapper.Map<VendorCategoryCount, VendorCategoryCountCrudModel>(VendorCategoryCount);
            string json = JsonConvert.SerializeObject(NewVendorCategoryCount, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult InsertNewVendorCategoryCount(VendorCategoryCountCrudModel NewVendorCategoryCount)
        {
            VendorCategoryCount VendorCategoryCount =
                Mapper.Map<VendorCategoryCountCrudModel, VendorCategoryCount>(NewVendorCategoryCount);
            _VendorCategoryCountService.Create(VendorCategoryCount);
            string json = JsonConvert.SerializeObject(VendorCategoryCount, Formatting.Indented,
                                     new JsonSerializerSettings
                                     {
                                         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                     });
            return Content(json, "application/json");
        }

        public ActionResult SaveChanges(VendorCategoryCountCrudModel VendorCategoryCountEdited)
        {
            VendorCategoryCount VendorCategoryCount =
                Mapper.Map<VendorCategoryCountCrudModel, VendorCategoryCount>(VendorCategoryCountEdited);
            _VendorCategoryCountService.Update(VendorCategoryCount);
            string json = JsonConvert.SerializeObject(VendorCategoryCountEdited, Formatting.Indented,
                                     new JsonSerializerSettings
                                     {
                                         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                     });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult EditVendorCategoryCount(int Id, string ColumnChanged, string Value)
        {
            VendorCategoryCount UpVendorCategoryCount = _VendorCategoryCountService.GetById(Id);
            string ColChangedType = UpVendorCategoryCount.GetType().GetProperty(ColumnChanged).PropertyType.Name;
            object ChangedVal = EfFilterHelper.GetValueByColType(Value, ColChangedType);
            UpVendorCategoryCount.GetType().GetProperty(ColumnChanged).SetValue(UpVendorCategoryCount, ChangedVal); 
            _VendorCategoryCountService.Update(UpVendorCategoryCount);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteVendorCategoryCount(int Id)
        {
            VendorCategoryCount VendorCategoryCount = _VendorCategoryCountService.GetById(Id);
            _VendorCategoryCountService.Delete(VendorCategoryCount);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}