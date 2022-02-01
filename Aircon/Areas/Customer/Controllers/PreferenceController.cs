using Aircon.Areas.Customer.Models;
using Aircon.Business.Services.Customer;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Aircon.Extensions;
using Aircon.Data.Helper;
using Aircon.Business.Services.Shared;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vg.Common.Notification.Message;
using System.Threading.Tasks;
using System;
using Aircon.Business.Models.Shared;

namespace Aircon.Areas.Customer.Controllers
{
    public class PreferenceController : BaseCustomerController
    {
        private readonly IPreferenceService _preferenceService;
        private readonly ICountryService _countryService;
        private readonly IGenericAttributeService _genericAttributeService;

        public PreferenceController(IPreferenceService preferenceService, ICountryService countryService, IGenericAttributeService genericAttributeService)
        {
            _preferenceService = preferenceService;
            _countryService = countryService;
            _genericAttributeService = genericAttributeService;
        }
        public IActionResult Index()
        {
            var userId = HttpContextHelper.UserId;
            if (!userId.HasValue)
                return AccessDeniedView();
            SystemSettingPageViewModel viewModel = new SystemSettingPageViewModel();
            viewModel.UserPreferenceViewModel = _preferenceService.GetCurrentPreference(userId.Value).ToViewModel();
            viewModel.UserUpdateNotificationSettingViewModel = _preferenceService.GetCurrentNotificationSettings(userId.Value).ToViewModel();
            ViewBag.CountryList = _countryService.GetCountryList().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            ViewBag.TimeZoneList = _countryService.GetTimeZoneList(viewModel.UserPreferenceViewModel.CountryId).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetTimeZoneList(int countryId)
        {
            return new JsonResult(_countryService.GetTimeZoneList(countryId).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList());
        }
        [HttpPost]
        public IActionResult SaveData(SystemSettingPageViewModel systemSettingPageViewModel)
        {
            var result = _preferenceService.SaveCurrentNotificationSettings(systemSettingPageViewModel.UserUpdateNotificationSettingViewModel.ToModel());
            var userPrefrece = _preferenceService.SaveCurrentPreference(systemSettingPageViewModel.UserPreferenceViewModel.ToModel());
            return RedirectToAction("Index");
        }

        [HttpPost]
        public virtual async Task<IActionResult> SavePreference(string name, bool value)
        {
            //permission validation is not required here
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            await _genericAttributeService.SaveAttributeAsync(new UserModel { Id = HttpContextHelper.UserId.Value }, name, value);

            return Json(new
            {
                Result = true
            });
        }

        [HttpPost]
        public virtual async Task<IActionResult> SaveIntPreference(string name, int value)
        {
            //permission validation is not required here
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            await _genericAttributeService.SaveAttributeAsync(new UserModel { Id = HttpContextHelper.UserId.Value }, name, value);

            return Json(new
            {
                Result = true
            });
        }

    }
}
