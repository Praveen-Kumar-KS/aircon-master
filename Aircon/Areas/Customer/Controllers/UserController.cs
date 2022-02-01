using Aircon.Areas.Customer.Models;
using Aircon.Areas.Customer.Models.User;
using Aircon.Business.Models.Shared;
using Aircon.Business.Services.Customer;
using Aircon.Business.Services.Shared;
using Aircon.Core.Data;
using Aircon.Data.Entities;
using Aircon.Data.Enums;
using Aircon.Data.Helper;
using Aircon.Data.Security;
using Aircon.Extensions;
using Aircon.Framework.Security;
using Aircon.ViewModels.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vg.Common.Notification.Message;

namespace Aircon.Areas.Customer.Controllers
{
    [PermissionAuthorize(PermissionSystemName.CompanySetting)]
    public class UserController : BaseCustomerController
    {
        private readonly ICustomerUserService _customerUserService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly UserManager<User> _userManager;
        private readonly INotify _notify;

        public UserController(ICustomerUserService customerUserService, IGenericAttributeService genericAttributeService, UserManager<User> userManager, INotify notify)
        {
            _customerUserService = customerUserService;
            _genericAttributeService = genericAttributeService;
            _userManager = userManager;
            _notify = notify;
        }
        public async Task<IActionResult> Index(CustomerUserListViewModel customerUserListViewModel)
        {
            var customerId = HttpContextHelper.CustomerId;
            if (!customerId.HasValue)
                return AccessDeniedView();
            int id = customerId.HasValue ? customerId.Value : 0;
            var searchText = SearchText();
            int recordCountUserQueue = await _genericAttributeService.GetAttributeAsync<UserModel, int>(HttpContextHelper.UserId.Value, CardGridSetting.CustomerSettings.UserQueue, 5);
            int recordCountCustomersQueue = await _genericAttributeService.GetAttributeAsync<UserModel, int>(HttpContextHelper.UserId.Value, CardGridSetting.CustomerSettings.Customers, 5);
            customerUserListViewModel.QueueUsers = _customerUserService.GetQueueUsers(id, UserStatus.AwaitingReview, searchText, recordCountUserQueue).Select(x => x.ToViewModel()).ToList();
            customerUserListViewModel.CustomerUsers = _customerUserService.GetCustomertUsers(id, true, true, searchText, recordCountCustomersQueue).Select(x => x.ToViewModel()).ToList();
            return View(customerUserListViewModel);
        }


