using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LexNews_Charlie.Data.Migrations
{
    public partial class ArhivedArticles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Archived",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archived",
                table: "Articles");
        }
    }
}
