using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aircon.Data.Migrations
{
    public partial class quoteshipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Quote_ShipmentHeaderId",
                table: "Quote");

            migrationBuilder.AddColumn<int>(
                name: "QuoteId",
                table: "ShippingDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ShipmentInformationDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Length = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuoteId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentInformationDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentInformationDetail_Quote_QuoteId",
                        column: x => x.QuoteId,
                        principalTable: "Quote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShippingDetail_QuoteId",
                table: "ShippingDetail",
                column: "QuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Quote_ShipmentHeaderId",
                table: "Quote",
                column: "ShipmentHeaderId",
                unique: true,
                filter: "[ShipmentHeaderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentInformationDetail_QuoteId",
                table: "ShipmentInformationDetail",
                column: "QuoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingDetail_Quote_QuoteId",
                table: "ShippingDetail",
                column: "QuoteId",
                principalTable: "Quote",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingDetail_Quote_QuoteId",
                table: "ShippingDetail");

            migrationBuilder.DropTable(
                name: "ShipmentInformationDetail");

            migrationBuilder.DropIndex(
                name: "IX_ShippingDetail_QuoteId",
                table: "ShippingDetail");

            migrationBuilder.DropIndex(
                name: "IX_Quote_ShipmentHeaderId",
                table: "Quote");

            migrationBuilder.DropColumn(
                name: "QuoteId",
                table: "ShippingDetail");

            migrationBuilder.CreateIndex(
                name: "IX_Quote_ShipmentHeaderId",
                table: "Quote",
                column: "ShipmentHeaderId");
        }
    }
}