        public IActionResult AddUserDetail()
        {
            return View();
        }
        public async Task<IActionResult> UpdateUserPost(UserViewModel viewCustomerUserViewModel)
        {
            var model = viewCustomerUserViewModel.ToModel();
            model = await _customerUserService.UpdateUser(model);
            var webModel = model.ToViewModel();
            return RedirectToAction("Index");
        }
        public IActionResult ApproveUser(int Id)
        {
            _customerUserService.ApprovingUser(Id);
            return RedirectToAction("Index");
        }
        public IActionResult DenyUser(int Id)
        {
            _customerUserService.DenyUser(Id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ActivatingUser(int Id)
        {
            var user = _customerUserService.ActivatingUser(Id);
            if (!user.EmailConfirmed)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { Area = "Identity", userId = user.Id, code = code, returnUrl = string.Empty }, Request.Scheme);
                var notifyModel = new NotifyForgotPasswordModel { displayname = string.Format("{0} {1}", user.FirstName, user.LastName), link = callbackUrl };
                await _notify.NotifyAsync(user.Email, TemplateDefinitionNames.General.SignUpWelcomeEmail, notifyModel);
            }
            return RedirectToAction("Index");
        }
        public IActionResult DeactivatingUser(int Id)
        {
            _customerUserService.DeactivatingUser(Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CustomersUsersPartial(bool isActive, bool isAll)
        {
            var customerId = HttpContextHelper.CustomerId;
            if (!customerId.HasValue)
                return AccessDeniedView();
            int id = customerId.HasValue ? customerId.Value : 0;
            List<UserViewModel> customerUsers = new List<UserViewModel>();
            var searchText = SearchText();
            int recordCountCustomersQueue = await _genericAttributeService.GetAttributeAsync<UserModel, int>(HttpContextHelper.UserId.Value, CardGridSetting.CustomerSettings.Customers, 5);
            customerUsers = _customerUserService.GetCustomertUsers(id, isActive, isAll, searchText, recordCountCustomersQueue).Select(x => x.ToViewModel()).ToList();
            return PartialView("_CustomerUsersContainer", customerUsers);
        }

        public async Task<IActionResult> QueueUsersPartial(UserStatus userStatus)
        {
            var customerId = HttpContextHelper.CustomerId;
            if (!customerId.HasValue)
                return AccessDeniedView();
            int id = customerId.HasValue ? customerId.Value : 0;
            var searchText = SearchText();
            int recordCountUserQueue = await _genericAttributeService.GetAttributeAsync<UserModel, int>(HttpContextHelper.UserId.Value, CardGridSetting.CustomerSettings.UserQueue, 5);
            List<UserViewModel> queueUsers = _customerUserService.GetQueueUsers(id, userStatus, searchText, recordCountUserQueue).Select(x => x.ToViewModel()).OrderBy(x => x.FirstName).ToList();
            return PartialView("_QueueUsersContainer", queueUsers);
        }
        public IActionResult AwaitingReviewQueueUsers()
        {
            return RedirectToAction("QueueUsersPartial", new { userStatus = UserStatus.AwaitingReview });
        }
        public IActionResult DeniedQueueUsers()
        {
            return RedirectToAction("QueueUsersPartial", new { userStatus = UserStatus.Denied });
        }

        public IActionResult AllCustomerUsers()
        {
            return RedirectToAction("CustomersUsersPartial", new { isActive = true, isAll = true });
        }

        public IActionResult ActiveCustomerUsers()
        {
            return RedirectToAction("CustomersUsersPartial", new { isActive = true, isAll = false });
        }
        public IActionResult InActiveCustomerUsers()
        {
            return RedirectToAction("CustomersUsersPartial", new { isActive = false, isAll = false });
        }
        public IActionResult EditUsersPartial(int Id)
        {
            var customerId = HttpContextHelper.CustomerId;
            if (!customerId.HasValue)
                return AccessDeniedView();
            int id = customerId.HasValue ? customerId.Value : 0;
            var viewModel = _customerUserService.GetUser(Id).ToViewModel();
            return PartialView("EditUsersPartial", viewModel);
        }
        public IActionResult AddUserPartial()
        {
            var customerId = HttpContextHelper.CustomerId;
            if (!customerId.HasValue)
                return AccessDeniedView();
            int id = customerId.HasValue ? customerId.Value : 0;
            UserViewModel addUserView = new UserViewModel
            {
                UserStatus = UserStatus.AwaitingReview,
                IsApproved = false,
                IsEmployee = false,
                IsActive = false,
                CustomerId = id
            };
            return PartialView("AddUserPartial", addUserView);
        }

        [HttpGet]
        public JsonResult IsValidEmailCheck(string Email, int Id, int CustomerId)
        {
            if (Id > 0)
                return Json(true);
            var isFreeEmail = _customerUserService.IsFreeEmail(Email);

            if (isFreeEmail)
                return Json($"This is not a business email");

            var isUniqueEmail = _customerUserService.IsUniqueEmail(Email);

            if (isUniqueEmail)
                return Json($"This email is already taken");
            bool IsExist = _customerUserService.CheckDomain(Email,CustomerId);
            if (IsExist)
                return Json($"Domain is already taken");

            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> SaveUser(UserViewModel addUserView)
        {
            if (addUserView.Id == 0)
            {
                var result = await _customerUserService.AddUser(addUserView.ToModel());
            }
            else
            {
                var model = addUserView.ToModel();
                var saveUser = await _customerUserService.UpdateUser(model);
            }
            return RedirectToAction("Index");
        }
    }
}
