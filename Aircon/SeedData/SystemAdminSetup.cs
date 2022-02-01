//using Aircon.Data;
//using Aircon.Data.Entities;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Aircon.Extensions;
//using Aircon.Data.Security;
//using Aircon.Data.Enums;

//namespace Aircon.SeedData
//{
//    public static class SystemAdminSetup
//    {
//        public static async Task CheckAddSystemAdminSetupAsync(this IServiceProvider serviceProvider)
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

//                var userEmail = systemAdminSection["Email"];
//                var userPassword = systemAdminSection["Password"];
//                var user = new User
//                {
//                    CreationDateUtc = DateTime.UtcNow,
//                    Deleted = false,
//                    Disabled = false,
//                    Email = userEmail,
//                    FirstName = "System",
//                    LastName = "Admin",
//                    UserName = userEmail,
//                    EmailConfirmed = true,
//                    UserStatus = UserStatus.Approved
//                };
//                var superUser = await userManager.CheckAddNewUserAsync(user,userPassword);
//                var addToRole = await userManager.AddToRoleAsync(superUser, RoleSystemName.SystemAdministrators);
//                var addToRole1 = await userManager.AddToRoleAsync(superUser, RoleSystemName.Administrators);
//                dbContext.SaveChanges();
//            }
//        }
//        public static async Task CheckAddAccountTypesSetupAsync(this IServiceProvider serviceProvider)
//        {
//            using (var scope = serviceProvider.CreateScope())
//            {
//                var services = scope.ServiceProvider;
//                var dbContext = services.GetRequiredService<AirconDbContext>();
//                var customerAddressTypes = GetCustomerAddressType();
//                foreach ( var addressType in customerAddressTypes)
//                {
//                    var chkAddressType = dbContext.AddressTypes.Where(x => x.Name == addressType.Name).SingleOrDefault();
//                    if (chkAddressType != null)
//                    {
//                        chkAddressType.IsCustomerAddressType = addressType.IsCustomerAddressType;
//                        dbContext.AddressTypes.Update(chkAddressType);

//                    }else
//                    {
//                        dbContext.AddressTypes.Add(addressType);
//                    }
//                }
//                await dbContext.SaveChangesAsync();
//            }
//        }
//        private static List<AddressType> GetCustomerAddressType()
//        {
//            return new List<AddressType>
//            {
//                new AddressType { Name = "Warehouse", Description = "Warehouse", Active = true, IsCustomerAddressType = true },
//                new AddressType { Name = "Shipping", Description = "Shipping", Active = true , IsCustomerAddressType = true},
//            };
//        }

//    }
//}
