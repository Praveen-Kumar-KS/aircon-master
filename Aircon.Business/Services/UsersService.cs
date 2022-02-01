//using Aircon.Data;
//using Aircon.Data.Entities;
//using Aircon.Business.Models.Roles;
//using Aircon.Business.Models.Users;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using Aircon.Business.Models.Shared;
//using Aircon.Business.Helper;

//namespace Aircon.Business.Services
//{
//    public interface IUsersService : IDisposable
//    {
//        List<CustomerUserModel> GetUsersList(SearchModel searchModel);
//        ViewUserModel GetUserById(int id);
//        ViewUserModel UpdateUser(ViewUserModel viewUserModel);
//        Task<ViewUserModel> AddSelectedRoleToUser(ViewUserModel addRoleUsersModel);
//        Task<ViewUserModel> RemoveSelectedRoleFromUser(int roleId, ViewUserModel removeUserRolesModel);
//        Task<AddUserModel> AddUser(AddUserModel addUserModel);
//        CustomerUserModel Authenticate(string username, string password);
//        CustomerUserModel GetUserByName(string username);
//        UserDetailModel UpdateSignUpUser(UserDetailModel viewUserModel);
//    }
//    public class UsersService : IUsersService
//    {
//        private readonly AirconDbContext _airconDBContext;
//        private readonly UserManager<User> UserManager;
//        private readonly RoleManager<Role> RoleManager;

//        public UsersService(AirconDbContext airconDbContext, UserManager<User> userManager, RoleManager<Role> roleManager)
//        {
//            UserManager = userManager;
//            _airconDBContext = airconDbContext;
//            RoleManager = roleManager;
//        }

//        public ViewUserModel GetUserById(int id)
//        {
//            ViewUserModel viewUserModel = new ViewUserModel();
//            viewUserModel = _airconDBContext.Users
//                .Where(x => x.Id == id)
//                .Select(x => new ViewUserModel
//                {
//                    Id = x.Id,
//                    UserName = x.UserName,
//                    Email = x.Email,
//                    FirstName = x.FirstName,
//                    LastName = x.LastName,
//                    Active = x.Disabled == true ? false : true,
//                }).SingleOrDefault();
//            var roleIds = _airconDBContext.UserRoles
//                                      .Where(x => x.UserId == id).Select(x => x.RoleId).ToList();
//            viewUserModel.UserRoles = new List<RoleModel>();
//            foreach (var i in roleIds)
//            {
//                viewUserModel.UserRoles.Add(_airconDBContext.Roles
//                                             .Where(x => x.Id == i && x.Active)
//                                             .Select(x => new RoleModel
//                                             {
//                                                 Id = x.Id,
//                                                 Active = x.Active,
//                                                 Name = x.Name,
//                                                 Description = x.Description
//                                             }).SingleOrDefault());
//            }
//            viewUserModel.RoleList = _airconDBContext.Roles
//                                .Where(x => !roleIds.Contains(x.Id) && x.Active)
//                                .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
//                                .ToList();
//            return viewUserModel;
//        }

//        public ViewUserModel UpdateUser(ViewUserModel viewUserModel)
//        {
//            User user = _airconDBContext.Users.Find(viewUserModel.Id);
//            user.UserName = viewUserModel.UserName;
//            user.Email = viewUserModel.Email;
//            user.FirstName = viewUserModel.FirstName;
//            user.LastName = viewUserModel.LastName;
//            user.Disabled = viewUserModel.Active == true ? false : true;
//            var result = UserManager.UpdateAsync(user).Result;
//            _airconDBContext.SaveChangesAsync();
//            return viewUserModel;
//        }

