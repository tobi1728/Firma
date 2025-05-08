using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firma.Intranet.Migrations
{
    /// <inheritdoc />
    public partial class AddHorseCheckupsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "HorseCheckups",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "HorseCheckups",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HorseId1",
                table: "HorseCheckups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HorseCheckups_HorseId1",
                table: "HorseCheckups",
                column: "HorseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_HorseCheckups_horses_HorseId1",
                table: "HorseCheckups",
                column: "HorseId1",
                principalTable: "horses",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HorseCheckups_horses_HorseId1",
                table: "HorseCheckups");

            migrationBuilder.DropIndex(
                name: "IX_HorseCheckups_HorseId1",
                table: "HorseCheckups");

            migrationBuilder.DropColumn(
                name: "HorseId1",
                table: "HorseCheckups");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "HorseCheckups",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "HorseCheckups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
