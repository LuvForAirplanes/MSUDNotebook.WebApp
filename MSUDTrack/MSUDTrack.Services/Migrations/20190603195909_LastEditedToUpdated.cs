using Microsoft.EntityFrameworkCore.Migrations;

namespace MSUDTrack.Services.Migrations
{
    public partial class LastEditedToUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "last_edited",
                table: "records",
                newName: "updated");

            migrationBuilder.RenameColumn(
                name: "last_edited",
                table: "periods",
                newName: "updated");

            migrationBuilder.RenameColumn(
                name: "last_edited",
                table: "foods",
                newName: "updated");

            migrationBuilder.RenameColumn(
                name: "last_edited",
                table: "children",
                newName: "updated");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updated",
                table: "records",
                newName: "last_edited");

            migrationBuilder.RenameColumn(
                name: "updated",
                table: "periods",
                newName: "last_edited");

            migrationBuilder.RenameColumn(
                name: "updated",
                table: "foods",
                newName: "last_edited");

            migrationBuilder.RenameColumn(
                name: "updated",
                table: "children",
                newName: "last_edited");
        }
    }
}
