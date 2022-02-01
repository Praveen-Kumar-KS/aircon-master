using Microsoft.EntityFrameworkCore.Migrations;

namespace Aircon.Data.Migrations
{
    public partial class UtcChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "WindowsTimeZone",
                newName: "UpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "WindowsTimeZone",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "UserNotificationSettings",
                newName: "UpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "UserNotificationSettings",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "SignedUpDate",
                table: "User",
                newName: "SignedUpDateUtc");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "User",
                newName: "CreationDateUtc");

            migrationBuilder.RenameColumn(
                name: "ApprovedDate",
                table: "User",
                newName: "ApprovedDateUtc");

            migrationBuilder.RenameColumn(
                name: "ActivatedDate",
                table: "User",
                newName: "ActivatedDateUtc");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "TemplateDefinition",
                newName: "UpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "TemplateDefinition",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "SystemSetting",
                newName: "UpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "SystemSetting",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "SubscriptionType",
                newName: "UpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "SubscriptionType",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Preference",
                newName: "UpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Preference",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Permission",
                newName: "UpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Permission",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "PaymentMethod",
                newName: "UpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "PaymentMethod",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "NotificationSetting",
                newName: "UpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "NotificationSetting",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Note",
                newName: "UpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Note",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "DefaultNotificationSetting",
                newName: "UpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "DefaultNotificationSetting",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "CustomerOpportunity",
                newName: "UpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "SignedUpDate",
                table: "CustomerOpportunity",
                newName: "SignedUpDateUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "CustomerOpportunity",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CallbackScheduledDate",
                table: "CustomerOpportunity",
                newName: "CallbackScheduledDateUtc");

            migrationBuilder.RenameColumn(
                name: "ApprovedDate",
                table: "CustomerOpportunity",
                newName: "ApprovedDateUtc");

            migrationBuilder.RenameColumn(
                name: "AbandonedDate",
                table: "CustomerOpportunity",
                newName: "AbandonedDateUtc");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "CustomerDomains",
                newName: "UpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "CustomerDomains",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Customer",
                newName: "UpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "SubscriptionExpiryDate",
                table: "Customer",
                newName: "SubscriptionExpiryDateUtc");

            migrationBuilder.RenameColumn(
                name: "SignedUpDate",
                table: "Customer",
                newName: "SignedUpDateUtc");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Customer",
                newName: "CreationDateUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Customer",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "ApprovedDate",
                table: "Customer",
                newName: "ApprovedDateUtc");

            migrationBuilder.RenameColumn(
                name: "ActivatedDate",
                table: "Customer",
                newName: "ActivatedDateUtc");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Country",
                newName: "UpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Country",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Contact",
                newName: "UpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Contact",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Branch",
                newName: "UpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Branch",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "AttachmentType",
                newName: "UpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "AttachmentType",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Attachment",
                newName: "UpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Attachment",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "AddressType",
                newName: "UpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "AddressType",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Address",
                newName: "UpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Address",
                newName: "CreatedOnUtc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedOnUtc",
                table: "WindowsTimeZone",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "WindowsTimeZone",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedOnUtc",
                table: "UserNotificationSettings",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "UserNotificationSettings",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "SignedUpDateUtc",
                table: "User",
                newName: "SignedUpDate");

            migrationBuilder.RenameColumn(
                name: "CreationDateUtc",
                table: "User",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "ApprovedDateUtc",
                table: "User",
                newName: "ApprovedDate");

            migrationBuilder.RenameColumn(
                name: "ActivatedDateUtc",
                table: "User",
                newName: "ActivatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedOnUtc",
                table: "TemplateDefinition",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "TemplateDefinition",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedOnUtc",
                table: "SystemSetting",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "SystemSetting",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedOnUtc",
                table: "SubscriptionType",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "SubscriptionType",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedOnUtc",
                table: "Preference",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Preference",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedOnUtc",
                table: "Permission",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Permission",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedOnUtc",
                table: "PaymentMethod",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "PaymentMethod",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedOnUtc",
                table: "NotificationSetting",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "NotificationSetting",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedOnUtc",
                table: "Note",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Note",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedOnUtc",
                table: "DefaultNotificationSetting",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "DefaultNotificationSetting",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedOnUtc",
                table: "CustomerOpportunity",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "SignedUpDateUtc",
                table: "CustomerOpportunity",
                newName: "SignedUpDate");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "CustomerOpportunity",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CallbackScheduledDateUtc",
                table: "CustomerOpportunity",
                newName: "CallbackScheduledDate");

            migrationBuilder.RenameColumn(
                name: "ApprovedDateUtc",
                table: "CustomerOpportunity",
                newName: "ApprovedDate");

            migrationBuilder.RenameColumn(
                name: "AbandonedDateUtc",
                table: "CustomerOpportunity",
                newName: "AbandonedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedOnUtc",
                table: "CustomerDomains",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "CustomerDomains",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedOnUtc",
                table: "Customer",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "SubscriptionExpiryDateUtc",
                table: "Customer",
                newName: "SubscriptionExpiryDate");

            migrationBuilder.RenameColumn(
                name: "SignedUpDateUtc",
                table: "Customer",
                newName: "SignedUpDate");

            migrationBuilder.RenameColumn(
                name: "CreationDateUtc",
                table: "Customer",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Customer",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ApprovedDateUtc",
                table: "Customer",
                newName: "ApprovedDate");

            migrationBuilder.RenameColumn(
                name: "ActivatedDateUtc",
                table: "Customer",
                newName: "ActivatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedOnUtc",
                table: "Country",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Country",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedOnUtc",
                table: "Contact",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Contact",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedOnUtc",
                table: "Branch",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Branch",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedOnUtc",
                table: "AttachmentType",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "AttachmentType",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedOnUtc",
                table: "Attachment",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Attachment",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedOnUtc",
                table: "AddressType",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "AddressType",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedOnUtc",
                table: "Address",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Address",
                newName: "CreatedOn");
        }
    }
}
