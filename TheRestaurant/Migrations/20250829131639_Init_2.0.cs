using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class Init_20 : Migration
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
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Description", "Image", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Fresh Atlantic salmon grilled to perfection, served with lemon herb butter, seasonal vegetables, and wild rice pilaf.", "https://plus.unsplash.com/premium_photo-1723478417559-2349252a3dda?q=80&w=766&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Grilled Salmon", 24.99m },
                    { 2, "Classic wood-fired pizza with fresh mozzarella, San Marzano tomatoes, basil, and extra virgin olive oil on our signature sourdough crust.", "https://images.unsplash.com/photo-1574071318508-1cdbab80d002?q=80&w=869&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Margherita Pizza", 16.50m },
                    { 3, "Crisp romaine lettuce tossed with house-made Caesar dressing, parmesan cheese, croutons, and anchovies.", "https://images.unsplash.com/photo-1546793665-c74683f339c1?q=80&w=387&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Caesar Salad", 12.99m },
                    { 4, "8oz premium beef tenderloin cooked to your preference, served with truffle mashed potatoes and roasted asparagus.", "https://plus.unsplash.com/premium_photo-1723924821443-5bb2822e9a57?q=80&w=867&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Beef Tenderloin", 32.00m },
                    { 5, "Decadent warm chocolate cake with a molten center, served with vanilla ice cream and fresh berries.", "https://images.unsplash.com/photo-1673551490812-eaee2e9bf0ef?q=80&w=870&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Chocolate Lava Cake", 8.95m }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 29, 13, 16, 38, 694, DateTimeKind.Utc).AddTicks(546), "$2a$11$vGeFo7MNZeFiedOpjlPmZORtpmURpkWjwkJjdK5bJUXtOVGSk0GQi" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 29, 11, 51, 0, 671, DateTimeKind.Utc).AddTicks(4968), "$2a$11$mRbIOMHJluNPbjqnADOXwOBCPDyHqIx//vlu9OlZiRGyMbdm1obLG" });
        }
    }
}
