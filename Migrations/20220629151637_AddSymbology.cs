using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OSDashboardBA.Migrations
{
    public partial class AddSymbology : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "Layers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Style",
                table: "Layers");
        }
    }
}
