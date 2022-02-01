using Aircon.Areas.Admin.Models;
using Aircon.Areas.Admin.Models.User;
using Aircon.Business.Models.Shared;
using Aircon.Business.Services.Admin;
using Aircon.Business.Services.Shared;
using Aircon.Core.Data;
using Aircon.Data.Entities;
using Aircon.Data.Enums;
using Aircon.Data.Helper;
using Aircon.Extensions;
using Aircon.ViewModels.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vg.Common.Notification.Message;

namespace Aircon.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IAdminUserService _adminUserService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly UserManager<User> _userManager;
        private readonly INotify _notify;
        public UserController(IAdminUserService adminUserService, IGenericAttributeService genericAttributeService, UserManager<User> userManager, INotify notify)
        {
            _adminUserService = adminUserService;
            _genericAttributeService = genericAttributeService;
            _userManager = userManager;
            _notify = notify;
        }
        public async Task<IActionResult> Index(AdminUserListViewModel adminUserListViewModel)
        {
            var searchText = SearchText();
            int recordCountUserQueue = await _genericAttributeService.GetAttributeAsync<UserModel, int>(HttpContextHelper.UserId.Value, CardGridSetting.AdminUser.UserQueue, 5);
            int recordCountCustomersQueue = await _genericAttributeService.GetAttributeAsync<UserModel, int>(HttpContextHelper.UserId.Value, CardGridSetting.AdminUser.Customers, 5);
            adminUserListViewModel.QueueUsers = _adminUserService.GetQueueUsers( UserStatus.AwaitingReview,searchText, recordCountUserQueue).Select(x => x.ToViewModel()).ToList();
            adminUserListViewModel.AdminUsers = _adminUserService.GetAdminUsers( true, true,searchText,recordCountCustomersQueue).Select(x => x.ToViewModel()).ToList();
            return View(adminUserListViewModel);
        }
        public async Task<IActionResult> CustomersUsersPartial (bool isActive, bool isAll)
        {
            List<UserViewModel> customerUsers = new List<UserViewModel>();
            var searchText = SearchText();
            int recordCountCustomersQueue = await _genericAttributeService.GetAttributeAsync<UserModel, int>(HttpContextHelper.UserId.Value, CardGridSetting.AdminUser.Customers, 5);
            customerUsers = _adminUserService.GetAdminUsers(isActive, isAll, searchText,recordCountCustomersQueue).Select(x => x.ToViewModel()).ToList();
            return PartialView("_AdminUsersContainer", customerUsers);
        }

        public async Task<IActionResult> QueueUsersPartialAsync (UserStatus userStatus)
        {
            var searchText = SearchText();
            int recordCountUserQueue = await _genericAttributeService.GetAttributeAsync<UserModel, int>(HttpContextHelper.UserId.Value, CardGridSetting.AdminUser.UserQueue, 5);
            List<UserViewModel> queueUsers = _adminUserService.GetQueueUsers(userStatus, searchText, recordCountUserQueue).Select(x => x.ToViewModel()).OrderBy(x => x.FirstName).ToList();
            return PartialView("_QueueAdminUsersContainer", queueUsers);
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
        public async Task<IActionResult> UpdateUserPost(UserViewModel viewAdminUserViewModel)
        {
            var model = viewAdminUserViewModel.ToModel();
            model = await _adminUserService.UpdateUser(model);
            var webModel = model.ToViewModel();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ApproveUser(int Id)
        {
            _adminUserService.ApprovingUser(Id);
            var user = _adminUserService.GetUser(Id);
            var notifyModel = new NotifyEmailModel { displayname = string.Format("{0} {1}", user.FirstName, user.LastName)};
            await _notify.NotifyAsync(user.Email, TemplateDefinitionNames.General.ApprovingUserEmail, notifyModel);
            return RedirectToAction("Index");
        }
        public IActionResult DenyUser(int Id)
        {
            _adminUserService.DenyUser(Id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ActivatingUser(int Id)
        {
            var user = _adminUserService.ActivatingUser(Id);
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
            _adminUserService.DeactivatingUser(Id);
            return RedirectToAction("Index");
        }
        public IActionResult EditAdminUsersPartial(int Id)
        {
            var viewModel = _adminUserService.GetUser(Id).ToViewModel();
            return PartialView("EditAdminUsersPartial", viewModel);
        }
        public IActionResult AddAdminUserPartial()
        {
            UserViewModel addUserView = new UserViewModel
            {
                UserStatus = UserStatus.AwaitingReview,
                IsApproved = false,
                IsEmployee = false,
                IsActive = false,
            };
            return PartialView("AddAdminUserPartial", addUserView);
        }

        [HttpPost]
        public async Task<IActionResult> SaveUser(UserViewModel addUserView)
        {
            if (addUserView.Id == 0)
            {
                var result = await _adminUserService.AddUser(addUserView.ToModel());
            }
            else
            {
                var saveUser = await _adminUserService.UpdateUser(addUserView.ToModel());
            }
            return RedirectToAction("Index");
        }
    }
}
