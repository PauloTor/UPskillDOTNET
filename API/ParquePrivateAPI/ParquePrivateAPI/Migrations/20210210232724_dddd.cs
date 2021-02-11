using Microsoft.EntityFrameworkCore.Migrations;

namespace ParquePrivateAPI.Migrations
{
    public partial class dddd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrecoLugar_Lugar_LugarID1",
                table: "PrecoLugar");

            migrationBuilder.DropIndex(
                name: "IX_PrecoLugar_LugarID1",
                table: "PrecoLugar");

            migrationBuilder.DropColumn(
                name: "LugarID1",
                table: "PrecoLugar");

            migrationBuilder.AlterColumn<long>(
                name: "LugarID",
                table: "PrecoLugar",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_PrecoLugar_LugarID",
                table: "PrecoLugar",
                column: "LugarID");

            migrationBuilder.AddForeignKey(
                name: "FK_PrecoLugar_Lugar_LugarID",
                table: "PrecoLugar",
                column: "LugarID",
                principalTable: "Lugar",
                principalColumn: "LugarID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrecoLugar_Lugar_LugarID",
                table: "PrecoLugar");

            migrationBuilder.DropIndex(
                name: "IX_PrecoLugar_LugarID",
                table: "PrecoLugar");

            migrationBuilder.AlterColumn<int>(
                name: "LugarID",
                table: "PrecoLugar",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "LugarID1",
                table: "PrecoLugar",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrecoLugar_LugarID1",
                table: "PrecoLugar",
                column: "LugarID1");

            migrationBuilder.AddForeignKey(
                name: "FK_PrecoLugar_Lugar_LugarID1",
                table: "PrecoLugar",
                column: "LugarID1",
                principalTable: "Lugar",
                principalColumn: "LugarID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
