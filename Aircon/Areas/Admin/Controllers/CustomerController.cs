using Aircon.Areas.Admin.Models.Customer;
using Aircon.Areas.Customer.Models;
using Aircon.Business.Models.Admin.Customer;
using Aircon.Business.Models.Shared;
using Aircon.Business.Services.Admin;
using Aircon.Business.Services.Customer;
using Aircon.Business.Services.Shared;
using Aircon.Core.Data;
using Aircon.Data.Entities;
using Aircon.Data.Enums;
using Aircon.Data.Helper;
using Aircon.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vg.Common.Notification.Message;

namespace Aircon.Areas.Admin.Controllers
{
    public class CustomerController : BaseAdminController
    {
        private readonly ICustomerAdminService _customerAdminService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly INotify _notify;
        public CustomerController(ICustomerAdminService customerAdminService, IGenericAttributeService genericAttributeService, INotify notify)
        {
            _customerAdminService = customerAdminService;
            _genericAttributeService = genericAttributeService;
            _notify = notify;
        }
        public async Task<IActionResult> Index(CustomerAdminListViewModel customerAdminUserListViewModel)
        {
            var searchText = SearchText();
            int recordCountCustomersOpportunityQueue = await _genericAttributeService.GetAttributeAsync<UserModel, int>(HttpContextHelper.UserId.Value, CardGridSetting.AdminCustomer.CustomerOpportunityQueue, 5);
            int recordCountCustomers = await _genericAttributeService.GetAttributeAsync<UserModel, int>(HttpContextHelper.UserId.Value, CardGridSetting.AdminCustomer.Customers, 5);
            customerAdminUserListViewModel.CustomerOpportunities = _customerAdminService.GetCustomerOpportunities(CustomerOpportunityStatus.CallbackScheduled, searchText, recordCountCustomersOpportunityQueue).Select(x => x.ToViewModel()).ToList();
            customerAdminUserListViewModel.Customers = _customerAdminService.GetCustomers(true, true, searchText, recordCountCustomers).Select(x => x.ToViewModel()).ToList();
            return View(customerAdminUserListViewModel);
        }

        public async Task<IActionResult> CustomersPartialAsync(bool isActive, bool isAll)
        {
            List<CustomerAdminViewModel> customers = new List<CustomerAdminViewModel>();
            var searchText = SearchText();
            int recordCountCustomersProfileQueue = await _genericAttributeService.GetAttributeAsync<UserModel, int>(HttpContextHelper.UserId.Value, CardGridSetting.AdminCustomer.Customers, 5);
            customers = _customerAdminService.GetCustomers(isActive, isAll, searchText, recordCountCustomersProfileQueue).Select(x => x.ToViewModel()).ToList();
            return PartialView("_AdminCustomerContainer", customers);
        }

        public async Task<IActionResult> CustomerOpportunityPartialAsync(CustomerOpportunityStatus status)
        {
            var searchText = SearchText();
            int recordCountCustomersOpportunityQueue = await _genericAttributeService.GetAttributeAsync<UserModel, int>(HttpContextHelper.UserId.Value, CardGridSetting.AdminCustomer.CustomerOpportunityQueue, 5);
            List<CustomerOpportunityAdminViewModel> customerOpportunities = _customerAdminService.GetCustomerOpportunities(status, searchText, recordCountCustomersOpportunityQueue).Select(x => x.ToViewModel()).OrderBy(x => x.CompanyName).ToList();
            return PartialView("_QueueCustomersContainer", customerOpportunities);
        }
        public IActionResult AbandonedQueueCustomers()
        {
            return RedirectToAction("CustomerOpportunityPartial", new { status = CustomerOpportunityStatus.Abandoned });
        }
        public IActionResult CallbackScheduledQueueCustomers()
        {
            return RedirectToAction("CustomerOpportunityPartial", new { status = CustomerOpportunityStatus.CallbackScheduled });
        }

        public IActionResult AllCustomers()
        {
            return RedirectToAction("CustomersPartial", new { isActive = true, isAll = true });
        }

