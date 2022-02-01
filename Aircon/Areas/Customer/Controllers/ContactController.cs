using Aircon.Areas.Customer.Models.Contact;
using Aircon.Business.Services.Customer;
using Aircon.Data.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Aircon.Extensions;
using System.Linq;
using System;

namespace Aircon.Areas.Customer.Controllers
{
    public class ContactController : BaseCustomerController
    {

        private readonly ICustomerContactService _customerContactService;

        public ContactController(ICustomerContactService customerContactService)
        {
            _customerContactService = customerContactService;
        }

        public IActionResult Index()
        {
            var result = new CustomerContactListViewModel();
            var customerId = HttpContextHelper.CustomerId;
            if (!customerId.HasValue)
                return AccessDeniedView();
            var searchText = SearchText();
            int id = customerId.HasValue ? customerId.Value : 0;
            result.Contacts = _customerContactService.GetCustomerContacts(id,searchText).Select(x => x.ToViewModel()).ToList();
            return View(result);
        }
        public IActionResult DeleteContact(int Id)
        {
            _customerContactService.DeleteContact(Id);
            return RedirectToAction("Index");
        }

        public IActionResult EditContactPartial(int Id)
        {
            var customerId = HttpContextHelper.CustomerId;
            if (!customerId.HasValue)
                return AccessDeniedView();
            int id = customerId.HasValue ? customerId.Value : 0;
            var viewModel = _customerContactService.GetCustomerContact(Id).ToViewModel();
            return PartialView("EditContactPartial", viewModel);
        }
        public IActionResult AddContactPartial()
        {
            var customerId = HttpContextHelper.CustomerId;
            if (!customerId.HasValue)
                return AccessDeniedView();
            int id = customerId.HasValue ? customerId.Value : 0;
            CustomerContactViewModel customerContactViewModel = new CustomerContactViewModel();
            return PartialView("AddContactPartial", customerContactViewModel);
        }

        public IActionResult AddContact(CustomerContactViewModel customerContact)
        {
            var customerId = HttpContextHelper.CustomerId;
            customerContact.CustomerId = Convert.ToInt32(customerId);
            var res = _customerContactService.AddContact(customerContact.ToModel());
            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult SaveContact(CustomerContactViewModel addUserView)
        {
            CustomerContactViewModel SaveContact = _customerContactService.UpdateContact(addUserView.ToModel()).ToViewModel();
            return RedirectToAction("Index");
        }
    }
}
