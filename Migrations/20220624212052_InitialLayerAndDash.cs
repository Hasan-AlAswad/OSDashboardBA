using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OSDashboardBA.Migrations
{
    public partial class InitialLayerAndDash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dashboards_Users_UserId",
                table: "Dashboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Layers_Users_UsersId",
                table: "Layers");

            migrationBuilder.DropTable(
                name: "DashboardLayer");

            migrationBuilder.DropIndex(
                name: "IX_Dashboards_UserId",
                table: "Dashboards");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "Layers",
                newName: "UserDId");

            migrationBuilder.RenameColumn(
                name: "LayerDescription",
                table: "Layers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Layers",
                newName: "CreatedOn");

            migrationBuilder.RenameIndex(
                name: "IX_Layers_UsersId",
                table: "Layers",
                newName: "IX_Layers_UserDId");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Dashboards",
                newName: "CreatedOn");

            migrationBuilder.AddColumn<int>(
                name: "DashboardId",
                table: "Layers",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Dashboards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserDId",
                table: "Dashboards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TextString",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DashboardId = table.Column<int>(type: "int", nullable: true),
                    LayerId = table.Column<int>(type: "int", nullable: true)
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
                name: "IX_Layers_DashboardId",
                table: "Layers",
                column: "DashboardId");

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_UserDId",
                table: "Dashboards",
                column: "UserDId");

            migrationBuilder.CreateIndex(
                name: "IX_TextString_DashboardId",
                table: "TextString",
                column: "DashboardId");

            migrationBuilder.CreateIndex(
                name: "IX_TextString_LayerId",
                table: "TextString",
                column: "LayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboards_Users_UserDId",
                table: "Dashboards",
                column: "UserDId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Layers_Dashboards_DashboardId",
                table: "Layers",
                column: "DashboardId",
                principalTable: "Dashboards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Layers_Users_UserDId",
                table: "Layers",
                column: "UserDId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dashboards_Users_UserDId",
                table: "Dashboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Layers_Dashboards_DashboardId",
                table: "Layers");

            migrationBuilder.DropForeignKey(
                name: "FK_Layers_Users_UserDId",
                table: "Layers");

            migrationBuilder.DropTable(
                name: "TextString");

            migrationBuilder.DropIndex(
                name: "IX_Layers_DashboardId",
                table: "Layers");

            migrationBuilder.DropIndex(
                name: "IX_Dashboards_UserDId",
                table: "Dashboards");

            migrationBuilder.DropColumn(
                name: "DashboardId",
                table: "Layers");

            migrationBuilder.DropColumn(
                name: "UserDId",
                table: "Dashboards");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Layers",
                newName: "LayerDescription");

            migrationBuilder.RenameColumn(
                name: "UserDId",
                table: "Layers",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Layers",
                newName: "CreatedDate");

            migrationBuilder.RenameIndex(
                name: "IX_Layers_UserDId",
                table: "Layers",
                newName: "IX_Layers_UsersId");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Dashboards",
                newName: "CreatedDate");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Dashboards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DashboardLayer",
                columns: table => new
                {
                    DashbordsId = table.Column<int>(type: "int", nullable: false),
                    LayersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardLayer", x => new { x.DashbordsId, x.LayersId });
                    table.ForeignKey(
                        name: "FK_DashboardLayer_Dashboards_DashbordsId",
                        column: x => x.DashbordsId,
                        principalTable: "Dashboards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DashboardLayer_Layers_LayersId",
                        column: x => x.LayersId,
                        principalTable: "Layers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_UserId",
                table: "Dashboards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardLayer_LayersId",
                table: "DashboardLayer",
                column: "LayersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboards_Users_UserId",
                table: "Dashboards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Layers_Users_UsersId",
                table: "Layers",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
