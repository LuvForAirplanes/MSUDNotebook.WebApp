using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSUDTrack.Services.Migrations
{
    public partial class Familes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "children");

            migrationBuilder.AddColumn<string>(
                name: "FamilyId",
                table: "children",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChildId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamilyId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "families",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_families", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_children_FamilyId",
                table: "children",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ChildId",
                table: "AspNetUsers",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FamilyId",
                table: "AspNetUsers",
                column: "FamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_children_ChildId",
                table: "AspNetUsers",
                column: "ChildId",
                principalTable: "children",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_families_FamilyId",
                table: "AspNetUsers",
                column: "FamilyId",
                principalTable: "families",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_children_families_FamilyId",
                table: "children",
                column: "FamilyId",
                principalTable: "families",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_children_ChildId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_families_FamilyId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_children_families_FamilyId",
                table: "children");

            migrationBuilder.DropTable(
                name: "families");

            migrationBuilder.DropIndex(
                name: "IX_children_FamilyId",
                table: "children");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ChildId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FamilyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FamilyId",
                table: "children");

            migrationBuilder.DropColumn(
                name: "ChildId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FamilyId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "children",
                nullable: false,
                defaultValue: false);
        }
    }
}
