using Microsoft.EntityFrameworkCore.Migrations;

namespace Aircon.Data.Migrations
{
    public partial class Logo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvatarId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LogoId",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_AvatarId",
                table: "User",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_LogoId",
                table: "Customer",
                column: "LogoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Attachment_LogoId",
                table: "Customer",
                column: "LogoId",
                principalTable: "Attachment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Attachment_AvatarId",
                table: "User",
                column: "AvatarId",
                principalTable: "Attachment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Attachment_LogoId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Attachment_AvatarId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_AvatarId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Customer_LogoId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LogoId",
                table: "Customer");
        }
    }
}
