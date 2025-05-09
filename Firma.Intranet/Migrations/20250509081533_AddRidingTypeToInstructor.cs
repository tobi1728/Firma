using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firma.Intranet.Migrations
{
    /// <inheritdoc />
    public partial class AddRidingTypeToInstructor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RidingTypes",
                table: "Instructors",
                newName: "RidingType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RidingType",
                table: "Instructors",
                newName: "RidingTypes");
        }
    }
}
