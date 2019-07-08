using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSUDTrack.Services.Migrations
{
    public partial class CurrentView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CurrentView",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CurrentViewSet",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentView",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CurrentViewSet",
                table: "AspNetUsers");
        }
    }
}
