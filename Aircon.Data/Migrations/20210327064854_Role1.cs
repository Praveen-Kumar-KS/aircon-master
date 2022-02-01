using Microsoft.EntityFrameworkCore.Migrations;

namespace Aircon.Data.Migrations
{
    public partial class Role1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Role");

            migrationBuilder.RenameColumn(
                name: "SystemName",
                table: "Role",
                newName: "DisplayName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "Role",
                newName: "SystemName");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Role",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
