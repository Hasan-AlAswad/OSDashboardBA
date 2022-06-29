using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OSDashboardBA.Migrations
{
    public partial class RemoveUserD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dashboards_UsersD_UserDId",
                table: "Dashboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Layers_UsersD_UserDId",
                table: "Layers");

            migrationBuilder.DropTable(
                name: "UsersD");

            migrationBuilder.DropIndex(
                name: "IX_Layers_UserDId",
                table: "Layers");

            migrationBuilder.DropIndex(
                name: "IX_Dashboards_UserDId",
                table: "Dashboards");

            migrationBuilder.DropColumn(
                name: "UserDId",
                table: "Layers");

            migrationBuilder.DropColumn(
                name: "UserDId",
                table: "Dashboards");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Layers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Dashboards",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "_Id",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Layers_UserId",
                table: "Layers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_UserId",
                table: "Dashboards",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboards_AspNetUsers_UserId",
                table: "Dashboards",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Layers_AspNetUsers_UserId",
                table: "Layers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dashboards_AspNetUsers_UserId",
                table: "Dashboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Layers_AspNetUsers_UserId",
                table: "Layers");

            migrationBuilder.DropIndex(
                name: "IX_Layers_UserId",
                table: "Layers");

            migrationBuilder.DropIndex(
                name: "IX_Dashboards_UserId",
                table: "Dashboards");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "_Id",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Layers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserDId",
                table: "Layers",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Dashboards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserDId",
                table: "Dashboards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UsersD",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersD", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Layers_UserDId",
                table: "Layers",
                column: "UserDId");

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_UserDId",
                table: "Dashboards",
                column: "UserDId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboards_UsersD_UserDId",
                table: "Dashboards",
                column: "UserDId",
                principalTable: "UsersD",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Layers_UsersD_UserDId",
                table: "Layers",
                column: "UserDId",
                principalTable: "UsersD",
                principalColumn: "Id");
        }
    }
}
