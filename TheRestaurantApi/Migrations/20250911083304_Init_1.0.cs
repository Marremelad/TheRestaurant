using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class Init_10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsPopular = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReservationHolds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    TimeSlot = table.Column<int>(type: "int", nullable: false),
                    TableNumber = table.Column<int>(type: "int", nullable: false),
                    TableCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationHolds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Number = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Number);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    TimeSlot = table.Column<int>(type: "int", nullable: false),
                    TableNumber = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Tables_TableNumber",
                        column: x => x.TableNumber,
                        principalTable: "Tables",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    UserIdFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserIdFk",
                        column: x => x.UserIdFk,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Description", "Image", "IsPopular", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Fresh Atlantic salmon grilled to perfection, served with lemon herb butter, seasonal vegetables, and wild rice pilaf.", "https://plus.unsplash.com/premium_photo-1723478417559-2349252a3dda?q=80&w=766&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", true, "Grilled Salmon", 24.99m },
                    { 2, "Classic wood-fired pizza with fresh mozzarella, San Marzano tomatoes, basil, and extra virgin olive oil on our signature sourdough crust.", "https://images.unsplash.com/photo-1574071318508-1cdbab80d002?q=80&w=869&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", true, "Margherita Pizza", 16.50m },
                    { 3, "Crisp romaine lettuce tossed with house-made Caesar dressing, parmesan cheese, croutons, and anchovies.", "https://images.unsplash.com/photo-1546793665-c74683f339c1?q=80&w=387&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", false, "Caesar Salad", 12.99m },
                    { 4, "8oz premium beef tenderloin cooked to your preference, served with truffle mashed potatoes and roasted asparagus.", "https://plus.unsplash.com/premium_photo-1723924821443-5bb2822e9a57?q=80&w=867&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", true, "Beef Tenderloin", 32.00m },
                    { 5, "Decadent warm chocolate cake with a molten center, served with vanilla ice cream and fresh berries.", "https://images.unsplash.com/photo-1673551490812-eaee2e9bf0ef?q=80&w=870&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", true, "Chocolate Lava Cake", 8.95m }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Number", "Capacity" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 4 },
                    { 3, 6 },
                    { 4, 4 },
                    { 5, 8 },
                    { 6, 10 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "PasswordHash", "UserName" },
                values: new object[] { 1, new DateTime(2025, 9, 11, 8, 33, 4, 656, DateTimeKind.Utc).AddTicks(5079), "$2a$11$omBoaf8eFt46vEoWgP9Eh.35usz9tcdkIF/eHmWJrZr8ojKqP3nQO", "admin" });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CreatedAt", "Date", "Email", "FirstName", "LastName", "TableNumber", "TimeSlot" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 11, 8, 33, 4, 656, DateTimeKind.Utc).AddTicks(5994), new DateOnly(2025, 9, 15), "john.smith@email.com", "John", "Smith", 1, 2 },
                    { 2, new DateTime(2025, 9, 11, 8, 33, 4, 656, DateTimeKind.Utc).AddTicks(5995), new DateOnly(2025, 9, 16), "sarah.johnson@email.com", "Sarah", "Johnson", 2, 4 },
                    { 3, new DateTime(2025, 9, 11, 8, 33, 4, 656, DateTimeKind.Utc).AddTicks(5996), new DateOnly(2025, 9, 17), "michael.brown@email.com", "Michael", "Brown", 3, 3 },
                    { 4, new DateTime(2025, 9, 11, 8, 33, 4, 656, DateTimeKind.Utc).AddTicks(5997), new DateOnly(2025, 9, 18), "emily.davis@email.com", "Emily", "Davis", 4, 1 },
                    { 5, new DateTime(2025, 9, 11, 8, 33, 4, 656, DateTimeKind.Utc).AddTicks(5998), new DateOnly(2025, 9, 19), "david.wilson@email.com", "David", "Wilson", 5, 2 },
                    { 6, new DateTime(2025, 9, 11, 8, 33, 4, 656, DateTimeKind.Utc).AddTicks(5999), new DateOnly(2025, 9, 15), "Jason.smith@email.com", "Jason", "Smith", 6, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_Name",
                table: "MenuItems",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserIdFk",
                table: "RefreshTokens",
                column: "UserIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHolds_Date_TimeSlot_TableNumber",
                table: "ReservationHolds",
                columns: new[] { "Date", "TimeSlot", "TableNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Date_TimeSlot_TableNumber",
                table: "Reservations",
                columns: new[] { "Date", "TimeSlot", "TableNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TableNumber",
                table: "Reservations",
                column: "TableNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "ReservationHolds");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Tables");
        }
    }
}
