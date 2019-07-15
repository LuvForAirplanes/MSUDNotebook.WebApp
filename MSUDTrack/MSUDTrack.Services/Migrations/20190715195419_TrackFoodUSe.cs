using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSUDTrack.Services.Migrations
{
    public partial class TrackFoodUSe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUsed",
                table: "foods",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TimesUsed",
                table: "foods",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUsed",
                table: "foods");

            migrationBuilder.DropColumn(
                name: "TimesUsed",
                table: "foods");
        }
    }
}
