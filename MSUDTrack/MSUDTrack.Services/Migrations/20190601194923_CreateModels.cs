using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSUDTrack.Services.Migrations
{
    public partial class CreateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.CreateTable(
                name: "children",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    last_edited = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    birthday = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_children", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "foods",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    last_edited = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    protein_grams = table.Column<string>(nullable: true),
                    leucine_milligrams = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foods", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "periods",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    last_edited = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    period_start = table.Column<DateTime>(nullable: false),
                    period_end = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_periods", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "records",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    last_edited = table.Column<DateTime>(nullable: false),
                    ChildId = table.Column<string>(nullable: true),
                    FoodId = table.Column<string>(nullable: true),
                    PeriodId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_records", x => x.id);
                    table.ForeignKey(
                        name: "records_childid_fkey",
                        column: x => x.ChildId,
                        principalTable: "children",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "records_foodid_fkey",
                        column: x => x.FoodId,
                        principalTable: "foods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "records_periodid_fkey",
                        column: x => x.PeriodId,
                        principalTable: "periods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_records_ChildId",
                table: "records",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_records_FoodId",
                table: "records",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_records_PeriodId",
                table: "records",
                column: "PeriodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "records");

            migrationBuilder.DropTable(
                name: "children");

            migrationBuilder.DropTable(
                name: "foods");

            migrationBuilder.DropTable(
                name: "periods");

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.id);
                });
        }
    }
}
