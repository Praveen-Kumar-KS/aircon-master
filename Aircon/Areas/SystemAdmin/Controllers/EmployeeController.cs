using Aircon.Areas.SystemAdmin.Models.Employee;
using Aircon.Business.Services.SystemAdmin;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aircon.Extensions;
using Aircon.Data.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Aircon.Areas.Customer.Models.User;
using Aircon.ViewModels.Shared;
using Aircon.Business.Models.Shared;
using Aircon.Business.Services.Shared;

namespace Aircon.Areas.SystemAdmin.Controllers
{
    public class EmployeeController : BaseSystemAdminController
    {
        private readonly IEmployeeUserService _employeeUserService;
        private readonly IGenericAttributeService _genericAttributeService;

        public EmployeeController(IEmployeeUserService employeeUserService, IGenericAttributeService genericAttributeService)
        {
            _employeeUserService = employeeUserService;
            _genericAttributeService = genericAttributeService;
        }
        public async Task<IActionResult> Index(EmployeeUserListViewModel employeeUserListViewModel)
        {
            //TODO - ADD PERMISSION CHECK FOR SYSTEM ADMIN ACCESS

            var searchText = SearchText();
            int recordCountEmployee = await _genericAttributeService.GetAttributeAsync<UserModel, int>(HttpContextHelper.UserId.Value, CardGridSetting.Employee.SystemSettingEmployee, 5);
            employeeUserListViewModel.Users = _employeeUserService.GetEmployees( true, true,searchText, recordCountEmployee).Select(x => x.ToViewModel()).ToList();
            return View(employeeUserListViewModel);
        }

        public async Task<IActionResult> EmployeeUsersPartial (bool isActive, bool isAll)
        {
            //TODO - ADD PERMISSION CHECK FOR SYSTEM ADMIN ACCESS
            var searchText = SearchText();
            List<UserViewModel> employeeUsers = new List<UserViewModel>();
            int recordCountEmployee = await _genericAttributeService.GetAttributeAsync<UserModel, int>(HttpContextHelper.UserId.Value, CardGridSetting.Employee.SystemSettingEmployee, 5);
            employeeUsers = _employeeUserService.GetEmployees(isActive, isAll,searchText, recordCountEmployee).Select(x => x.ToViewModel()).ToList();
            return PartialView("_EmployeeUsersContainer", employeeUsers);
        }

        public IActionResult AllEmployeeUsers()
        {
            return RedirectToAction("EmployeeUsersPartial", new { isActive = true, isAll = true });
        }

        public IActionResult ActiveEmployeeUsers()
        {
            return RedirectToAction("EmployeeUsersPartial", new { isActive = true, isAll = false });
        }
        public IActionResult InActiveEmployeeUsers()
        {
            return RedirectToAction("EmployeeUsersPartial", new { isActive = false, isAll = false });
        }
        public IActionResult EditEmployeePartial(int Id)
        {
            UserViewModel employeeUserViewModel = _employeeUserService.GetEmployee(Id).ToViewModel();
            return PartialView("EditEmployeePartial", employeeUserViewModel);
        }
        public IActionResult ActivateEmployeeUser(int Id)
        {
            _employeeUserService.ActivateEmployeeUser(Id);
            return RedirectToAction("Index");
        }

        public IActionResult DeactivateEmployeeUser(int Id)
        {
            _employeeUserService.DeactivateEmployeeUser(Id);
            return RedirectToAction("Index");
        }

        public IActionResult AddEmployeePartial()
        {
            UserViewModel addemployee = new UserViewModel
            {
                IsApproved = true,
                IsEmployee = true,
                IsActive = true,
            };
            return PartialView("AddEmployeePartial", addemployee);
        }
        [HttpPost]
        public async Task<IActionResult> SaveUser(UserViewModel addUserView)
        {
            if(addUserView.Id == 0)
            {
                var result = await _employeeUserService.AddUser(addUserView.ToModel());
            }
            else
            {
                var result = await _employeeUserService.UpdateUser(addUserView.ToModel());
            }
            return RedirectToAction("Index");
        }
    }
}