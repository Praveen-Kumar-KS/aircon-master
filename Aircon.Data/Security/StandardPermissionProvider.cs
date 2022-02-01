using Aircon.Data.Entities;
using Aircon.Data.Security;
using Aircon.Framework.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aircon.Data.Security
{
    public partial class StandardPermissionProvider : IPermissionProvider
    {
        //public static readonly Role RoleSystemAdmin = new Role { Name = RoleSystemName.SystemAdministrators, Description = "System Admins", Active = true, NormalizedName = RoleSystemName.SystemAdministrators };
        //public static readonly Role RoleAdministrators = new Role { Name = RoleSystemName.Administrators, Description = "Administrators", Active = true, NormalizedName = RoleSystemName.Administrators };
        //public static readonly Role RoleManager = new Role { Name = RoleSystemName.Manager, Description = "Manager", Active = true, NormalizedName = RoleSystemName.Manager };
        //public static readonly Role RoleSales = new Role { Name = RoleSystemName.Sales, Description = "Sales", Active = true, NormalizedName = RoleSystemName.Sales };
        //public static readonly Role RoleUser = new Role { Name = RoleSystemName.User, Description = "End User", Active = true, NormalizedName = RoleSystemName.User };
        //public static readonly Role RoleWarehouseAssociate = new Role { Name = RoleSystemName.WarehouseAssociate, Description = "Warehouse Associate", Active = true, NormalizedName = RoleSystemName.WarehouseAssociate };


        public static readonly Role RoleSystemAdmin = new Role { Name = RoleSystemName.SystemAdministrators, DisplayName = "System Admins", Active = true, IsSystemRole = true };
        public static readonly Role RoleAdministrators = new Role { Name = RoleSystemName.Administrators, DisplayName = "Administrators", Active = true };
        public static readonly Role RoleManager = new Role { Name = RoleSystemName.Manager, DisplayName = "Manager", Active = true };
        public static readonly Role RoleSales = new Role { Name = RoleSystemName.Sales, DisplayName = "Sales", Active = true };
        public static readonly Role RoleUser = new Role { Name = RoleSystemName.User, DisplayName = "End User", Active = true };
        public static readonly Role RoleWarehouseAssociate = new Role { Name = RoleSystemName.WarehouseAssociate, DisplayName = "Warehouse Associate", Active = true };


        public static readonly Permission AccessSystemAdmin = new Permission { Name = "System Admin", SystemName = PermissionSystemName.SystemAdmin, Category = "SystemAdmin" };
        public static readonly Permission AccessAdmin = new Permission { Name = "Access admin", SystemName = PermissionSystemName.AccessAdmin, Category = "Admin" };
        public static readonly Permission AccessCustomer = new Permission { Name = "Access customer", SystemName = PermissionSystemName.AccessCustomer, Category = "Customer" };
        public static readonly Permission ManageUserProfile = new Permission { Name = "Admin. Manage User Profiles", SystemName = PermissionSystemName.UserProfile, Category = "UserManagement" };
        public static readonly Permission ManageRole = new Permission { Name = "Admin. Manage Roles", SystemName = PermissionSystemName.Role, Category = "UserManagement" };
        public static readonly Permission ManageUser = new Permission { Name = "Admin area. Manage Users", SystemName = PermissionSystemName.User, Category = "UserManagement" };
        public static readonly Permission AccessCustomerSupport = new Permission { Name = "Access Customer Support", SystemName = PermissionSystemName.CustomerSupport, Category = "CustomerSupport" };
        public static readonly Permission ManageCustomerOpportunity = new Permission { Name = "Admin area. Manage Customer Opportunity", SystemName = PermissionSystemName.CustomerOpportunity, Category = "CustomerSupport" };
        public static readonly Permission ManageCompanySetting = new Permission { Name = "Admin area. Manage Company Setting", SystemName = PermissionSystemName.CompanySetting, Category = "Manager" };
        public static readonly Permission ManagerUser = new Permission { Name = "Admin area. Manage User", SystemName = PermissionSystemName.ManagerUser, Category = "Manager" };
        //Settings--Preference
        public static readonly Permission Preference = new Permission { Name = "Settings.Preference", SystemName = PermissionSystemName.CompanyPrefernce, Category = "CustomerPrefernce" };
        //Settings--Profile
        public static readonly Permission UploadImage = new Permission { Name = "Settings.Profile.UploadImage", SystemName = PermissionSystemName.Image, Category = "CustomerProfileImage" };
        public static readonly Permission Profile = new Permission { Name = "Settings.Profile", SystemName = PermissionSystemName.CompanyProfile, Category = "CustomerProfile" };
        //Settings--User
        public static readonly Permission AddUser = new Permission { Name = "Settings.User.AddUser", SystemName = PermissionSystemName.CompanyAddUser, Category = "CustomerAddUser" };
        public static readonly Permission UserCustomer = new Permission { Name = "Settings.User.UserCustomer", SystemName = PermissionSystemName.CompanyUser, Category = "CustomerUser" };
        public static readonly Permission UserDeny = new Permission { Name = "Settings.User.UserDeny", SystemName = PermissionSystemName.DenyUser, Category = "CustomerUserDeny" };
        public static readonly Permission UserApproval = new Permission { Name = "Settings.User.UserApproval", SystemName = PermissionSystemName.ApprovalUser, Category = "CustomerUserApproval" };
        public static readonly Permission ActiveUser = new Permission { Name = "Settings.User.ActiveUser", SystemName = PermissionSystemName.UserActive, Category = "CustomerUserActive" };
        public static readonly Permission InactiveUser = new Permission { Name = "Settings.User.InactiveUser", SystemName = PermissionSystemName.UserInactive, Category = "CustomerUserInactive" };
        public static readonly Permission UserEdit = new Permission { Name = "Settings.User.EditUser", SystemName = PermissionSystemName.EditUser, Category = "CustomerUserEdit" };
        //Settings--Company
        public static readonly Permission Company = new Permission { Name = "Settings.Company", SystemName = PermissionSystemName.CustomerCompany, Category = "CustomerCompany" };
        public static readonly Permission CompanyProfile = new Permission { Name = "Settings.Company.CompanyProfile", SystemName = PermissionSystemName.CustomerCompanyProfile, Category = "CompanyProfile" };
        public static readonly Permission RemovePhoto = new Permission { Name = "Settings.Company.RemovePhoto", SystemName = PermissionSystemName.CompanyPhoto, Category = "CompanyRemovePhoto" };
        public static readonly Permission AddPayment = new Permission { Name = "Settings.Company.AddPayment", SystemName = PermissionSystemName.AddCompanyPayment, Category = "CompanyAddPayment" };
        public static readonly Permission DeletePayment = new Permission { Name = "Settings.Company.DeletePayment", SystemName = PermissionSystemName.DeleteCompanyPayment, Category = "CompanyDeletePayment" };
        public static readonly Permission EditPayment = new Permission { Name = "Settings.Company.EditPayment", SystemName = PermissionSystemName.EditCompanyPayment, Category = "CompanyEditPayment" };
        public static readonly Permission AddAddress = new Permission { Name = "Settings.Company.AddAddress", SystemName = PermissionSystemName.AddCompanyAddress, Category = "CompanyAddAddress" };
        public static readonly Permission DeleteAddress = new Permission { Name = "Settings.Company.DeleteAddress", SystemName = PermissionSystemName.DeleteCompanyAddress, Category = "CompanyDeleteAddress" };
        public static readonly Permission EditAddress = new Permission { Name = "Settings.Company.EditAddress", SystemName = PermissionSystemName.EditCompanyAddress, Category = "CompanyEditAddress" };
        public static readonly Permission DownloadCsvTemplate = new Permission { Name = "Settings.Company.DownloadCsv", SystemName = PermissionSystemName.CsvTemplate, Category = "CompanyDownloadCsv" };
        public static readonly Permission UploadCsv = new Permission { Name = "Settings.Company.UploadCsv", SystemName = PermissionSystemName.CsvUpload, Category = "CompanyUploadCsv" };
        //Settings--Contact
        public static readonly Permission Contact = new Permission { Name = "Settings.Contact", SystemName = PermissionSystemName.CompanyContact, Category = "CustomerContact" };
        public static readonly Permission AddContact = new Permission { Name = "Settings.Contact.AddContact", SystemName = PermissionSystemName.AddCompanyContact, Category = "CustomerAddContact" };
        public static readonly Permission EditContact = new Permission { Name = "Settings.Contact.EditContact", SystemName = PermissionSystemName.EditCompanyContact, Category = "CustomerEditContact" };
        public static readonly Permission DeleteContact = new Permission { Name = "Settings.Contact.DeleteContact", SystemName = PermissionSystemName.DeleteCompanyContact, Category = "CustomeDeleteContact" };
        //Settings--Employee
        public static readonly Permission Employee = new Permission { Name = "Settings.Employee", SystemName = PermissionSystemName.CompanyEmployee, Category = "Employee" };
        public static readonly Permission AddEmployee = new Permission { Name = "Settings.Employee.AddEmployee", SystemName = PermissionSystemName.AddCompanyEmployee, Category = "AddEmployee" };
        public static readonly Permission ActiveEmployee = new Permission { Name = "Settings.Employee.ActiveEmployee", SystemName = PermissionSystemName.ActiveCompanyEmployee, Category = "ActiveEmployee" };
        public static readonly Permission InactiveEmployee = new Permission { Name = "Settings.Employee.InactiveEmployee", SystemName = PermissionSystemName.InactiveCompanyEmployee, Category = "InactiveEmployee" };
        public static readonly Permission EditEmployee = new Permission { Name = "Settings.Employee.EditEmployee", SystemName = PermissionSystemName.EditCompanyEmployee, Category = "EditEmployee" };
        //Admin--User
        public static readonly Permission AddUserAdmin = new Permission { Name = "Admin.User.AddUser", SystemName = PermissionSystemName.AddAdminUser, Category = "AddUserAdmin" };
        public static readonly Permission AdminUser = new Permission { Name = "Admin.User.AdminUser", SystemName = PermissionSystemName.AdminUser, Category = "UserAdmin" };
        public static readonly Permission UserAdminDeny = new Permission { Name = "Admin.User.UserDeny", SystemName = PermissionSystemName.DenyAdminUser, Category = "AdminUserDeny" };
        public static readonly Permission UserAdminApproval = new Permission { Name = "Admin.User.UserApproval", SystemName = PermissionSystemName.ApprovalAdminUser, Category = "AdminUserApproval" };
        public static readonly Permission ActiveUserAdmin = new Permission { Name = "Admin.User.ActiveUser", SystemName = PermissionSystemName.ActiveAdminUser, Category = "AdminActiveUser" };
        public static readonly Permission InactiveUserAdmin = new Permission { Name = "Admin.User.InactiveUser", SystemName = PermissionSystemName.InactiveAdminUser, Category = "AdminInactiveUser" };
        public static readonly Permission AdminUserEdit = new Permission { Name = "Admin.User.EditUser", SystemName = PermissionSystemName.EditAdminUser, Category = "AdminUserEdit" };
        //Admin--Customer
        public static readonly Permission AdminCustomer = new Permission { Name = "Admin.Customer.CustomerUser", SystemName = PermissionSystemName.AdminCustomer, Category = "AdminCustomer" };
        public static readonly Permission DenyAdminCustomer = new Permission { Name = "Admin.Customer.UserDeny", SystemName = PermissionSystemName.AdminDenyCustomer, Category = "AdminCustomerDeny" };
        public static readonly Permission ApprovalAdminCustomer = new Permission { Name = "Admin.Customer.ApprovalUser", SystemName = PermissionSystemName.AdminApprovalCustomer, Category = "AdminCustomerApproval" };
        public static readonly Permission ActiveAdminCustomer = new Permission { Name = "Admin.Customer.ActiveUser", SystemName = PermissionSystemName.AdminActiveCustomer, Category = "AdminActiveCustomer" };
        public static readonly Permission InactiveAdminCustomer = new Permission { Name = "Admin.Customer.InactiveUser", SystemName = PermissionSystemName.AdminInactiveCustomer, Category = "AdminInactiveCustomer" };
        public static readonly Permission AdminCustomerEdit = new Permission { Name = "Admin.Customer.EditUser", SystemName = PermissionSystemName.AdminEditCustomer, Category = "AdminCustomerEdit" };
        public static readonly Permission AdminCustomerDelete = new Permission { Name = "Admin.Customer.DeleteUser", SystemName = PermissionSystemName.AdminDeleteCustomer, Category = "AdminCustomerDelete" };


        public virtual IEnumerable<Permission> GetPermissions()
        {
            return new[]
            {
               AccessSystemAdmin,
               AccessCustomer,
                AccessAdmin,
                ManageUserProfile,
                ManageRole,
                ManageUser,
                AccessCustomerSupport,
                ManageCustomerOpportunity,
                ManageCompanySetting,
                ManagerUser,
                Preference,
                UploadImage,
                Profile,
                AddUser,
                UserCustomer,
                UserDeny,
                UserApproval,
                ActiveUser,
                InactiveUser,
                UserEdit,
                Company,
                CompanyProfile,
                RemovePhoto,
                AddPayment,
                DeletePayment,
                EditPayment,
                AddAddress,
                DeleteAddress,
                EditAddress,
                DownloadCsvTemplate,
                UploadCsv,
                Contact,
                AddContact,
                EditContact,
                DeleteContact,
                Employee,
                AddEmployee,
                ActiveEmployee,
                InactiveEmployee,
                EditEmployee,
                AddUserAdmin,
                AdminUser,
                UserAdminDeny,
                UserAdminApproval,
                ActiveUserAdmin,
                InactiveUserAdmin,
                AdminUserEdit,
                AdminCustomer,
                DenyAdminCustomer,
                ApprovalAdminCustomer,
                ActiveAdminCustomer,
                InactiveAdminCustomer,
                AdminCustomerEdit,
                AdminCustomerDelete
            };
        }

        public virtual IEnumerable<Role> GetRoles()
        {
            return new[]
            {
                RoleSystemAdmin,
                RoleAdministrators,
                RoleManager,
                RoleSales,
                RoleUser,
                RoleWarehouseAssociate
            };
        }

        public virtual IEnumerable<DefaultPermission> GetDefaultPermissions()
        {
            return new[]
            {
                new DefaultPermission
                {
                    RoleSystemName = RoleSystemName.Administrators,
                    Permissions = new[]
                    {
                        AccessCustomer,
                        ManageUserProfile,
                        ManageRole,
                        ManageUser,
                        AccessCustomerSupport,
                        ManageCustomerOpportunity,
                        ManageCompanySetting
                    }
                },
                new DefaultPermission
                {
                    RoleSystemName = RoleSystemName.Manager,
                    Permissions = new[]
                    {
                        AccessCustomer,
                        ManageUserProfile,
                        ManageCompanySetting
                    }
                },
                new DefaultPermission
                {
                    RoleSystemName = RoleSystemName.Sales,
                    Permissions = new[]
                    {
                        AccessCustomer,
                        ManageUserProfile
                    }
                },
                new DefaultPermission
                {
                    RoleSystemName = RoleSystemName.User,
                    Permissions = new[]
                    {
                        AccessCustomer,
                        ManageUserProfile
                    }
                },
                new DefaultPermission
                {
                    RoleSystemName = RoleSystemName.WarehouseAssociate,
                    Permissions = new[]
                    {
                        AccessCustomer,
                        ManageUserProfile
                    }
                },
                new DefaultPermission
                {
                    RoleSystemName = RoleSystemName.SystemAdministrators,
                    Permissions = new[]
                    {
                        AccessSystemAdmin
                    }
                },
            };
        }
    }
}
