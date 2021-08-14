using System;
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
                    PageOrder = table.Column<int>(type: "int", nullable: false),
                    PageDateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PageDateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PageTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    PostDateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostDateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
