using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GarageMVC3.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MembershipType",
                columns: table => new
                {
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipType", x => x.Type);
                });

            migrationBuilder.CreateTable(
                name: "ParkingSpace",
                columns: table => new
                {
                    PK = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpace", x => x.PK);
                });

            migrationBuilder.CreateTable(
                name: "VehicleType",
                columns: table => new
                {
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleType", x => x.Type);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemberPK = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDNumber = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MembershipTypeType = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Joined = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExtendedMemberShipEndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.MemberPK);
                    table.ForeignKey(
                        name: "FK_Member_MembershipType_MembershipTypeType",
                        column: x => x.MembershipTypeType,
                        principalTable: "MembershipType",
                        principalColumn: "Type",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerMemberPK = table.Column<int>(type: "int", nullable: true),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumberOfWheels = table.Column<int>(type: "int", nullable: false),
                    VehicleTypeType = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicle_Member_OwnerMemberPK",
                        column: x => x.OwnerMemberPK,
                        principalTable: "Member",
                        principalColumn: "MemberPK",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicle_VehicleType_VehicleTypeType",
                        column: x => x.VehicleTypeType,
                        principalTable: "VehicleType",
                        principalColumn: "Type",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParkingSpaceVehicle",
                columns: table => new
                {
                    ParkedAtPK = table.Column<int>(type: "int", nullable: false),
                    VehiclesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpaceVehicle", x => new { x.ParkedAtPK, x.VehiclesId });
                    table.ForeignKey(
                        name: "FK_ParkingSpaceVehicle_ParkingSpace_ParkedAtPK",
                        column: x => x.ParkedAtPK,
                        principalTable: "ParkingSpace",
                        principalColumn: "PK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParkingSpaceVehicle_Vehicle_VehiclesId",
                        column: x => x.VehiclesId,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Member_MembershipTypeType",
                table: "Member",
                column: "MembershipTypeType");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpaceVehicle_VehiclesId",
                table: "ParkingSpaceVehicle",
                column: "VehiclesId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_OwnerMemberPK",
                table: "Vehicle",
                column: "OwnerMemberPK");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_VehicleTypeType",
                table: "Vehicle",
                column: "VehicleTypeType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingSpaceVehicle");

            migrationBuilder.DropTable(
                name: "ParkingSpace");

            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "VehicleType");

            migrationBuilder.DropTable(
                name: "MembershipType");
        }
    }
}