        public IActionResult ActiveCustomers()
        {
            return RedirectToAction("CustomersPartial", new { isActive = true, isAll = false });
        }
        public IActionResult InActiveCustomers()
        {
            return RedirectToAction("CustomersPartial", new { isActive = false, isAll = false });
        }
        //public IActionResult ViewCustomer(int id)
        //{
        //    CustomerAdminViewModel viewCustomerViewModel = new CustomerAdminViewModel();
        //    var model = viewCustomerViewModel.ToModel();
        //    model = _customerAdminService.GetCustomerById(id);
        //    var webModel = model.ToViewModel();
        //    return View(webModel);
        //}
        public IActionResult UpdateCustomerPost(CustomerAdminViewModel viewCustomerViewModel)
        {
            var model = viewCustomerViewModel.ToModel();
            model = _customerAdminService.UpdateCustomer(model);
            var webModel = model.ToViewModel();
            return RedirectToAction("Index");
        }
        public IActionResult UpdateCustomerOpportunityPost(CustomerOpportunityAdminViewModel viewCustomerOppViewModel)
        {
            var model = viewCustomerOppViewModel.ToModel();
            model = _customerAdminService.UpdateCustomerOpportunity(model);
            var webModel = model.ToViewModel();
            return RedirectToAction("Index");
        }
        public IActionResult CustomerDeleteProfile(int Id)
        {
            CustomerAdminViewModel customerAdminViewModel = _customerAdminService.GetCustomer(Id).ToViewModel();
            return PartialView("~/Areas/Admin/Views/Customer/CustomerDeleteProfile.cshtml", customerAdminViewModel);
        }

        public IActionResult DeleteCustomerProfile(CustomerAdminViewModel customerAdminViewModel)
        {
            _customerAdminService.DeleteCustomerProfile(customerAdminViewModel.CustomerId);
            return RedirectToAction("Index");

        }
        public IActionResult DeleteCustomerOpportunity(int Id)
        {
            _customerAdminService.DeleteCustomerOpportunity(Id);
            return RedirectToAction("Index");
        }
        public IActionResult DenyCustomerOpportunity(int Id)
        {
            _customerAdminService.DenyCustomerOpportunity(Id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ApproveCustomer(int Id)
        {
            _customerAdminService.ApproveCustomer(Id);
            var customer = _customerAdminService.GetCustomerOpportunity(Id);
            var notifyModel = new NotifyEmailModel { displayname = string.Format("{0}", customer.AdminName) };
            await _notify.NotifyAsync(customer.AdminEmail, TemplateDefinitionNames.General.ApprovingCustomerEmail, notifyModel);
            return RedirectToAction("Index");

        }
        public IActionResult DeactivateCustomer(int Id)
        {
            //TODO ADD THE CODE AIRCON-15
            _customerAdminService.DeactiveCustomer(Id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ActivateCustomer(int Id)
        {
            _customerAdminService.ActivateCustomer(Id);
            var customer = _customerAdminService.GetCustomer(Id);
            var notifyModel = new NotifyEmailModel { displayname = string.Format("{0}", customer.AdminName) };
            await _notify.NotifyAsync(customer.AdminEmail, TemplateDefinitionNames.General.ActivatingCustomerEmail, notifyModel);
            return RedirectToAction("Index");
        }

        public IActionResult EditCustomerProfilePartial(int Id)
        {
            CustomerAdminViewModel editCustomerProfile = _customerAdminService.GetCustomer(Id).ToViewModel();
            return PartialView("EditCustomerProfilePartial", editCustomerProfile);
        }

        public IActionResult EditCustomerOpportunityPartial(int Id)
        {
            CustomerOpportunityAdminViewModel editCustomerOpportunity = _customerAdminService.GetCustomerOpportunity(Id).ToViewModel();
            return PartialView("EditCustomerOpportunityPartial", editCustomerOpportunity);
        }

        public IActionResult SaveCustomerProfile(CustomerAdminViewModel customerAdminViewModel)
        {
            CustomerAdminViewModel customerProfile= _customerAdminService.UpdateCustomer(customerAdminViewModel.ToModel()).ToViewModel();
            return RedirectToAction("Index");
        }

        public IActionResult SaveCustomerOpportunity(CustomerOpportunityAdminViewModel customerOpportunityAdminViewModel)
        {
            CustomerOpportunityAdminViewModel customerOpportunity = _customerAdminService.UpdateCustomerOpportunity(customerOpportunityAdminViewModel.ToModel()).ToViewModel();
            return RedirectToAction("Index");
        }
    }
}