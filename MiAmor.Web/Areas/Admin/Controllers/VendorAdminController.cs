using AutoMapper;
using System.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;

using MiAmor.Services;
using MiAmor.Data;
using MiAmor.Core;
using MiAmor.Web.Models;
using MiAmor.Web.Filters;
using MiAmor.Services;
using MiAmor.Web.Areas.Admin.Models;
using Newtonsoft.Json;

namespace MiAmor.Web.Areas.Admin.Controllers
{
    public class VendorAdminController : Controller
    {

        #region fields
        private IVendorService _VendorService;
        private IVendorAddressService _VendorAddressService;
        #endregion
        #region ctr
        public VendorAdminController(IVendorService IVendorService, IVendorAddressService VendorAddressService)
        {
            _VendorService = IVendorService;
            _VendorAddressService = VendorAddressService;
        }
        #endregion

        #region actions
        // GET: Admin/Customer
        [HttpPost]
        public ActionResult GetFilteredVendors(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            PagedDataResponse<Vendor> PagedDataResponse = _VendorService.GetFilteredVendors(FilterElements, PageOptions);
            string json = JsonConvert.SerializeObject(PagedDataResponse, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                PreserveReferencesHandling= PreserveReferencesHandling.Objects
                            });

            return Content(json, "application/json");            
        }

        [HttpPost]
        public ActionResult EditVendor(int Id, string ColumnChanged, string Value)
        {
            
            if (ColumnChanged == "Addresses[0].Cities.Name")
            {
               VendorAddress VendorAddress= _VendorAddressService.GetByVendorId(Id);
               VendorAddress.Cities.Name = Value;
               _VendorAddressService.Update(VendorAddress);
            }
            else if (ColumnChanged == "Addresses[0].PhoneNumber")
            {
               VendorAddress VendorAddress= _VendorAddressService.GetByVendorId(Id);
               VendorAddress.PhoneNumber = Value;
               _VendorAddressService.Update(VendorAddress);
            }
            else if (ColumnChanged == "Addresses[0].MobileNumber")
            {
                VendorAddress VendorAddress = _VendorAddressService.GetByVendorId(Id);
                VendorAddress.MobileNumber = Value;
                _VendorAddressService.Update(VendorAddress);
            }
            else if (ColumnChanged == "Addresses[0].MainEmail")
            {
                VendorAddress VendorAddress = _VendorAddressService.GetByVendorId(Id);
                VendorAddress.MainEmail = Value;
                _VendorAddressService.Update(VendorAddress);
            }
            else
            {
                Vendor UpVendor = _VendorService.GetById(Id);
                UpVendor.GetType().GetProperty(ColumnChanged).SetValue(UpVendor, Value);
                _VendorService.Update(UpVendor);
            }
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteVendor(int Id)
        {
            Vendor Vendor = _VendorService.GetById(Id);
            _VendorService.Delete(Vendor);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetVendorById(int Id)
        {
            Vendor Vendor = _VendorService.GetById(Id);
            VendorCrudModel NewVendor =
                Mapper.Map<Vendor, VendorCrudModel>(Vendor);
            string json = JsonConvert.SerializeObject(NewVendor, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
            return Content(json, "application/json");      
            
        }

        [HttpPost]
        public ActionResult InsertNewVendor(VendorCrudModel NewVendor)
        {
            Vendor Vendor =
                Mapper.Map<VendorCrudModel, Vendor>(NewVendor);
            if (NewVendor.VendorAddressCrudModel[0].address1 != null)
            {
                Vendor.Addresses =
                Mapper.Map<List<VendorAddressCrudModel>, List<VendorAddress>>(NewVendor.VendorAddressCrudModel);
                Vendor.Active = true;
                Vendor.Addresses[0].Active = true;
                Vendor.Addresses[0].CreatedOnUtc = DateTime.Now;
            }
            _VendorService.Create(Vendor);
            string json = JsonConvert.SerializeObject(Vendor, Formatting.Indented,
                                        new JsonSerializerSettings
                                        {
                                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                        });
            return Content(json, "application/json");
        }

        public ActionResult SaveChanges(VendorCrudModel VendorEdited)
        {
            Vendor Vendor =
                Mapper.Map<VendorCrudModel, Vendor>(VendorEdited);
            Vendor.Addresses=
                Mapper.Map<List<VendorAddressCrudModel>, List<VendorAddress>>(VendorEdited.VendorAddressCrudModel);
            foreach (VendorAddress VA in Vendor.Addresses)
            {
                VA.VendorId = VendorEdited.Id;
                VA.CreatedOnUtc = DateTime.Now;
                _VendorAddressService.Update(VA);
            }
            _VendorService.Update(Vendor);
            string json = JsonConvert.SerializeObject(VendorEdited, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        public ActionResult GetVendorAdressByVendorId(int VendorId)
        {
            VendorAddress VendorAddress = _VendorAddressService.GetByVendorId(VendorId);
            VendorAddressCrudModel NewVendorAddress =
                Mapper.Map<VendorAddress, VendorAddressCrudModel>(VendorAddress);
            string json = JsonConvert.SerializeObject(NewVendorAddress, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        public ActionResult GetVendorDetailsById(int Id)
        {
            Vendor Vendor = _VendorService.GetBoxDetailsById(Id);
            VendorCrudModel NewVendor =
                Mapper.Map<Vendor, VendorCrudModel>(Vendor);
            NewVendor.VendorAddressCrudModel =
                Mapper.Map<List<VendorAddress>, List<VendorAddressCrudModel>>(Vendor.Addresses);
            string json = JsonConvert.SerializeObject(NewVendor, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }


        public ActionResult GetVendorAutoComplete(string PartialName)
        {
            List<Vendor> Vendors = _VendorService.GetVendorByPartialName(PartialName,5);
            List<VendorAutocompleteModel> VendorAutocompleteModels = new List<VendorAutocompleteModel>();
            VendorAutocompleteModels =
                Mapper.Map<List<Vendor>, List<VendorAutocompleteModel>>(Vendors);
            string json = JsonConvert.SerializeObject(VendorAutocompleteModels, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        
        #endregion
    }
}