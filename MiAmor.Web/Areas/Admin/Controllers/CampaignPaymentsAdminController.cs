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
    public class CampaignPaymentsAdminController : Controller
   {
        #region fields
        private ICampaignPaymentService _CampaignPaymentService;
        #endregion

         #region ctr
        public CampaignPaymentsAdminController(ICampaignPaymentService ICampaignPaymentService)
        {
            _CampaignPaymentService = ICampaignPaymentService;
        }
        #endregion

        #region actions
        // GET: Admin/Customer
        [HttpPost]
        public ActionResult GetFilteredCampaignPayments(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            PagedDataResponse<CampaignPayment> PagedDataResponse = _CampaignPaymentService.GetFilteredCampaignPayments(FilterElements, PageOptions);
            string json = JsonConvert.SerializeObject(PagedDataResponse, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        public ActionResult GetCampaignPaymentsById(int Id)
        {
            CampaignPayment CampaignPayment = _CampaignPaymentService.GetById(Id);
            CampaignPaymentsCrudModel NewCampaignPayment =
                Mapper.Map<CampaignPayment, CampaignPaymentsCrudModel>(CampaignPayment);
            string json = JsonConvert.SerializeObject(NewCampaignPayment, Formatting.Indented,
                                      new JsonSerializerSettings
                                      {
                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                      });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult InsertNewCampaignPayments(CampaignPaymentsCrudModel NewCampaignPayment)
        {
            CampaignPayment CampaignPayment =
                Mapper.Map<CampaignPaymentsCrudModel, CampaignPayment>(NewCampaignPayment);
            CampaignPayment.PurchaseDate = DateTime.Now;
            CampaignPayment.PayDate = DateTime.Now;
            CampaignPayment.SyTimeStamp = DateTime.Now;
            _CampaignPaymentService.Create(CampaignPayment);
            string json = JsonConvert.SerializeObject(CampaignPayment, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        public ActionResult SaveChanges(CampaignPaymentsCrudModel CampaignPaymentEdited)
        {
            CampaignPayment CampaignPayment =
                Mapper.Map<CampaignPaymentsCrudModel, CampaignPayment>(CampaignPaymentEdited);
            CampaignPayment.PurchaseDate = DateTime.Now;
            CampaignPayment.PayDate = DateTime.Now;
            CampaignPayment.SyTimeStamp = DateTime.Now;
            _CampaignPaymentService.Update(CampaignPayment);
            string json = JsonConvert.SerializeObject(CampaignPaymentEdited, Formatting.Indented,
                                       new JsonSerializerSettings
                                       {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       });
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult EditCampaignPayments(int Id, string ColumnChanged, string Value)
        {
            CampaignPayment UpCampaignPayment = _CampaignPaymentService.GetById(Id);
            string ColChangedType = UpCampaignPayment.GetType().GetProperty(ColumnChanged).PropertyType.Name;
            object ChangedVal = EfFilterHelper.GetValueByColType(Value, ColChangedType);
            UpCampaignPayment.GetType().GetProperty(ColumnChanged).SetValue(UpCampaignPayment, ChangedVal);
            _CampaignPaymentService.Update(UpCampaignPayment);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCampaignPayments(int Id)
        {
            CampaignPayment CampaignPayment = _CampaignPaymentService.GetById(Id);
            _CampaignPaymentService.Delete(CampaignPayment);
            return Json(new { data = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}