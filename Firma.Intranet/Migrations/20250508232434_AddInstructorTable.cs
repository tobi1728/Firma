using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firma.Intranet.Migrations
{
    /// <inheritdoc />
    public partial class AddInstructorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstructorRidingTypes");

            migrationBuilder.DropTable(
                name: "RidingTypes");

            migrationBuilder.AddColumn<string>(
                name: "RidingTypes",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RidingTypes",
                table: "Instructors");

            migrationBuilder.CreateTable(
                name: "RidingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RidingTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstructorRidingTypes",
                columns: table => new
                {
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    RidingTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorRidingTypes", x => new { x.InstructorId, x.RidingTypeId });
                    table.ForeignKey(
                        name: "FK_InstructorRidingTypes_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorRidingTypes_RidingTypes_RidingTypeId",
                        column: x => x.RidingTypeId,
                        principalTable: "RidingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstructorRidingTypes_RidingTypeId",
                table: "InstructorRidingTypes",
                column: "RidingTypeId");
        }
    }
}
