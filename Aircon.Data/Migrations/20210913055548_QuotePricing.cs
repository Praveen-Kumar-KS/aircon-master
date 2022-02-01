using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aircon.Data.Migrations
{
    public partial class QuotePricing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "ReviewPricing");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "ReviewPricing");

            migrationBuilder.DropColumn(
                name: "CutOffTime",
                table: "ReviewPricing");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "ReviewPricing");

            migrationBuilder.DropColumn(
                name: "OriginId",
                table: "ReviewPricing");

            migrationBuilder.DropColumn(
                name: "State",
                table: "ReviewPricing");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "ReviewPricing");

            migrationBuilder.RenameColumn(
                name: "Zip",
                table: "ReviewPricing",
                newName: "ItemName");

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "ReviewPricing",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CustomerPrice",
                table: "ReviewPricing",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "MarkupPercent",
                table: "ReviewPricing",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Pricing",
                table: "ReviewPricing",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "ReviewPricing");

            migrationBuilder.DropColumn(
                name: "CustomerPrice",
                table: "ReviewPricing");

            migrationBuilder.DropColumn(
                name: "MarkupPercent",
                table: "ReviewPricing");

            migrationBuilder.DropColumn(
                name: "Pricing",
                table: "ReviewPricing");

            migrationBuilder.RenameColumn(
                name: "ItemName",
                table: "ReviewPricing",
                newName: "Zip");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "ReviewPricing",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "ReviewPricing",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CutOffTime",
                table: "ReviewPricing",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "ReviewPricing",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OriginId",
                table: "ReviewPricing",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "ReviewPricing",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "ReviewPricing",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
