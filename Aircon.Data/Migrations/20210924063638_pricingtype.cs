using Microsoft.EntityFrameworkCore.Migrations;

namespace Aircon.Data.Migrations
{
    public partial class pricingtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pricing",
                table: "ReviewPricing",
                newName: "PricingType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PricingType",
                table: "ReviewPricing",
                newName: "Pricing");
        }
    }
}
