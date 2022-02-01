using Aircon.Business.Models.Shared;
using Aircon.Business.Services.Shared;
using Aircon.Data;
using Aircon.Data.Entities;
using Aircon.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Business.Services.Admin
{

    public interface IAdminUserService
    {
        List<UserModel> GetQueueUsers(UserStatus userStatus,string searchText, int recordCountUserQueue);
        List<UserModel> GetAdminUsers( bool isActive, bool isAll,string searchText, int recordCountCustomersQueue);
        UserModel GetUser(int id);
        void ApprovingUser(int id);
        void DenyUser(int id);
        User ActivatingUser(int id);
        void DeactivatingUser(int id);
        Task<UserModel> UpdateUser(UserModel updateEmployeeUserModel);
        Task<UserModel> AddUser(UserModel addEmployeeUserModel);

    }
    public class AdminUserService : SharedUserService ,IAdminUserService
    {
        public AdminUserService(AirconDbContext airconDbContext, UserManager<User> userManager) : base(airconDbContext, userManager)
        {
        }

        public List<UserModel> GetQueueUsers( UserStatus userStatus,string searchText, int recordCountUserQueue)
        {
            var users = _airconDbContext.Users.Include(x => x.Customer)
                .Where(x => x.UserStatus == userStatus && x.CustomerId != null)
                .Select(x => new UserModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    WorkTitle = x.WorkTitle,
                    UserName = x.UserName,
                    Email = x.Email,
                    CompanyName = x.CustomerId.HasValue ? x.Customer.CompanyName : string.Empty,
                    CustomerId = x.CustomerId.HasValue ? x.CustomerId.Value : 0,
                    IsActive = x.IsActive,
                    PhoneNumber = x.PhoneNumber,
                    DisplayUserId = x.DisplayUserId,
                    CreationDateUtc = x.CreationDateUtc,
                    ApprovedDateUtc = x.ApprovedDateUtc,
                    ActivatedDateUtc = x.ActivatedDateUtc,
                    SignedUpDateUtc = x.SignedUpDateUtc,
                    IsApproved = x.IsApproved,
                    IsEmployee = x.IsEmployee,
                    UserStatus = x.UserStatus
                });
            if (searchText != null)
            {
                users =
                    users.Where(x =>
                         (x.FirstName == null ? false : x.FirstName.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.LastName == null ? false : x.LastName.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.DisplayUserId == null ? false : x.DisplayUserId.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.WorkTitle == null ? false : x.WorkTitle.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.Email == null ? false : x.Email.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.PhoneNumber == null ? false : x.PhoneNumber.Contains(searchText)) ||
                         (x.UserName == null ? false : x.UserName.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.CompanyName == null ? false : x.CompanyName.ToUpper().Contains(searchText.ToUpper()))
                        ).Select(y => y);
            }
            users = users.Take(recordCountUserQueue);
            return users.ToList();
        }
        public List<UserModel> GetAdminUsers(bool isActive, bool isAll,string searchText,int recordCountCustomersQueue)
        {
            var users = _airconDbContext.Users.Include(x=>x.Customer).Where(x => x.UserStatus == UserStatus.Approved && x.CustomerId != null )
                .Select(x => new UserModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    WorkTitle = x.WorkTitle,
                    UserName = x.UserName,
                    Email = x.Email,
                    CompanyName = x.CustomerId.HasValue ? x.Customer.CompanyName : string.Empty,
                    CustomerId = x.CustomerId.HasValue ? x.CustomerId.Value : 0,
                    IsActive = x.IsActive,
                    PhoneNumber = x.PhoneNumber,
                    DisplayUserId = x.DisplayUserId,
                    CreationDateUtc = x.CreationDateUtc,
                    ApprovedDateUtc = x.ApprovedDateUtc,
                    ActivatedDateUtc = x.ActivatedDateUtc,
                    SignedUpDateUtc = x.SignedUpDateUtc,
                    IsApproved = x.IsApproved,
                    IsEmployee = x.IsEmployee,
                    UserStatus = x.UserStatus
                }).AsQueryable();
            var userList = new List<UserModel>();
            if (!isAll)
            {
                users = users.Where(x => x.IsActive == isActive).AsQueryable();
            }
           
            if (searchText != null)
            {

                users = users.Where(x =>
                     (x.FirstName == null ? false : x.FirstName.ToUpper().Contains(searchText.ToUpper())) ||
                     (x.LastName == null ? false : x.LastName.ToUpper().Contains(searchText.ToUpper())) ||
                     (x.DisplayUserId == null ? false : x.DisplayUserId.ToUpper().Contains(searchText.ToUpper())) ||
                     (x.WorkTitle == null ? false : x.WorkTitle.ToUpper().Contains(searchText.ToUpper())) ||
                     (x.Email == null ? false : x.Email.ToUpper().Contains(searchText.ToUpper())) ||
                     (x.PhoneNumber == null ? false : x.PhoneNumber.Contains(searchText)) ||
                     (x.UserName == null ? false : x.UserName.ToUpper().Contains(searchText.ToUpper())) ||
                     (x.CompanyName == null ? false : x.CompanyName.ToUpper().Contains(searchText.ToUpper()))
                    ).Select(y => y);
            }
            
            users = users.Take(recordCountCustomersQueue);
            return users.ToList();
        }

    }
}
