using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage3.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_VehicleType_VehicleTypeType",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleType",
                table: "VehicleType");

            migrationBuilder.RenameTable(
                name: "VehicleType",
                newName: "VehicleTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleTypes",
                table: "VehicleTypes",
                column: "Type");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_VehicleTypes_VehicleTypeType",
                table: "Vehicle",
                column: "VehicleTypeType",
                principalTable: "VehicleTypes",
                principalColumn: "Type",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_VehicleTypes_VehicleTypeType",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleTypes",
                table: "VehicleTypes");

            migrationBuilder.RenameTable(
                name: "VehicleTypes",
                newName: "VehicleType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleType",
                table: "VehicleType",
                column: "Type");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_VehicleType_VehicleTypeType",
                table: "Vehicle",
                column: "VehicleTypeType",
                principalTable: "VehicleType",
                principalColumn: "Type",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
