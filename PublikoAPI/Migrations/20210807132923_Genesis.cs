using Microsoft.EntityFrameworkCore.Migrations;

namespace PublikoAPI.Migrations
{
    public partial class Genesis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    PageID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PageTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.PageID);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
