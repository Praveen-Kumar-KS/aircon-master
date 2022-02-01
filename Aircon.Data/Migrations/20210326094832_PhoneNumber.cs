using Microsoft.EntityFrameworkCore.Migrations;

namespace Aircon.Data.Migrations
{
    public partial class PhoneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethod_Address_BillingAddressId",
                table: "PaymentMethod");

            migrationBuilder.RenameColumn(
                name: "EinSsn",
                table: "Customer",
                newName: "EinOrSsn");

            migrationBuilder.AlterColumn<int>(
                name: "BillingAddressId",
                table: "PaymentMethod",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AdminPhoneNumber",
                table: "CustomerOpportunity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminPhoneNumber",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethod_Address_BillingAddressId",
                table: "PaymentMethod",
                column: "BillingAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethod_Address_BillingAddressId",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "AdminPhoneNumber",
                table: "CustomerOpportunity");

            migrationBuilder.DropColumn(
                name: "AdminPhoneNumber",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "EinOrSsn",
                table: "Customer",
                newName: "EinSsn");

            migrationBuilder.AlterColumn<int>(
                name: "BillingAddressId",
                table: "PaymentMethod",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethod_Address_BillingAddressId",
                table: "PaymentMethod",
                column: "BillingAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
