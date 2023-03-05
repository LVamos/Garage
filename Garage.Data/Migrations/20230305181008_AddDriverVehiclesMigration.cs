using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDriverVehiclesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DriverVehiclesId",
                table: "vehicles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DriverVehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverVehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverVehicles_drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_DriverVehiclesId",
                table: "vehicles",
                column: "DriverVehiclesId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverVehicles_DriverId",
                table: "DriverVehicles",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_vehicles_DriverVehicles_DriverVehiclesId",
                table: "vehicles",
                column: "DriverVehiclesId",
                principalTable: "DriverVehicles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vehicles_DriverVehicles_DriverVehiclesId",
                table: "vehicles");

            migrationBuilder.DropTable(
                name: "DriverVehicles");

            migrationBuilder.DropIndex(
                name: "IX_vehicles_DriverVehiclesId",
                table: "vehicles");

            migrationBuilder.DropColumn(
                name: "DriverVehiclesId",
                table: "vehicles");
        }
    }
}
