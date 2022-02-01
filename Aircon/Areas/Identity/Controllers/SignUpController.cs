using Aircon.Business.Services;
using Aircon.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Aircon.Data.Helper;
using System.Security.Claims;
using Aircon.Extensions;
using Aircon.Core;
using Aircon.ViewModels.Shared;
using Aircon.Business.Models.Identity.SignUp;
using Aircon.Business.Services.Identity;
using Aircon.Areas.Identity.Models.SignUp;
using Vg.Common.Notification.Message;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Aircon.Business.Models.Shared;
using Aircon.Core.Data;
using Aircon.Data.Security;
using Aircon.Business.Services.Customer;

namespace Aircon.Areas.Identity.Controllers
{

    [AllowAnonymous]
    public class SignUpController : BaseIdentityController
    {
        
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<SignUpController> _logger;
        private readonly ISignUpService _signUpService;
        private readonly INotify _notify;

        public SignUpController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender,
            ILogger<SignUpController> logger,
            ISignUpService signUpService,
            INotify notify)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _signUpService = signUpService;
            _notify = notify;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUpPost(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    var userCreated = await _userManager.FindByNameAsync(model.Email);
                    var userCreatedRole = await _userManager.AddToRoleAsync(userCreated, RoleSystemName.User);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("UserDetails");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    ErrorNotification(ModelState);
                }
            }
            return View("Index");
        }
        public IActionResult UserDetails()
        {
            var userName = HttpContextHelper.Current.User.FindFirst(ClaimTypes.Name).Value;
            var userModel = _signUpService.GetUserByName(userName);
            CustomerModel customer;
            try
            {
                 customer = _signUpService.CheckCustomerDomain(userName);
            }catch (AppException ex)
            {
                customer = new CustomerModel { CompanyName = string.Empty, Id = 0 };
            }
            var model = new UserDetailViewModel { UserId = userModel.Id, CompanyId = customer.Id, CompanyName = customer.CompanyName , Avatar = new Business.Media.PictureModel()};
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UserDetailsPostAsync(UserDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
               var result = _signUpService.UpdateSignUpUser(model.ToModel());
                var user = await _userManager.FindByIdAsync(model.UserId.ToString());
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code, returnUrl = string.Empty }, Request.Scheme);
                var notifyModel = new NotifyForgotPasswordModel { displayname = string.Format("{0} {1}", user.FirstName, user.LastName), link = callbackUrl };
                await _notify.NotifyAsync(user.Email, TemplateDefinitionNames.General.SignUpWelcomeEmail, notifyModel);
                if (model.CompanyId > 0)
                    return RedirectToAction("AwaitngApproval","SignUp",new { Area = "Identity"});
                else
                    return RedirectToAction("CompanyProfile", "SignUp", new { Area = "Identity" });
            }
            else
            {
                ErrorNotification(ModelState);
            }
            return View("UserDetails",model);
        }

        public async Task<IActionResult> AwaitngApproval()
        {
            await _signInManager.SignOutAsync();
            return View();
        }
        public IActionResult CompanyProfile()
        {
            return View();
        }
        [HttpPost]
        public IActionResult NewCompanyProfilePost(SignUpCompanyProfileViewModel viewModel)
        {
            var result = _signUpService.AddCustomerOpportunity(viewModel.ToModel());
            return RedirectToAction("CompanyAddress", new { CustomerOpportunityId = result.Id });
        }

        public IActionResult CompanyAddress(int CustomerOpportunityId )
        {
            CustomerOpportunityAddressViewModel model = new CustomerOpportunityAddressViewModel { CustomerOpportunityId = CustomerOpportunityId, Logo = new Business.Media.PictureModel() };
            return View(model);
        }
        [HttpPost]
        public IActionResult CompanyAddressPost(CustomerOpportunityAddressViewModel viewModel)
        {
            var result = _signUpService.UpdateAddressForCustomerOpportunity(viewModel.ToModel());
            return RedirectToAction("CompanyOtherAddress",new { CustomerOpportunityId = result.CustomerOpportunityId});
        }
        public IActionResult CompanyOtherAddress(int CustomerOpportunityId)
        {
            CustomerOpportunityAddressViewModel model = new CustomerOpportunityAddressViewModel { CustomerOpportunityId = CustomerOpportunityId };
            return View(model);
        }
        [HttpPost]
        public IActionResult CompanyOtherAddressPost(CustomerOpportunityAddressViewModel viewModel, string Action)
        {
            if (Action.Equals("AddAddress") || Action.Equals("Next"))
            {
                var result = _signUpService.AddAddressForCustomerOpportunity(viewModel.ToModel());
            }
            if (Action.Equals("DeleteAddress"))
            {
                var result = _signUpService.DeleteAddressForCustomerOpportunity(viewModel.ToModel());
            }
            if (Action.Equals("Skip") || Action.Equals("Next"))
            {
                return RedirectToAction("SubscriptionPlan", new { CustomerOpportunityId = viewModel.CustomerOpportunityId });
            }

            return RedirectToAction("CompanyOtherAddress", new { CustomerOpportunityId = viewModel.CustomerOpportunityId });
        }
        public IActionResult SubscriptionPlan(int CustomerOpportunityId )
        {
            SubscriptionPageViewModel subscriptionPageModel = new SubscriptionPageViewModel { CustomerOpportunityId = CustomerOpportunityId };
            return View(subscriptionPageModel);
        }
        [HttpPost]
        public IActionResult SubscriptionPlanPost(SubscriptionPageViewModel viewModel)
        {
            var result = _signUpService.AddSubscriptionPlan(viewModel.ToModel());
            return RedirectToAction("SetupPaymentMethod", new { CustomerOpportunityId = viewModel.CustomerOpportunityId });
        }
        public IActionResult SetupPaymentMethod(int CustomerOpportunityId)
        {
            var model = new OpportunityPaymentMethodViewModel { CustomerOpportunityId = CustomerOpportunityId };
            return View(model);
        }
        [HttpPost]
        public IActionResult SetupPaymentMethodPost(OpportunityPaymentMethodViewModel viewModel, string Action)
        {
            if (Action.Equals("AddAddress") || Action.Equals("Next"))
            {
                var result = _signUpService.AddOpportunityPaymentMethod(viewModel.ToModel());
            }
            if (Action.Equals("DeleteAddress"))
            {
                var result = _signUpService.DeleteOpportunityPaymentMethod(viewModel.ToModel());
            }
            if (Action.Equals("Skip") || Action.Equals("Next"))
            {
                return RedirectToAction("TermsAndConditions", new { CustomerOpportunityId = viewModel.CustomerOpportunityId });
            }
            return RedirectToAction("SetupPaymentMethod", new { CustomerOpportunityId = viewModel.CustomerOpportunityId });
        }
        public IActionResult TermsAndConditions(int CustomerOpportunityId)
        {
            var model = new TermsAndConditionsViewModel { CustomerOpportunityId = CustomerOpportunityId };
            return View(model);
        }
        [HttpPost]
        public IActionResult TermsAndConditionsPost(TermsAndConditionsViewModel viewModel)
        {

            var result = _signUpService.UpdateTermsAndConditions(viewModel.ToModel());
            return RedirectToAction("GettingThingsUp");
        }
        public async Task<IActionResult> GettingThingsUp()
        {
            await _signInManager.SignOutAsync();
            return View();
        }

    }
}
