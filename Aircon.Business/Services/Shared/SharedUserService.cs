using Aircon.Business.Models.Shared;
using Aircon.Core;
using Aircon.Data;
using Aircon.Data.Entities;
using Aircon.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Services.Shared
{
    public abstract class SharedUserService
    {
        protected readonly AirconDbContext _airconDbContext;
        protected readonly UserManager<User> _userManager;

        public SharedUserService(AirconDbContext airconDbContext, UserManager<User> userManager)
        {
            _airconDbContext = airconDbContext;
            _userManager = userManager;
        }
        public async Task<UserModel> UpdateUser(UserModel updateEmployeeUserModel)
        {
            User user = _airconDbContext.Users.Find(updateEmployeeUserModel.Id);
            user.FirstName = updateEmployeeUserModel.FirstName;
            user.LastName = updateEmployeeUserModel.LastName;
            user.WorkTitle = updateEmployeeUserModel.WorkTitle;
            user.PhoneNumber = updateEmployeeUserModel.PhoneNumber;
            user.IsActive = updateEmployeeUserModel.IsActive;
            user.DisplayUserId = updateEmployeeUserModel.DisplayUserId;
            user.CreationDateUtc = updateEmployeeUserModel.CreationDateUtc;
            user.ApprovedDateUtc = updateEmployeeUserModel.ApprovedDateUtc;
            user.ActivatedDateUtc = updateEmployeeUserModel.ActivatedDateUtc;
            user.SignedUpDateUtc = updateEmployeeUserModel.SignedUpDateUtc;
            user.IsApproved = updateEmployeeUserModel.IsApproved;
            user.IsEmployee = updateEmployeeUserModel.IsEmployee;
            user.UserStatus = updateEmployeeUserModel.UserStatus;
            _airconDbContext.Users.Update(user);
            _airconDbContext.SaveChanges();
            if(!string.IsNullOrEmpty(updateEmployeeUserModel.Role))
            {
                bool userinRole = await _userManager.IsInRoleAsync(user, updateEmployeeUserModel.Role);
                if (!userinRole)
                {
                    var userRoles = _airconDbContext.UserRoles.Where(x => x.UserId == user.Id).ToList();
                    foreach (var userRole in userRoles)
                    {
                        _airconDbContext.UserRoles.Remove(userRole);
                    }
                    await _airconDbContext.SaveChangesAsync();
                    var userResult = await _userManager.AddToRoleAsync(user, updateEmployeeUserModel.Role);
                }
            }

            return updateEmployeeUserModel;
        }

        public async Task<UserModel> AddUser(UserModel addEmployeeUserModel)
        {
            var password = CommonHelper.GenerateRandomDigitCode(16);
            password = string.Format("{0}{1}", password, "@1Aa");
            User user = new User();
            user.UserName = addEmployeeUserModel.Email;
            user.FirstName = addEmployeeUserModel.FirstName;
            user.LastName = addEmployeeUserModel.LastName;
            user.Email = addEmployeeUserModel.Email;
            user.WorkTitle = addEmployeeUserModel.WorkTitle;
            user.PhoneNumber = addEmployeeUserModel.PhoneNumber;
            user.IsActive = addEmployeeUserModel.IsActive;
            user.DisplayUserId = addEmployeeUserModel.DisplayUserId;
            user.CustomerId = addEmployeeUserModel.CustomerId;            
            user.CreationDateUtc = addEmployeeUserModel.CreationDateUtc;
            user.ApprovedDateUtc = addEmployeeUserModel.ApprovedDateUtc;
            user.ActivatedDateUtc = addEmployeeUserModel.ActivatedDateUtc;
            user.SignedUpDateUtc = addEmployeeUserModel.SignedUpDateUtc;
            user.IsApproved = addEmployeeUserModel.IsApproved;
            user.IsEmployee = addEmployeeUserModel.IsEmployee;
            user.UserStatus = addEmployeeUserModel.UserStatus;
            user.CreationDateUtc = DateTime.UtcNow;

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {

                throw new Exception(result.Errors.FirstOrDefault()?.Description);
            }
            user.DisplayUserId = user.Id.ToString("D8");
            _airconDbContext.SaveChanges();
            addEmployeeUserModel.Id = user.Id;
            bool userinRole = await _userManager.IsInRoleAsync(user, addEmployeeUserModel.Role);
            if (!userinRole)
            {
                var userResult = await _userManager.AddToRoleAsync(user, addEmployeeUserModel.Role);
            }

            return addEmployeeUserModel;
        }

        public bool IsFreeEmail(string email)
        {
            string host = string.Empty;
            bool result = false;
            try
            {
                MailAddress address = new MailAddress(email);
                host = address.Host;
            }
            catch (Exception ex)
            {
                result = false;
            }
            result = _airconDbContext.FreeDomains.Any(x=> x.DomainName == host);
            return result;
        }

        public bool IsUniqueEmail(string email)
        {
            bool result = false;
            result = _airconDbContext.Users.Any(x => x.Email == email);
            return result;
        }
        public bool CheckDomain(string email, int customerId)
        {
            string host = string.Empty;
            bool result = false;
            try
            {
                MailAddress address = new MailAddress(email);
                host = address.Host;
            }
            catch (Exception ex)
            {
                result = false;
            }
            result = _airconDbContext.CustomerDomains.Any(x =>x.DomainName == host && x.CustomerId != customerId);
            return result;
        }
        public void ApprovingUser(int id)
        {
            User user = _airconDbContext.Users.Find(id);
            user.IsApproved = true;
            user.ApprovedDateUtc = DateTime.UtcNow;
            user.UserStatus = UserStatus.Approved;
            _airconDbContext.Users.Update(user);
            _airconDbContext.SaveChanges();
        }
        public void DenyUser(int id)
        {
            User user = _airconDbContext.Users.Find(id);
            user.IsApproved = false;
            user.UserStatus = UserStatus.Denied;
            _airconDbContext.Users.Update(user);
            _airconDbContext.SaveChanges();
        }

        public User ActivatingUser(int id)
        {
            User user = _airconDbContext.Users.Find(id);
            user.IsActive = true;
            user.ActivatedDateUtc = DateTime.UtcNow;
            _airconDbContext.Users.Update(user);
            _airconDbContext.SaveChanges();
            return user;
        }

        public void DeactivatingUser(int id)
        {
            User user = _airconDbContext.Users.Find(id);
            user.IsActive = false;
            _airconDbContext.Users.Update(user);
            _airconDbContext.SaveChanges();
        }

        public UserModel GetUser(int id)
        {
            var role = _airconDbContext.UserRoles.Where(x => x.UserId == id).FirstOrDefault();//  x.UserRoles.FirstOrDefault() != null ? x.UserRoles.FirstOrDefault().Role.Name : string.Empty;
            var roleName = string.Empty;
            if (role != null)
            {
                roleName = _airconDbContext.Roles.Where(x => x.Id == role.RoleId).SingleOrDefault().Name;
            }

            return _airconDbContext.Users.Include(x => x.Customer).Include(x => x.UserRoles)//.Where(x => x.UserStatus == UserStatus.Approved)
                            .Where(x => x.Id == id)
                            .Select(x => new UserModel
                            {
                                Id = x.Id,
                                FirstName = x.FirstName,
                                LastName = x.LastName,
                                WorkTitle = x.WorkTitle,
                                UserName = x.UserName,
                                Email = x.Email,
                                IsActive = x.IsActive,
                                PhoneNumber = x.PhoneNumber,
                                DisplayUserId = x.DisplayUserId,
                                CreationDateUtc = x.CreationDateUtc,
                                ApprovedDateUtc = x.ApprovedDateUtc,
                                ActivatedDateUtc = x.ActivatedDateUtc,
                                SignedUpDateUtc = x.SignedUpDateUtc,
                                IsApproved = x.IsApproved,
                                IsEmployee = x.IsEmployee,
                                UserStatus = x.UserStatus,
                                CompanyName = x.Customer.CompanyName,
                                CustomerId = x.CustomerId.Value,
                                Role = roleName,
                            }).SingleOrDefault();
        }

    }
}
