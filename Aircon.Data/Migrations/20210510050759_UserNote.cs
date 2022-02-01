using Microsoft.EntityFrameworkCore.Migrations;

namespace Aircon.Data.Migrations
{
    public partial class UserNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserNote",
                table: "UserNote");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserNote",
                table: "UserNote",
                columns: new[] { "Id", "NoteId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserNote",
                table: "UserNote");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserNote",
                table: "UserNote",
                column: "Id");
        }
    }
}
