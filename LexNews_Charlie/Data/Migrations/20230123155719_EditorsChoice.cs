using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LexNews_Charlie.Data.Migrations
{
    public partial class EditorsChoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherForecast");

            migrationBuilder.AddColumn<bool>(
                name: "EditorsChoice",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditorsChoice",
                table: "Articles");

            migrationBuilder.CreateTable(
                name: "WeatherForecast",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Humidity = table.Column<double>(type: "float", nullable: false),
                    Summary = table.Column<double>(type: "float", nullable: false),
                    TemperatureC = table.Column<double>(type: "float", nullable: false),
                    TemperatureF = table.Column<double>(type: "float", nullable: false),
                    WindSpeed = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecast", x => x.Id);
                });
        }
    }
}
