using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCloudSystem.Dal.Migrations
{
    public partial class FileNameOnServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileNameOnServer",
                table: "Files",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileNameOnServer",
                table: "Files");
        }
    }
}
