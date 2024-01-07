using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteManagmentFacility.Migrations
{
    /// <inheritdoc />
    public partial class addedbunchofstufftodatabaseasstartaswell2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FacilityId",
                table: "FileUploads",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VehicleId",
                table: "FileUploads",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileUploads_FacilityId",
                table: "FileUploads",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_FileUploads_VehicleId",
                table: "FileUploads",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileUploads_Facilities_FacilityId",
                table: "FileUploads",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FileUploads_Vehicles_VehicleId",
                table: "FileUploads",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileUploads_Facilities_FacilityId",
                table: "FileUploads");

            migrationBuilder.DropForeignKey(
                name: "FK_FileUploads_Vehicles_VehicleId",
                table: "FileUploads");

            migrationBuilder.DropIndex(
                name: "IX_FileUploads_FacilityId",
                table: "FileUploads");

            migrationBuilder.DropIndex(
                name: "IX_FileUploads_VehicleId",
                table: "FileUploads");

            migrationBuilder.DropColumn(
                name: "FacilityId",
                table: "FileUploads");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "FileUploads");
        }
    }
}
