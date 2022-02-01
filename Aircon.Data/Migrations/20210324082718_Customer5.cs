using Microsoft.EntityFrameworkCore.Migrations;

namespace Aircon.Data.Migrations
{
    public partial class Customer5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerContact",
                table: "CustomerContact");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerContact",
                table: "CustomerContact",
                columns: new[] { "Id", "ContactId", "AddressId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerContact",
                table: "CustomerContact");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerContact",
                table: "CustomerContact",
                column: "Id");
        }
    }
}
