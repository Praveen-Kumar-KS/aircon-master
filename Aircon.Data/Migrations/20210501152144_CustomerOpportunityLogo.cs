using Microsoft.EntityFrameworkCore.Migrations;

namespace Aircon.Data.Migrations
{
    public partial class CustomerOpportunityLogo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LogoId",
                table: "CustomerOpportunity",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOpportunity_LogoId",
                table: "CustomerOpportunity",
                column: "LogoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOpportunity_Attachment_LogoId",
                table: "CustomerOpportunity",
                column: "LogoId",
                principalTable: "Attachment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOpportunity_Attachment_LogoId",
                table: "CustomerOpportunity");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOpportunity_LogoId",
                table: "CustomerOpportunity");

            migrationBuilder.DropColumn(
                name: "LogoId",
                table: "CustomerOpportunity");
        }
    }
}
