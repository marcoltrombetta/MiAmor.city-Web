using AutoMapper;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiAmor.Web.Controllers
{
    public class ContactUsController : Controller
    {

        #region fields

        private IMessageService _MessageService;
        private ICustomerService _CustomerService;
      
        #endregion

        #region ctr
        public ContactUsController(IMessageService MessageService, ICustomerService CustomerService)
        {
            _MessageService = MessageService;
            _CustomerService = CustomerService;
        }

        #endregion

        public ActionResult Index()
        {
           return View();
        }

        [HttpPost]
        public ActionResult SendMessage(ContactUsModel model) {

            Customer Customer = _CustomerService.GetByCookie();

            if (ModelState.IsValid)
            {
                Message message = Mapper.Map<ContactUsModel, Message>(model);
                message.CustomerId = Customer.Id;
                message.CreatedOnUtc = DateTime.Now;
                message.UpdateOnUtc = DateTime.Now;
                _MessageService.Create(message);
            }
        
            return Json(new { data = "OK" }, JsonRequestBehavior.AllowGet);
        }
    }
}