using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Aircon.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Security.Claims;
using System.Linq;
using Aircon.Data.Helper;

namespace Aircon.Data
{
    public class AirconDbContext : IdentityDbContext<User, Role, int>
    {
        public AirconDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<AttachmentType> AttachmentTypes { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<CustomerContact> CustomerContacts { get; set; }
        public DbSet<CustomerNote> CustomerNotes { get; set; }
        public DbSet<CustomerOpportunity> CustomerOpportunities { get; set; }
        public DbSet<CustomerOpportunityAddress> CustomerOpportunityAddresses { get; set; }
        public DbSet<CustomerOpportunityNote> CustomerOpportunityNotes { get; set; }
        public DbSet<CustomerOpportunityPaymentMethod> CustomerOpportunityPaymentMethods { get; set; }
        public DbSet<CustomerPaymentMethod> CustomerPaymentMethods { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<NotificationSetting> NotificationSettings { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<TemplateDefinition> TemplateDefinitions { get; set; }
        public DbSet<UserNote> UserNotes { get; set; }
        public DbSet<CustomerDomain> CustomerDomains { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<WindowsTimeZone> WindowsTimeZones { get; set; }
        public DbSet<UserNotificationSetting> UserNotificationSettings { get; set; }
        public DbSet<FreeDomain> FreeDomains { get; set; }
        //public DbSet<DefaultPreference> DefaultPreferences { get; set; }
        //public DbSet<UserPreference> UserPreferences { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<GenericAttribute> GenericAttributes { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<ShipmentInformationHeader> ShippingDetails { get; set; }
        public DbSet<ShipmentInformationDetail> ShipmentInformationDetails { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<QuotePricing> QuotePricings { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingAddress> BookingAddresses { get; set; }
        public DbSet<BookingDocumentMaster> BookingDocumentMasters { get; set; }
        public DbSet<BookingNotification> BookingNotifications { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUserRole<int>>().ToTable("UserRole");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaim");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserToken");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogin");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaim");

            builder.Entity<Address>().ToTable("Address");
            builder.Entity<AddressType>().ToTable("AddressType");
            builder.Entity<Attachment>().ToTable("Attachment");
            builder.Entity<AttachmentType>().ToTable("AttachmentType");
            builder.Entity<Branch>().ToTable("Branch");
            builder.Entity<Contact>().ToTable("Contact");
            builder.Entity<Customer>().ToTable("Customer");
            builder.Entity<CustomerAddress>().ToTable("CustomerAddress");
            builder.Entity<CustomerContact>().ToTable("CustomerContact");
            builder.Entity<CustomerNote>().ToTable("CustomerNote");
            builder.Entity<CustomerOpportunity>().ToTable("CustomerOpportunity");
            builder.Entity<CustomerOpportunityAddress>().ToTable("CustomerOpportunityAddress");
            builder.Entity<CustomerOpportunityNote>().ToTable("CustomerOpportunityNote");
            builder.Entity<CustomerOpportunityPaymentMethod>().ToTable("CustomerOpportunityPaymentMethod");
            builder.Entity<CustomerPaymentMethod>().ToTable("CustomerPaymentMethod");
            builder.Entity<Note>().ToTable("Note");
            builder.Entity<NotificationSetting>().ToTable("NotificationSetting");
            builder.Entity<PaymentMethod>().ToTable("PaymentMethod");
            builder.Entity<Permission>().ToTable("Permission");
            builder.Entity<Preference>().ToTable("Preference");
            builder.Entity<Role>().ToTable("Role");
            builder.Entity<RolePermission>().ToTable("RolePermission");
            builder.Entity<SubscriptionType>().ToTable("SubscriptionType");
            builder.Entity<SystemSetting>().ToTable("SystemSetting");
            builder.Entity<TemplateDefinition>().ToTable("TemplateDefinition");
            builder.Entity<User>().ToTable("User");
            builder.Entity<UserNote>().ToTable("UserNote");
            builder.Entity<Country>().ToTable("Country");
            builder.Entity<WindowsTimeZone>().ToTable("WindowsTimeZone");
            builder.Entity<Quote>().ToTable("Quote");
            builder.Entity<ShipmentInformationHeader>().ToTable("ShippingDetail");
            builder.Entity<ShipmentInformationDetail>().ToTable("ShipmentInformationDetail");
            builder.Entity<Airport>().ToTable("Airport");
            builder.Entity<QuotePricing>().ToTable("ReviewPricing");
            builder.Entity<Booking>().ToTable("Booking");
            builder.Entity<BookingAddress>().ToTable("BookingAddress");
            builder.Entity<BookingDocumentMaster>().ToTable("BookingDocumentMaster");
            builder.Entity<BookingNotification>().ToTable("BookingNotification");


            //builder.Entity<DefaultPreference>().ToTable("DefaultPreference");


            #region admin

            builder.Entity<RolePermission>()
              .HasKey(t => new { t.RoleId, t.PermissionId });

            builder.Entity<RolePermission>()
                .HasOne(pt => pt.Role)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(pt => pt.RoleId);

            builder.Entity<RolePermission>()
                .HasOne(pt => pt.Permission)
                .WithMany(t => t.RolePermissions)
                .HasForeignKey(pt => pt.PermissionId);

            #endregion
            builder.Entity<CustomerAddress>()
                .HasKey(t => new { t.Id, t.AddressTypeId, t.AddressId });

            builder.Entity<CustomerAddress>()
                .HasOne(pt => pt.Customer)
                .WithMany(p => p.CustomerAddresses)
                .HasForeignKey(pt => pt.Id);

            builder.Entity<CustomerPaymentMethod>()
                .HasKey(t => new { t.Id, t.PaymentMethodId });

            builder.Entity<CustomerPaymentMethod>()
                .HasOne(pt => pt.Customer)
                .WithMany(p => p.PaymentMethods)
                .HasForeignKey(pt => pt.Id);

            builder.Entity<CustomerContact>()
                .HasKey(t => new { t.Id, t.ContactId, t.AddressId });

            builder.Entity<CustomerContact>()
                .HasOne(pt => pt.Customer)
                .WithMany(p => p.CustomerContacts)
                .HasForeignKey(pt => pt.Id);

            builder.Entity<CustomerNote>()
                .HasKey(t => new { t.Id, t.NoteId });

            builder.Entity<CustomerNote>()
                .HasOne(pt => pt.Customer)
                .WithMany(p => p.CustomerNotes)
                .HasForeignKey(pt => pt.Id);

            builder.Entity<CustomerOpportunityNote>()
                .HasKey(t => new { t.Id, t.NoteId });

            builder.Entity<CustomerOpportunityNote>()
                .HasOne(pt => pt.CustomerOpportunity)
                .WithMany(p => p.CustomerOpportunityNotes)
                .HasForeignKey(pt => pt.Id);

            builder.Entity<CustomerOpportunityPaymentMethod>()
                .HasKey(t => new { t.Id, t.PaymentMethodId });

            builder.Entity<CustomerOpportunityPaymentMethod>()
                .HasOne(pt => pt.CustomerOpportunity)
                .WithMany(p => p.PaymentMethods)
                .HasForeignKey(pt => pt.Id);


            builder.Entity<CustomerOpportunityAddress>()
                .HasKey(t => new { t.Id, t.AddressTypeId, t.AddressId });

            builder.Entity<CustomerOpportunityAddress>()
                .HasOne(pt => pt.CustomerOpportunity)
                .WithMany(p => p.CustomerOpportunityAddresses)
                .HasForeignKey(pt => pt.Id);

            //builder.Entity<DefaultPreference>()
            //    .HasKey(t => new { t.Id,t.PreferenceId });

            //builder.Entity<DefaultPreference>()
            //    .HasOne(pt => pt.SystemSetting)
            //    .WithMany(p => p.DefaultPreferences)
            //    .HasForeignKey(pt => pt.Id);

            //builder.Entity<UserPreference>()
            //    .HasKey(t => new { t.Id, t.PreferenceId });

            //builder.Entity<UserPreference>()
            //    .HasOne(pt => pt.User)
            //    .WithMany(p => p.UserPreferences)
            //    .HasForeignKey(pt => pt.Id);

            builder.Entity<DefaultNotificationSetting>()
                .HasKey(t => new { t.Id, t.NotificationSettingId });

            builder.Entity<DefaultNotificationSetting>()
                .HasOne(pt => pt.SystemSetting)
                .WithMany(p => p.DefaultNotificationSettings)
                .HasForeignKey(pt => pt.Id);

            builder.Entity<UserNotificationSetting>()
                .HasKey(t => new { t.Id, t.NotificationSettingId });

            builder.Entity<UserNotificationSetting>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserNotificationSettings)
                .HasForeignKey(pt => pt.Id);

            builder.Entity<UserNote>()
                .HasKey(t => new { t.Id, t.NoteId });

            builder.Entity<UserNote>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserNotes)
                .HasForeignKey(pt => pt.Id);
        }
        private void BeforeCommit()
        {
            try
            {
                var userName = "Aircon Admin";
                try
                {
                    if( HttpContextHelper.Current != null)
                    if (HttpContextHelper.Current.User.Claims.Count() > 0)
                    {
                        userName = HttpContextHelper.Current.User.FindFirst(ClaimTypes.Name).Value;
                    }
                }
                catch (Exception ex)
                {
                    userName = "Aircon Admin";
                }
                foreach (var dbEntityEntry in ChangeTracker.Entries())
                {
                    if (dbEntityEntry.Entity.GetType().GetProperty("CreatedOnUtc") != null && dbEntityEntry.Entity.GetType().GetProperty("UpdatedOnUtc") != null
                        && dbEntityEntry.Entity.GetType().GetProperty("CreatedBy") != null && dbEntityEntry.Entity.GetType().GetProperty("UpdatedBy") != null)
                    {
                        if (dbEntityEntry.State == EntityState.Added)
                        {
                            dbEntityEntry.CurrentValues["CreatedOnUtc"] = DateTime.UtcNow;
                            dbEntityEntry.CurrentValues["UpdatedOnUtc"] = DateTime.UtcNow;
                            dbEntityEntry.CurrentValues["CreatedBy"] = userName;
                            dbEntityEntry.CurrentValues["UpdatedBy"] = userName;
                        }
                        else if (dbEntityEntry.State == EntityState.Modified)
                        {
                            dbEntityEntry.CurrentValues["UpdatedOnUtc"] = DateTime.UtcNow;
                            dbEntityEntry.CurrentValues["UpdatedBy"] = userName;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex.InnerException);
            }
            finally
            { }
        }
        public override int SaveChanges()
        {
            int result = 0;
            BeforeCommit();
            try
            {
                result = base.SaveChanges(true);
            }catch(Exception ex)
            {

                foreach (var entry in ChangeTracker.Entries())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                throw;
            }
            return result;
        }
    }
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AirconDbContext>
    {
        public AirconDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            var builder = new DbContextOptionsBuilder<AirconDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new AirconDbContext(builder.Options);
        }

    }
}
