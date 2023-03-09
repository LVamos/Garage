using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameEngineToEngineTypeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Engine",
                table: "vehicles",
                newName: "EngineType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EngineType",
                table: "vehicles",
                newName: "Engine");
        }
    }
}
