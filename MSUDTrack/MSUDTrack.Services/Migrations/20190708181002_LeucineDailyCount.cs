using Microsoft.EntityFrameworkCore.Migrations;

namespace MSUDTrack.Services.Migrations
{
    public partial class LeucineDailyCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "LeucineDailyCount",
                table: "children",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeucineDailyCount",
                table: "children");
        }
    }
}
