
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
    public class ManufacturerAdminController : Controller
    {
        #region fields
        private IManufacturerService _ManufactererService;
        #endregion

         #region ctr
        public ManufacturerAdminController(IManufacturerService IManufacturerService)
        {
            _ManufactererService = IManufacturerService;
        }
        #endregion

        #region actions
        // GET: Admin/Customer
        [HttpPost]
        public ActionResult GetFilteredManufacturers(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            PagedDataResponse<Manufacturer> PagedDataResponse = _ManufactererService.GetFilteredManufacturers(FilterElements, PageOptions);
            string json = JsonConvert.SerializeObject(PagedDataResponse, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        public ActionResult GetManufacturerById(int Id)
        {
            Manufacturer Manufacturer = _ManufactererService.GetById(Id);
            ManufacturerCrudModel NewManufacturer =
                Mapper.Map<Manufacturer, ManufacturerCrudModel>(Manufacturer);
            string json = JsonConvert.SerializeObject(NewManufacturer, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult InsertNewManufacturer(ManufacturerCrudModel NewManufacturer)
        {
            Manufacturer Manufacturer =
                Mapper.Map<ManufacturerCrudModel, Manufacturer>(NewManufacturer);
            Manufacturer.CreatedOnUtc = DateTime.Now;
            Manufacturer.UpdatedOnUtc = DateTime.Now;
            _ManufactererService.Create(Manufacturer);
            string json = JsonConvert.SerializeObject(Manufacturer, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        public ActionResult SaveChanges(ManufacturerCrudModel ManufacturerEdited)
        {
            Manufacturer Manufacturer =
                Mapper.Map<ManufacturerCrudModel, Manufacturer>(ManufacturerEdited);
            Manufacturer.CreatedOnUtc = DateTime.Now;
            Manufacturer.UpdatedOnUtc = DateTime.Now;
            _ManufactererService.Update(Manufacturer);
            string json = JsonConvert.SerializeObject(ManufacturerEdited, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult EditManufacturer(int Id, string ColumnChanged, string Value)
        {
            Manufacturer UpManufacturer = _ManufactererService.GetById(Id);
            string ColChangedType = UpManufacturer.GetType().GetProperty(ColumnChanged).PropertyType.Name;
            object ChangedVal = EfFilterHelper.GetValueByColType(Value, ColChangedType);
            UpManufacturer.GetType().GetProperty(ColumnChanged).SetValue(UpManufacturer, ChangedVal); 
            _ManufactererService.Update(UpManufacturer);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteManufacturer(int Id)
        {
            Manufacturer Manufacturer = _ManufactererService.GetById(Id);
            _ManufactererService.Delete(Manufacturer);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}