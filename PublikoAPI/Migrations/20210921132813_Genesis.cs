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
                    PageID = table.Column<string>(type: "TEXT", nullable: false),
                    PageDateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PageDateUpdated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PageTitle = table.Column<string>(type: "TEXT", nullable: false),
                    PageBody = table.Column<string>(type: "TEXT", nullable: false),
                    UserID = table.Column<string>(type: "TEXT", nullable: false),
                    PageOrder = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.PageID);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostID = table.Column<string>(type: "TEXT", nullable: false),
                    PostDateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PostDateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PostTitle = table.Column<string>(type: "TEXT", nullable: false),
                    PostContent = table.Column<string>(type: "TEXT", nullable: false),
                    UserID = table.Column<string>(type: "TEXT", nullable: false)
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
