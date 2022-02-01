using Microsoft.EntityFrameworkCore.Migrations;

namespace Aircon.Data.Migrations
{
    public partial class Initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNotificationSetting_NotificationSetting_NotificationSettingId",
                table: "UserNotificationSetting");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNotificationSetting_User_Id",
                table: "UserNotificationSetting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserNotificationSetting",
                table: "UserNotificationSetting");

            migrationBuilder.RenameTable(
                name: "UserNotificationSetting",
                newName: "UserNotificationSettings");

            migrationBuilder.RenameIndex(
                name: "IX_UserNotificationSetting_NotificationSettingId",
                table: "UserNotificationSettings",
                newName: "IX_UserNotificationSettings_NotificationSettingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserNotificationSettings",
                table: "UserNotificationSettings",
                columns: new[] { "Id", "NotificationSettingId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotificationSettings_NotificationSetting_NotificationSettingId",
                table: "UserNotificationSettings",
                column: "NotificationSettingId",
                principalTable: "NotificationSetting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotificationSettings_User_Id",
                table: "UserNotificationSettings",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNotificationSettings_NotificationSetting_NotificationSettingId",
                table: "UserNotificationSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNotificationSettings_User_Id",
                table: "UserNotificationSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserNotificationSettings",
                table: "UserNotificationSettings");

            migrationBuilder.RenameTable(
                name: "UserNotificationSettings",
                newName: "UserNotificationSetting");

            migrationBuilder.RenameIndex(
                name: "IX_UserNotificationSettings_NotificationSettingId",
                table: "UserNotificationSetting",
                newName: "IX_UserNotificationSetting_NotificationSettingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserNotificationSetting",
                table: "UserNotificationSetting",
                columns: new[] { "Id", "NotificationSettingId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotificationSetting_NotificationSetting_NotificationSettingId",
                table: "UserNotificationSetting",
                column: "NotificationSettingId",
                principalTable: "NotificationSetting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotificationSetting_User_Id",
                table: "UserNotificationSetting",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
