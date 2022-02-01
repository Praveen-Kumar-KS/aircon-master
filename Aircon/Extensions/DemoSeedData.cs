//using Aircon.Data;
//using Aircon.Data.Entities;
//using Aircon.SampleData.Bogus;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Aircon.Business.Extensions;

//namespace Aircon.Extensions
//{
//    public static class DemoSeedData
//    {
//        public static async Task CheckAddDemoSeedAsync(this IServiceProvider serviceProvider)
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

//                var customerCnt = dbContext.Customers.ToList().Count;
//                if (customerCnt < 10)
//                {
//                    var customerList = BogusCustomerData.GetCustomer();
//                    foreach (var fakeCustomer in customerList)
//                    {
//                        var domain = fakeCustomer.CustomerDomains.FirstOrDefault().DomainName.ToLower();
//                        foreach(var user in fakeCustomer.Users)
//                        {
//                            user.Email = string.Format("{0}{1}", user.Email, domain);
//                            user.UserName = user.Email;
//                            var userCreated = await userManager.CheckAddNewUserAsync(user, userPassword);
//                            var userCreatedRole = await userManager.AddToRoleAsync(userCreated, BogusCustomerData.GetRole());
//                        }
//                        dbContext.Customers.Add(fakeCustomer);
//                    }

//                    var customerOpportunityList = BogusCustomerData.GetCustomerOpportunity();
//                    foreach (var fakeCustomerOpportunity in customerOpportunityList)
//                    {
//                        var domain = BogusCustomerData.GetDomain();
//                        foreach (var user in fakeCustomerOpportunity.Users)
//                        {
//                            user.Email = string.Format("{0}{1}", user.Email, domain);
//                            user.UserName = user.Email;
//                            var userCreated = await userManager.CheckAddNewUserAsync(user, userPassword);
//                            var userCreatedRole = await userManager.AddToRoleAsync(userCreated, BogusCustomerData.GetRole());
//                        }

//                        dbContext.CustomerOpportunities.Add(fakeCustomerOpportunity);
//                    }
//                    await dbContext.SaveChangesAsync();

//                }

//            }
//        }

//    }
//}
