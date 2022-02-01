//using Aircon.Core.Data;
//using Aircon.Data;
//using Aircon.Data.Entities;
//using Aircon.Data.Enums;
//using Aircon.Data.Security;
//using Aircon.SampleData.Bogus;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Aircon.Extensions
//{
//    public static class SeedExtensions
//    {
//        public static async Task CheckRolesAndPermissionsAsync(this IServiceProvider serviceProvider)
//        {
//            using (var scope = serviceProvider.CreateScope())
//            {
//                var services = scope.ServiceProvider;
//                var env = services.GetRequiredService<IWebHostEnvironment>();
//                var _airconDBContext = services.GetRequiredService<AirconDbContext>();
//                var _permissionProvider = services.GetRequiredService<IPermissionProvider>();
//                var _roleManager = services.GetRequiredService<RoleManager<Role>>();
//                var _logger = services.GetRequiredService<ILogger<Startup>>();

//                var permissions = _permissionProvider.GetPermissions();
//                foreach (var permission in permissions)
//                {
//                    var result = _airconDBContext.Permissions.Where(x => x.SystemName == permission.SystemName).SingleOrDefault();
//                    if(result == null)
//                    {
//                        _airconDBContext.Permissions.Add(permission);
//                    }
//                    _airconDBContext.SaveChanges();
//                }
//                var roles = _permissionProvider.GetRoles();
//                foreach (var role in roles)
//                {
//                    var result = await _roleManager.CheckAddNewRoleAsync(role);
//                }
//                _airconDBContext.SaveChanges();
//                foreach (var role in roles)
//                {
//                    var result = _airconDBContext.Roles.Where(x => x.Name == role.Name).Include(role => role.RolePermissions).SingleOrDefault();
//                    if (result != null)
//                    {
//                        var defaultPermission = _permissionProvider.GetDefaultPermissions().Where(x => x.RoleSystemName == role.Name).SingleOrDefault();
//                        foreach(var permission2 in defaultPermission.Permissions)
//                        {
//                            var rolePermission = result.RolePermissions.Where(x => x.Permission.SystemName == permission2.SystemName).SingleOrDefault();
//                            if(rolePermission == null)
//                            {
//                               var permission1 = _airconDBContext.Permissions.Where(x => x.SystemName == permission2.SystemName).SingleOrDefault();
//                               result.RolePermissions.Add(new RolePermission {  Permission = permission1, RoleId = result.Id });
//                            }
//                        }
//                        _airconDBContext.Roles.Update(result);
//                    }
//                    await _airconDBContext.SaveChangesAsync();
//                }
//                // Load conuntry and timezones..

//                try
//                {
//                    string path = Path.Combine(env.WebRootPath, "assets", "data", "countries_with_timeZones.json");//  "/assets/data/countries_with_timeZones.json");
//                    string data = File.ReadAllText(path);
//                    List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(data);
//                    foreach (var country in countries)
//                    {
//                        var result = _airconDBContext.Countries.Where(x => x.CountryName == country.CountryName).SingleOrDefault();
//                        if (result == null)
//                        {
//                            _airconDBContext.Countries.Add(country);
//                        }
//                    }
//                    await _airconDBContext.SaveChangesAsync();
//                }
//                catch(Exception ex)
//                {
//                    _logger.LogError(ex.ToString());
//                }

//                try
//                {
//                    if(!(_airconDBContext.FreeDomains.ToList().Count > 100))
//                    {
//                        string path = Path.Combine(env.WebRootPath, "assets", "data", "freemail.txt");//  "/assets/data/countries_with_timeZones.json");
//                        string line;
//                        StreamReader file = new StreamReader(path);
//                        if ((line = file.ReadLine()) != null)
//                        {
//                            while ((line = file.ReadLine()) != null)
//                            {
//                                _airconDBContext.FreeDomains.Add(new FreeDomain { DomainName = line });
//                            }
//                        }
//                    }
//                    await _airconDBContext.SaveChangesAsync();
//                }
//                catch (Exception ex)
//                {
//                    _logger.LogError(ex.ToString());
//                }

//                // Default preferences and Notification settings
//                try
//                {

//                    var systemSetting = _airconDBContext.SystemSettings.Include(x => x.DefaultNotificationSettings).Include(x => x.DefaultPreference).FirstOrDefault();
//                    var defaultTimezoneValue = _airconDBContext.Countries.Where(x => x.IsoAlpha3 == "USA").Include(x => x.WindowsTimeZones).Select(x => new {
//                        Id = x.Id,
//                        TimeZoneId = x.WindowsTimeZones.Where(x => x.Name == "Central Standard Time").SingleOrDefault().Id
//                    }).SingleOrDefault();

