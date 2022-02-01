using Aircon.Business.Helper;
using Aircon.Business.Models.Shared;
using Aircon.Business.Services.Shared;
using Aircon.Core;
using Aircon.Data;
using Aircon.Data.Entities;
using Aircon.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Business.Services.SystemAdmin
{
    public interface IEmployeeUserService
    {
        List<UserModel> GetEmployees(bool isActive, bool isAll,string searchText, int recordCountEmployee);
        UserModel GetEmployee(int id);
        Task<UserModel> UpdateUser(UserModel updateEmployeeUserModel);
        Task<UserModel> AddUser(UserModel addEmployeeUserModel);
        void ActivateEmployeeUser(int id);
        void DeactivateEmployeeUser(int id);
         

    }
    public class EmployeeUserService : SharedUserService , IEmployeeUserService
    {

        public EmployeeUserService(AirconDbContext airconDbContext, UserManager<User> userManager): base(airconDbContext,userManager)
        {
        }

        public List<UserModel> GetEmployees(bool isActive, bool isAll,string searchText, int recordCountEmployee)
        {
            var users = _airconDbContext.Users
                .Where(x => x.IsEmployee == true)
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
                    UserStatus = x.UserStatus
                }).AsQueryable();
            var userList = new List<UserModel>();
            if (isAll)
            {
                users = users.Where(x => x.IsActive == isActive);
            }
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
                         (x.UserName == null ? false : x.UserName.ToUpper().Contains(searchText.ToUpper()))
                        ).Select(y => y);
            }
            users = users.Take(recordCountEmployee);
            return users.ToList();
        }

        public UserModel GetEmployee(int id)
        {
            return _airconDbContext.Users
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
                                UserStatus = x.UserStatus
                            }).SingleOrDefault();
        }

        public void ActivateEmployeeUser(int id)
        {

             User user = _airconDbContext.Users.Find(id);
             user.IsActive = true;
             user.ActivatedDateUtc = DateTime.UtcNow;
            _airconDbContext.Users.Update(user);
            _airconDbContext.SaveChanges();
        
        }


        public void DeactivateEmployeeUser(int id)
        {
            User user = _airconDbContext.Users.Find(id);
            user.IsActive = false;
            user.ActivatedDateUtc = null;
           _airconDbContext.Users.Update(user);
           _airconDbContext.SaveChanges();

        }

    }
}
