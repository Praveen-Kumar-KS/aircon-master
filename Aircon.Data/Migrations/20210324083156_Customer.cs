using Microsoft.EntityFrameworkCore.Migrations;

namespace Aircon.Data.Migrations
{
    public partial class Customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerOpportunityNote",
                table: "CustomerOpportunityNote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerNote",
                table: "CustomerNote");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerOpportunityNote",
                table: "CustomerOpportunityNote",
                columns: new[] { "Id", "NoteId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerNote",
                table: "CustomerNote",
                columns: new[] { "Id", "NoteId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerOpportunityNote",
                table: "CustomerOpportunityNote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerNote",
                table: "CustomerNote");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerOpportunityNote",
                table: "CustomerOpportunityNote",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerNote",
                table: "CustomerNote",
                column: "Id");
        }
    }
}