//                    if (systemSetting != null)
//                    {
//                            if (systemSetting.DefaultPreference  == null)
//                            {
//                                systemSetting.DefaultPreference
//                                    = new Preference
//                                    {
//                                        LandingPage = LandingPage.Workflow,
//                                        MeasurementUnit = MeasurementUnit.Metric,
//                                        CountryId = defaultTimezoneValue.Id,
//                                        WindowsTimeZoneId = defaultTimezoneValue.TimeZoneId
//                                    };
//                            }
//                            if (systemSetting.DefaultNotificationSettings.FirstOrDefault() == null)
//                            {
//                                systemSetting.DefaultNotificationSettings = SeedDefaultNotificationSetting.GetDefaultNotificationSettings();
//                            }
//                            _airconDBContext.SystemSettings.Update(systemSetting);

//                    }
//                    else
//                    {
//                        // SystemSetting is null
//                        systemSetting = new SystemSetting
//                        {
//                            DefaultPreference =  new Preference
//                                        {
//                                            LandingPage = LandingPage.Workflow,
//                                            MeasurementUnit = MeasurementUnit.Metric,
//                                            CountryId = defaultTimezoneValue.Id,
//                                            WindowsTimeZoneId = defaultTimezoneValue.TimeZoneId
//                                        },
//                            DefaultNotificationSettings = SeedDefaultNotificationSetting.GetDefaultNotificationSettings()
//                        };

//                        _airconDBContext.SystemSettings.Add(systemSetting);
//                    }
//                    await _airconDBContext.SaveChangesAsync();

//                }
//                catch (Exception ex)
//                {
//                    _logger.LogError(ex.ToString());
//                }

//                foreach(var type in _airconDBContext.SubscriptionTypes)
//                {
//                    type.Active = false;
//                    _airconDBContext.SubscriptionTypes.Update(type);
//                }
//                _airconDBContext.SaveChanges();
//                var subscription1 = new SubscriptionType
//                {
//                    IsPopular = true,
//                    Line1 = "2 YEARS",
//                    Line2 = "24 x 7 Live Support",
//                    Line3 = "Consolidated Rates ",
//                    Line4 = "Savings of up-to 60% per shipment",
//                    Line5 = "Cloud Access",
//                    Active = true,
//                    Name = "2 YEARS",
//                    MonthlyAmount = 900,
//                    AnnualAmount = 10800,
//                    DisplayOrder = 1
//                };
//                var subscription2 = new SubscriptionType
//                {
//                    IsPopular = false,
//                    Line1 = "1 YEAR",
//                    Line2 = "24 x 7 Live Support",
//                    Line3 = "Consolidated Rates ",
//                    Line4 = "Savings of up-to 60% per shipment",
//                    Line5 = "Cloud Access",
//                    Active = true,
//                    Name = "1 YEAR",
//                    MonthlyAmount = 1000,
//                    AnnualAmount = 12000,
//                    DisplayOrder = 2
//                };
//                var subscription3 = new SubscriptionType
//                {
//                    IsPopular = false,
//                    Line1 = "MONTH TO MONTH",
//                    Line2 = "24 x 7 Live Support",
//                    Line3 = "Consolidated Rates ",
//                    Line4 = "Savings of up-to 60% per shipment",
//                    Line5 = "Cloud Access",
//                    Active = true,
//                    Name = "MONTH TO MONTH",
//                    MonthlyAmount = 1500,
//                    AnnualAmount = 18000,
//                    DisplayOrder = 3
//                };
//                var s1 = _airconDBContext.SubscriptionTypes.Where(x => x.Name == subscription1.Name).SingleOrDefault();
//                if (s1 != null)
//                {
//                    s1.IsPopular = subscription1.IsPopular;
//                    s1.Line1 = subscription1.Line1;
//                    s1.Line2 = subscription1.Line2;
//                    s1.Line3 = subscription1.Line3;
//                    s1.Line4 = subscription1.Line4;
//                    s1.Line5 = subscription1.Line5;
//                    s1.Active = subscription1.Active;
//                    s1.MonthlyAmount = subscription1.MonthlyAmount;
//                    s1.AnnualAmount = subscription1.AnnualAmount;
//                    s1.DisplayOrder = subscription1.DisplayOrder;
//                    _airconDBContext.SubscriptionTypes.Update(s1);
//                }
//                else
//                {
//                    _airconDBContext.SubscriptionTypes.Add(subscription1);
//                }

