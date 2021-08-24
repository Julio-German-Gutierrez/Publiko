using Microsoft.EntityFrameworkCore.Migrations;

namespace PublikoWebApp.Migrations
{
    public partial class AddedPropertyWebSiteName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WebSiteName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WebSiteName",
                table: "AspNetUsers");
        }
    }
}
