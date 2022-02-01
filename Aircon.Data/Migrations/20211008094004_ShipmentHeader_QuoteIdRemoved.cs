using Microsoft.EntityFrameworkCore.Migrations;

namespace Aircon.Data.Migrations
{
    public partial class ShipmentHeader_QuoteIdRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingDetail_Quote_QuoteId",
                table: "ShippingDetail");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingDetail_Quote_QuoteId",
                table: "ShippingDetail",
                column: "QuoteId",
                principalTable: "Quote",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