//                var s2 = _airconDBContext.SubscriptionTypes.Where(x => x.Name == subscription2.Name).SingleOrDefault();
//                if (s2 != null)
//                {
//                    s2.IsPopular = subscription2.IsPopular;
//                    s2.Line1 = subscription2.Line1;
//                    s2.Line2 = subscription2.Line2;
//                    s2.Line3 = subscription2.Line3;
//                    s2.Line4 = subscription2.Line4;
//                    s2.Line5 = subscription2.Line5;
//                    s2.Active = subscription2.Active;
//                    s2.MonthlyAmount = subscription2.MonthlyAmount;
//                    s2.AnnualAmount = subscription2.AnnualAmount;
//                    s2.DisplayOrder = subscription2.DisplayOrder;
//                    _airconDBContext.SubscriptionTypes.Update(s2);
//                }
//                else
//                {
//                    _airconDBContext.SubscriptionTypes.Add(subscription2);
//                }

//                var s3 = _airconDBContext.SubscriptionTypes.Where(x => x.Name == subscription3.Name).SingleOrDefault();
//                if (s3 != null)
//                {
//                    s3.IsPopular = subscription3.IsPopular;
//                    s3.Line1 = subscription3.Line1;
//                    s3.Line2 = subscription3.Line2;
//                    s3.Line3 = subscription3.Line3;
//                    s3.Line4 = subscription3.Line4;
//                    s3.Line5 = subscription3.Line5;
//                    s3.Active = subscription3.Active;
//                    s3.MonthlyAmount = subscription3.MonthlyAmount;
//                    s3.AnnualAmount = subscription3.AnnualAmount;
//                    s3.DisplayOrder = subscription3.DisplayOrder;
//                    _airconDBContext.SubscriptionTypes.Update(s3);
//                }
//                else
//                {
//                    _airconDBContext.SubscriptionTypes.Add(subscription3);
//                }

//                _airconDBContext.SaveChanges();

//                // For Template providers -- Need to validate per row.
//                var emailLayout = _airconDBContext.TemplateDefinitions.Where(x => x.Name == TemplateDefinitionNames.Layout.EmailLayout).SingleOrDefault();
//                if(emailLayout == null)
//                {
//                    _airconDBContext.TemplateDefinitions.Add(TemplateDefinationProvider.EmailLayout);
//                    _airconDBContext.TemplateDefinitions.Add(TemplateDefinationProvider.GeneralPasswordReset);
//                    _airconDBContext.SaveChanges();
//                }
//                var generalSignUpWelcomeEmail = _airconDBContext.TemplateDefinitions.Where(x => x.Name == TemplateDefinitionNames.General.SignUpWelcomeEmail).SingleOrDefault();
//                if(generalSignUpWelcomeEmail == null)
//                {
//                    _airconDBContext.TemplateDefinitions.Add(TemplateDefinationProvider.GeneralSignUpWelcomeEmail);
//                    _airconDBContext.SaveChanges();
//                }
//            }
//        }


//        public static async Task CheckAddSampleAirconUsersAsync(this IServiceProvider serviceProvider)
//        {
//            using (var scope = serviceProvider.CreateScope())
//            {
//                var services = scope.ServiceProvider;
//                var dbContext = services.GetRequiredService<AirconDbContext>();
//                var userManager = services.GetRequiredService<UserManager<User>>();

//                var config = services.GetRequiredService<IConfiguration>();
//                var systemAdminSection = config.GetSection("SystemAdmin");
//                if (systemAdminSection == null)
//                    return;

//                var userPassword = systemAdminSection["Password"];

//                var userList = BogusEmployeeData.GetUsers();
//                var userCnt = dbContext.Users.Where(x=> x.IsEmployee).ToList().Count;
//                if (userCnt < 10)
//                {
//                    foreach (var fakeUser in userList)
//                    {
//                        var user = new User
//                        {
//                            UserName = fakeUser.Email,
//                            DisplayUserId = fakeUser.DisplayUserId,
//                            FirstName = fakeUser.FirstName,
//                            LastName = fakeUser.LastName,
//                            WorkTitle = fakeUser.WorkTitle,
//                            CreationDateUtc = fakeUser.CreationDate,
//                            ApprovedDateUtc = fakeUser.ApprovedDate,
//                            ActivatedDateUtc = fakeUser.ActivatedDate,
//                            SignedUpDateUtc = fakeUser.SignedUpDate,
//                            IsActive = fakeUser.IsActive,
//                            IsApproved = fakeUser.IsApproved,
//                            IsEmployee = fakeUser.IsEmployee,
//                            UserStatus = fakeUser.UserStatus,
//                            Email = fakeUser.Email,
//                            PhoneNumber = fakeUser.PhoneNumber
//                        };
//                        var userCreated = await userManager.CheckAddNewUserAsync(user, userPassword);
//                        var userCreatedRole = await userManager.AddToRoleAsync(userCreated, fakeUser.Role);
//                    }
//                    await dbContext.SaveChangesAsync();

//                }
//                await dbContext.SaveChangesAsync();
//            }
//        }


//    }
//}
