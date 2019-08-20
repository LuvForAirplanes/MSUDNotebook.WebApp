using Microsoft.EntityFrameworkCore.Migrations;

namespace MSUDTrack.Services.Migrations
{
    public partial class CascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "records_childid_fkey",
                table: "records");

            migrationBuilder.AddForeignKey(
                name: "records_childid_fkey",
                table: "records",
                column: "ChildId",
                principalTable: "children",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "records_childid_fkey",
                table: "records");

            migrationBuilder.AddForeignKey(
                name: "records_childid_fkey",
                table: "records",
                column: "ChildId",
                principalTable: "children",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