//        public List<CustomerUserModel> GetUsersList(SearchModel searchModel)
//        {
//            List<CustomerUserModel> userList = new List<CustomerUserModel>();
//            var query = (from x in _airconDBContext.Users.AsQueryable()
//                         select new CustomerUserModel
//                         {
//                             Id = x.Id,
//                             Email = x.Email,
//                             UserName = x.UserName,
//                             IsActive = x.Disabled == true ? false : true
//                         });
//            var parameters = new Dictionary<string, object>();
//            parameters.Add("Name", searchModel.Name);
//            parameters.Add("Email", searchModel.Name);
//            parameters.Add("UserName", searchModel.Name);
//            query = SearchHelpler.GetBasicSearchResults(query, parameters);
//            userList = query.ToList();
//            return userList;
//        }

//        public async Task<ViewUserModel> AddSelectedRoleToUser(ViewUserModel addRoleUsersModel)
//        {
//            Role role = _airconDBContext.Roles.Where(x => x.Id == Convert.ToInt32(addRoleUsersModel.SelectedRoleId)).SingleOrDefault();
//            bool roleExists = await RoleManager.RoleExistsAsync(role.Name);
//            if (roleExists)
//            {
//                User user = await UserManager.FindByNameAsync(addRoleUsersModel.UserName);
//                bool userinRole = await UserManager.IsInRoleAsync(user, role.Name);
//                if (!userinRole)
//                {
//                    var userResult = await UserManager.AddToRoleAsync(user, role.Name);
//                }
//            }
//            return addRoleUsersModel;
//        }

//        public async Task<ViewUserModel> RemoveSelectedRoleFromUser(int roleId, ViewUserModel removeUserRolesModel)
//        {
//            Role role = _airconDBContext.Roles.Where(x => x.Id == roleId).SingleOrDefault();
//            bool roleExists = await RoleManager.RoleExistsAsync(role.Name);
//            if (roleExists)
//            {
//                User user = await UserManager.FindByNameAsync(removeUserRolesModel.UserName);
//                bool userinRole = await UserManager.IsInRoleAsync(user, role.Name);
//                if (userinRole)
//                {
//                    var userResult = await UserManager.RemoveFromRoleAsync(user, role.Name);
//                }
//            }
//            return removeUserRolesModel;
//        }


//        public async Task<AddUserModel> AddUser(AddUserModel addUserModel)
//        {
//            var userModel = new User
//            {
//                CreationDate = DateTime.Now,
//                Email = addUserModel.Email,
//                FirstName = addUserModel.FirstName,
//                LastName = addUserModel.LastName,
//                UserName = addUserModel.UserName
//            };
//            var result = await UserManager.CreateAsync(userModel, addUserModel.Password);
//            _airconDBContext.SaveChanges();
//            addUserModel.Id = userModel.Id;
//            return addUserModel;

//        }

//        public CustomerUserModel Authenticate(string username, string password)
//        {
//            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
//                return null;

//            var user = _airconDBContext.Users.AsQueryable().Where(x => x.UserName == username)
//                .Select(x => new CustomerUserModel
//                {
//                    Id = x.Id,
//                    Email = x.Email,
//                    UserName = x.UserName
//                }).SingleOrDefault();

//            // check if username exists
//            if (user == null)
//                return null;

//            // authentication successful
//            return user;
//        }

//        public CustomerUserModel GetUserByName(string username)
//        {
//            var user = _airconDBContext.Users.Where(x => x.UserName == username).Select(x => new CustomerUserModel
//            {
//                UserName = x.UserName,
//                Id = x.Id
//            }).SingleOrDefault();
//            return user;
//        }

//        public UserDetailModel UpdateSignUpUser(UserDetailModel viewUserModel)
//        {
//            User user = _airconDBContext.Users.Find(viewUserModel.UserId);
//            user.FirstName = viewUserModel.FirstName;
//            user.LastName = viewUserModel.LastName;
//            user.WorkTitle = viewUserModel.Role;
//            user.PhoneNumber = viewUserModel.Phone;
//            var result = UserManager.UpdateAsync(user).Result;
//            _airconDBContext.SaveChangesAsync();
//            return viewUserModel;
//        }

//        public void Dispose()
//        {
//            _airconDBContext.Dispose();
//            UserManager.Dispose();
//            RoleManager.Dispose();
//        }
//    }
//}
