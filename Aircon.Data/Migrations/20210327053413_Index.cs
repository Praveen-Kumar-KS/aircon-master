using Microsoft.EntityFrameworkCore.Migrations;

namespace Aircon.Data.Migrations
{
    public partial class Index : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_User_IsActive",
                table: "User",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_User_IsEmployee",
                table: "User",
                column: "IsEmployee");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_IsActive",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_IsEmployee",
                table: "User");
        }
    }
}
