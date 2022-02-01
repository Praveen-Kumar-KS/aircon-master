using Microsoft.EntityFrameworkCore.Migrations;

namespace Aircon.Data.Migrations
{
    public partial class Customer4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerPaymentMethod",
                table: "CustomerPaymentMethod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerOpportunityPaymentMethod",
                table: "CustomerOpportunityPaymentMethod");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerPaymentMethod",
                table: "CustomerPaymentMethod",
                columns: new[] { "Id", "PaymentMethodId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerOpportunityPaymentMethod",
                table: "CustomerOpportunityPaymentMethod",
                columns: new[] { "Id", "PaymentMethodId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerPaymentMethod",
                table: "CustomerPaymentMethod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerOpportunityPaymentMethod",
                table: "CustomerOpportunityPaymentMethod");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerPaymentMethod",
                table: "CustomerPaymentMethod",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerOpportunityPaymentMethod",
                table: "CustomerOpportunityPaymentMethod",
                column: "Id");
        }
    }
}
