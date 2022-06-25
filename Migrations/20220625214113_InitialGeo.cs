using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OSDashboardBA.Migrations
{
    public partial class InitialGeo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TextString");

            migrationBuilder.AddColumn<string>(
                name: "GeoJson",
                table: "Layers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Widgets",
                table: "Dashboards",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeoJson",
                table: "Layers");

            migrationBuilder.DropColumn(
                name: "Widgets",
                table: "Dashboards");

            migrationBuilder.CreateTable(
                name: "TextString",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DashboardId = table.Column<int>(type: "int", nullable: true),
                    LayerId = table.Column<int>(type: "int", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextString", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextString_Dashboards_DashboardId",
                        column: x => x.DashboardId,
                        principalTable: "Dashboards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TextString_Layers_LayerId",
                        column: x => x.LayerId,
                        principalTable: "Layers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TextString_DashboardId",
                table: "TextString",
                column: "DashboardId");

            migrationBuilder.CreateIndex(
                name: "IX_TextString_LayerId",
                table: "TextString",
                column: "LayerId");
        }
    }
}
