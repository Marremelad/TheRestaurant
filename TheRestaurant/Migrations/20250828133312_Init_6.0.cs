using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class Init_60 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHolds_Date_TimeSlot_TableNumber",
                table: "ReservationHolds",
                columns: new[] { "Date", "TimeSlot", "TableNumber" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReservationHolds_Date_TimeSlot_TableNumber",
                table: "ReservationHolds");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Reservations");
        }
    }
}
