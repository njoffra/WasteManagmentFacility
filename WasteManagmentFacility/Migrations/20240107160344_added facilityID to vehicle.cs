﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteManagmentFacility.Migrations
{
    /// <inheritdoc />
    public partial class addedfacilityIDtovehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Facilities_FacilityId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "Waste");

            migrationBuilder.AlterColumn<Guid>(
                name: "FacilityId",
                table: "Vehicles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Facilities_FacilityId",
                table: "Vehicles",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Facilities_FacilityId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<Guid>(
                name: "FacilityId",
                table: "Vehicles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Waste",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsMetal = table.Column<bool>(type: "bit", nullable: true),
                    IsOrganic = table.Column<bool>(type: "bit", nullable: true),
                    IsPaper = table.Column<bool>(type: "bit", nullable: true),
                    IsPlastic = table.Column<bool>(type: "bit", nullable: true),
                    IsWood = table.Column<bool>(type: "bit", nullable: true),
                    Quantity = table.Column<float>(type: "real", nullable: false)
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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
