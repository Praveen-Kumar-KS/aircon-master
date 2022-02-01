using Aircon.Areas.Customer.Models;
using Aircon.Areas.Customer.Models.Company;
using Aircon.Business.Services.Customer;
using Aircon.Data.Entities;
using Aircon.Data.Helper;
using Aircon.Data.Security;
using Aircon.Extensions;
using Aircon.Framework.Security;
using Aircon.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Aircon.Areas.Customer.Controllers
{
    [PermissionAuthorize(PermissionSystemName.CompanySetting)]
    public class CompanyController : BaseCustomerController
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        
        public IActionResult Index()
        {
            var customerId = HttpContextHelper.CustomerId;
            if (!customerId.HasValue)
                return AccessDeniedView();
            int id = customerId.HasValue ? customerId.Value : 0;
            var viewModel = new CompanyViewModel();
            viewModel.CompanyProfile = _companyService.GetCompanyProfile(id).ToViewModel();
            viewModel.PaymentMethods = _companyService.GetPaymentMethods(id).Select(x => x.ToViewModel()).ToList();
            viewModel.CustomerAddresses = _companyService.GetCustomerAddresses(id).Select(x => x.ToViewModel()).ToList();
            ViewBag.AddressTypes = LookupHelper.GetAllLookupItems<AddressType>();
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult SaveData(CompanyViewModel companyViewModel)
        {
            var customerId = HttpContextHelper.CustomerId;
            if (!customerId.HasValue)
                return AccessDeniedView();
            int id = customerId.HasValue ? customerId.Value : 0;
            var viewModel = _companyService.SaveCompanyProfile(companyViewModel.CompanyProfile.ToModel());
            return RedirectToAction("Index");
        }

        public IActionResult EditAddressPartial(int Id)
        {
            var customerId = HttpContextHelper.CustomerId;
            if (!customerId.HasValue)
                return AccessDeniedView();
            int id = customerId.HasValue ? customerId.Value : 0;
            var viewModel = _companyService.GetCustomerAddress(Id).ToViewModel();
            return PartialView("EditAddressPartial", viewModel);
        }
        public IActionResult AddAddressPartial()
        {
            var customerId = HttpContextHelper.CustomerId;
            if (!customerId.HasValue)
                return AccessDeniedView();
            int id = customerId.HasValue ? customerId.Value : 0;
            CustomerAddressViewModel customerAddressViewModel = new CustomerAddressViewModel
            {
                CustomerId = id
            };
            return PartialView("AddAddressPartial", customerAddressViewModel);
        }

        [HttpPost]
        public IActionResult SaveAddress(CustomerAddressViewModel customerAddressViewModel)
        {
            var customerId = HttpContextHelper.CustomerId;
            if (!customerId.HasValue)
                return AccessDeniedView();
            CustomerAddressViewModel SaveAddress = _companyService.UpdateAddress(customerAddressViewModel.ToModel()).ToViewModel();
            return RedirectToAction("Index");
        }
        
        public IActionResult AddAddress(CustomerAddressViewModel customerAddress)
        {
           var customerId = HttpContextHelper.CustomerId;
            if (!customerId.HasValue)
                return AccessDeniedView();
            customerAddress.CustomerId = Convert.ToInt32(customerId);           
            var viewmodel = _companyService.AddAddress(customerAddress.ToModel()).ToViewModel();
            return RedirectToAction("Index");

        }
        public IActionResult EditPaymentMethodPartial(int Id)
        {
            var customerId = HttpContextHelper.CustomerId;
            if (!customerId.HasValue)
                return AccessDeniedView();
            int id = customerId.HasValue ? customerId.Value : 0;
            var viewModel = _companyService.GetPaymentMethod(Id).ToViewModel();
            return PartialView("EditPaymentMethodPartial", viewModel);
        }
        public IActionResult AddPaymentMethodPartial()
        {
            var customerId = HttpContextHelper.CustomerId;
            if (!customerId.HasValue)
                return AccessDeniedView();
            int id = customerId.HasValue ? customerId.Value : 0;
            PaymentMethodViewModel paymentMethodViewModel = new PaymentMethodViewModel
            {
                CustomerId = id,
                IsBillingAddressSameAsCompanyAddress = true
            };
            return PartialView("AddPaymentMethodPartial", paymentMethodViewModel);
        }

        [HttpPost]
        public IActionResult SavePaymentMethod(PaymentMethodViewModel paymentMethodViewModel)
        {
            var customerId = HttpContextHelper.CustomerId;
            if (!customerId.HasValue)
                return AccessDeniedView();
            PaymentMethodViewModel SavePayment = _companyService.UpdatePaymentMethod(paymentMethodViewModel.ToModel()).ToViewModel();
            return RedirectToAction("Index");
        }
        public IActionResult AddPaymentMethod(PaymentMethodViewModel paymentMethodViewModel)
        {
            var customerId = HttpContextHelper.CustomerId;
            if (!customerId.HasValue)
                return AccessDeniedView();
            var viewModel = _companyService.AddPaymentMethod(paymentMethodViewModel.ToModel()).ToViewModel();
            return RedirectToAction("Index");
        }
        public IActionResult DeleteAddress(int Id)
        {
            _companyService.DeleteAddress(Id);
            return RedirectToAction("Index");
        }

    }
}
