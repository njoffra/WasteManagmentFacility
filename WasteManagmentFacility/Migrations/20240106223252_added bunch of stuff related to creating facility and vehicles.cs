using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteManagmentFacility.Migrations
{
    /// <inheritdoc />
    public partial class addedbunchofstuffrelatedtocreatingfacilityandvehicles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Facilities_FacilityId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "Waste");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_FacilityId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "FacilityId",
                table: "Vehicles");

            migrationBuilder.AddColumn<Guid>(
                name: "VehicleId",
                table: "Facilities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Facilities");

            migrationBuilder.AddColumn<Guid>(
                name: "FacilityId",
                table: "Vehicles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Waste",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsMetal = table.Column<bool>(type: "bit", nullable: false),
                    IsOrganic = table.Column<bool>(type: "bit", nullable: false),
                    IsPaper = table.Column<bool>(type: "bit", nullable: false),
                    IsPlastic = table.Column<bool>(type: "bit", nullable: false),
                    IsWood = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waste", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Waste_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Waste_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_FacilityId",
                table: "Vehicles",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Waste_FacilityId",
                table: "Waste",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Waste_VehicleId",
                table: "Waste",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Facilities_FacilityId",
                table: "Vehicles",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id");
        }
    }
}
