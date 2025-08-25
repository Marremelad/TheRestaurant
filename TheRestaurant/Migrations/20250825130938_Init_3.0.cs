using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class Init_30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Date_TimeSlot_TableNumber",
                table: "Reservations",
                columns: new[] { "Date", "TimeSlot", "TableNumber" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservations_Date_TimeSlot_TableNumber",
                table: "Reservations");
        }
    }
}
