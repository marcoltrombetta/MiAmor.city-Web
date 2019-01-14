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
using MiAmor.Web.Areas.Admin.Models;
using Newtonsoft.Json;
using MiAmor.Web.Area;

namespace MiAmor.Web.Areas.Admin.Controllers
{
    public class CustomerAdminController : Controller
    {
       
            #region fields
            private ICustomerService _CustomerService;           
            private ICustomerTypeService _CustomerTypeService;
            #endregion
            #region ctr
            public CustomerAdminController(ICustomerService ICustomerService, ICustomerTypeService CustomerTypeService)
            {
                _CustomerTypeService = CustomerTypeService;
                _CustomerService = ICustomerService;             
            }
            #endregion

            #region actions
            // GET: Admin/Customer
            [HttpPost]
            public ActionResult GetFilteredCustomers(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
            {             
                PagedDataResponse<Customer> PagedDataResponse = _CustomerService.GetFilteredCustomers(FilterElements, PageOptions);
                string json = JsonConvert.SerializeObject(PagedDataResponse, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
                return Content(json, "application/json");
            }

            [HttpPost]
            public ActionResult InsertNewCustomer(CustomerCrudModel NewCustomer)
            {
                Customer Customer =
                    Mapper.Map<CustomerCrudModel, Customer>(NewCustomer);
                Customer.Active = true;
                _CustomerService.Create(Customer);
                string json = JsonConvert.SerializeObject(Customer, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
                return Content(json, "application/json");
            }

            [HttpPost]
            public ActionResult EditCustomer(int Id, string ColumnChanged, string Value)
            {
                Customer UpCustomer = _CustomerService.GetById(Id);
                string ColChangedType = UpCustomer.GetType().GetProperty(ColumnChanged).PropertyType.Name;
                object ChangedVal = EfFilterHelper.GetValueByColType(Value, ColChangedType);
                UpCustomer.GetType().GetProperty(ColumnChanged).SetValue(UpCustomer, ChangedVal);       
                _CustomerService.Update(UpCustomer);
                return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
            }

            public ActionResult GetCustomerById(int Id) {
                Customer Customer = _CustomerService.GetById(Id);
                CustomerCrudModel NewCustomer =
                    Mapper.Map<Customer, CustomerCrudModel>(Customer);                                             
                return Json(NewCustomer, JsonRequestBehavior.AllowGet);
            }

            public ActionResult SaveChanges(CustomerCrudModel CustomerEdited)
            {
                Customer Customer =
                    Mapper.Map<CustomerCrudModel, Customer>(CustomerEdited);    
                _CustomerService.Update(Customer);
                string json = JsonConvert.SerializeObject(CustomerEdited, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
                return Content(json, "application/json");
            }

            public ActionResult DeleteCustomer(int Id)
            {
                Customer Customer = _CustomerService.GetById(Id);
                    _CustomerService.Delete(Customer);
                return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
            }         

            public ActionResult GetCustomerTypes()
            {
                IEnumerable<CustomerType> CustomerTypes;
                CustomerTypes = _CustomerTypeService.GetAll();

                string json = JsonConvert.SerializeObject(CustomerTypes, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
                return Content(json, "application/json");
            }

            [HttpPost]
            public ActionResult GetAllCustomers()
            {
                IEnumerable<Customer> Customers;
                Customers = _CustomerService.GetAll();

                string json = JsonConvert.SerializeObject(Customers, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
                return Content(json, "application/json");
            }

            public ActionResult GetCustomerAutoComplete(string PartialName)
            {
                List<Customer> Customers = _CustomerService.GetCustomerByPartialName(PartialName, 5);
                List<CustomerAutocompleteModel> CustomerAutocompleteModels = new List<CustomerAutocompleteModel>();
                CustomerAutocompleteModels =
                    Mapper.Map<List<Customer>, List<CustomerAutocompleteModel>>(Customers);
                string json = JsonConvert.SerializeObject(CustomerAutocompleteModels, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
                return Content(json, "application/json");
            }

            #endregion
        }
    }
